#!/bin/bash
# shellcheck disable=SC2005,SC2016
DESC="example script to read values from the World Air Quality Index (aqicn.org) & store in LineMetrics Cloud via RestAPI"
VERSION="1.0.0 # 08.2023"
SPACER=##############################

set -u

usage() {
	ME=$(basename "$0")
	echo "### $DESC"
	echo "### Version: $VERSION"
	echo "### https://aqicn.org/json-api/doc/"
	echo "$SPACER"
	echo "$ME [-v] [-h] -s SOURCE_AQI_INDEX -d DESTINATION_ALIAS_IN_LINEMETRICS_CLOUD"
	echo "	-v/--version	$ME -v			=> shows the programm version"
	echo "	-h/--help	$ME -h			=> this usage"
	echo "	-s/--source	$ME -s			=> defines the source for the AQI value gathering "
	echo "	-d/--destination	$ME -d			=> define the uston name or object id in the LineMetrics cloud"
	echo ""
	echo "$SPACER"
	echo " 	example:	$ME -s \"here\" -d \"aqi_test\"		=> queries the nearest AQI station based on IP address & stores it in the custom LineMetrics aliasKey \"aqi_test\""
	echo " 	example:	$ME -s \"Amstetten\" -d \"cb49b2fa7b354891876e1fd91d2ed2d7\"		=> queries the nearest station to Amstetten & stores it in uid cb49b2fa7b354891876e1fd91d2ed2d7"
	echo " 	example:	$ME -s \"geo:48.09761;14.53986\" -d \"cb49b2fa7b354891876e1fd91d2ed2d7\"		=> queries the nearest station to geo:48.09761;14.53986 & stores it in uid cb49b2fa7b354891876e1fd91d2ed2d7"
	echo " 	example:	$ME -s \"@14533\" -d \"cb49b2fa7b354891876e1fd91d2ed2d7\"		=> queries the nearest station to @14533 & stores it in uid cb49b2fa7b354891876e1fd91d2ed2d7"
	echo "$SPACER"
	exit 1
}

LM_ENV="./lm_restapi_import_from_aqicn.env"
if [[ -f "$LM_ENV" ]]; then
	source "$LM_ENV"
else
		echo "no $LM_ENV file"
		usage
fi
EPOCH=$(date +"%s")
CURL=$(command -v curl) || {
	echo "curl not found"
	usage
}
JQ=$(command -v jq) || {
	echo "jq not found"
	usage
}
LM_URLTOKEN=https://restapi.linemetrics.com/oauth/access_token
#LM_URLDATA=https://restapi.linemetrics.com/v2/data/${CUSTOM_KEY}/${ALIAS}
LM_URLDATA=https://restapi.linemetrics.com/v2/data
#LM_CUSTOM_KEY=aqi_test
LM_CREDENTIALS=/tmp/lm_${LM_CLIENT_ID}_credentials
#AQI_LOCATION=here
VALUE_TEMPLATE='[{"val": 0}]'

version() {
	echo "$VERSION"
}

error() {
	echo "Error: (${1:-0})"
	exit 1
}

validate_json() {
	if ! $JQ -e . >/dev/null 2>&1 <<<"$1"; then
		error "invalid json $1"
	fi
}

validate_decimal() {
	if [[ ! "$1" =~ ^[0-9]+(\.[0-9]+)?$ ]]; then
		error "invalid decimal value $1"
	fi
}

get_opts() {
	while [[ $# -gt 0 ]]; do
		key="$1"
		case $key in
		-s | --source)
			shift
			AQI_LOCATION="$1"
			shift
			;;
		-d | --destination)
			shift
			LM_CUSTOM_KEY="$1"
			shift
			;;
		-h | --help)
			usage
			;;
		-v | --version)
			version
			exit
			;;
		*)
			shift
			;;
		esac
	done
}

renew_token() {
	# store received token in temporary credentials file
	$CURL -s -X POST -d "client_id=${LM_CLIENT_ID}&grant_type=client_credentials&client_secret=${LM_CLIENT_SECRET}" "$LM_URLTOKEN" >"$LM_CREDENTIALS"
	EXPIRES_IN=$($JQ -r .expires_in <"$LM_CREDENTIALS")

	# add renew epoch time to credentials file (renew when 75% token lifetime passed)
	#let EPOCH_EXPIRE=$((1 + 3 * EXPIRES_IN / 4))
	EPOCH_EXPIRE=$((1 + 3 * EXPIRES_IN / 4))
	RENEW_AT=$((EPOCH_EXPIRE + EPOCH))
	[[ $RENEW_AT =~ ^-?[0-9]+$ ]] && echo "$($JQ --argjson renew_at "$RENEW_AT" '. += $ARGS.named' <"$LM_CREDENTIALS")" >"$LM_CREDENTIALS"

# finaly reads token on initial run
TOKEN=$($JQ -r .access_token <"$LM_CREDENTIALS")
RENEW=$($JQ -r .renew_at <"$LM_CREDENTIALS")
}

get_opts "$@"
if [ $# -gt 4 ] || [ $# -lt 4 ]; then
	usage
fi

[[ ! -f "$LM_CREDENTIALS" ]] && renew_token
TOKEN=$($JQ -r .access_token <"$LM_CREDENTIALS")
RENEW=$($JQ -r .renew_at <"$LM_CREDENTIALS")
if [ -z "$TOKEN" ]; then
	renew_token
elif [ -z "$RENEW" ]; then
	renew_token
elif [[ "$EPOCH" > "$RENEW" ]]; then
	echo "$EPOCH: renew token ..."
	renew_token
fi

# grab value from aqi database for location
AQI_JSON=$($CURL -s "https://api.waqi.info/feed/$AQI_LOCATION/?token=$AQI_TOKEN")
#echo $AQI_JSON | $JQ
validate_json "$AQI_JSON"
AQI_VALUE=$($JQ -r .data.aqi <<<"$AQI_JSON")
validate_decimal "$AQI_VALUE"
DATA=$($JQ ".[].val=$AQI_VALUE" <<<"$VALUE_TEMPLATE")
validate_json "$DATA"
# post value to LineMetrics RestAPI
RESPONSE=$($CURL -s -X POST "$LM_URLDATA/$LM_CUSTOM_KEY" -H "Authorization: Bearer $TOKEN" -d "$DATA")
if [[ "$(jq -r .[].response <<<"$RESPONSE")" != "Success" ]]; then
	echo "$RESPONSE"
	error "on curl post $LM_CUSTOM_KEY to LineMetrics RestApi"
fi

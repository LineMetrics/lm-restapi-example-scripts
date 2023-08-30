#!/bin/bash
LIST="./lm_restapi_import_from_aqicn_bulk.list"
SCRIPT="./lm_restapi_import_from_aqicn.sh"
[ ! -f "$SCRIPT" ] && echo "Update script \"$SCRIPT\" not found" && exit 1
[ ! -f "$LIST" ] && echo "The file $LIST does not exists" && exit 1

error() {
	echo "Error: (${1:-0})"
	exit 1
}

while IFS= read -r LINE || [[ "$LINE" ]]; do
	GEO=$(cut -d',' -f1 <<<"$LINE")
	LM_UID=$(cut -d',' -f2 <<<"$LINE")
	echo "$GEO # $LM_UID"
	# TODO GEO & UID validation
	$SCRIPT -s "$GEO" -d "$LM_UID" || error "on $GEO - $LM_UID"
done <"$LIST"

# post to healthchecks.io
# curl -fsS -m 10 --retry 5 -o /dev/null https://hc-ping.com/....

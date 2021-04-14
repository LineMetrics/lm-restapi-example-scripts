using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LineMetrics.API;
using LineMetrics.API.Extensions;
using LineMetrics.API.ReturnTypes;

namespace LM_RestAPI_Example_1
{
    public partial class Frm_Main
    {
        //Please insert here your client id and client secret, which you got from the linemetrics support
        private string ms_client_id = "<your LineMetrics Client-ID>";
        private string ms_secret_key = "<your LineMetrics Client-Secret>";

        private ILMService linemetrics_api;



        public class ElementData
        {
            public string ParentID { get; set; }
            
            public string ID { get; set; }
            public string Alias { get; set; }
            public string Text { get; set; }


            public override string ToString()
            {
                return Text;
            }
        }

        public Frm_Main()
        {
            InitializeComponent();
        }
        private void Frm_Main_Load_1(object sender, EventArgs e)
        {
            txt_clientID.Text = ms_client_id;
            txt_clientSecret.Text = ms_secret_key;

            dtpFrom.Value = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            dtpTo.Value = DateTime.Now;

            //LoadRootObjectsFromLineMetricsObjectTree();
        }


        private void _btn_connect_Click(object sender, EventArgs e)
        {
            LoadRootObjectsFromLineMetricsObjectTree();
        }

        /// <summary>
        /// load all root objects from the linemetrics object tree
        /// </summary>
        private void LoadRootObjectsFromLineMetricsObjectTree()
        {
            try
            {
                ms_client_id = txt_clientID.Text;
                ms_secret_key = txt_clientSecret.Text;

                linemetrics_api = new LineMetricsService(ms_client_id, ms_secret_key);

                //Load all objects from the root of the linemetrics object tree
                var rootList = linemetrics_api.LoadAssets();

                List<ElementData> data = new List<ElementData>();
                foreach (Asset _rootEntry in rootList)
                {
                    data.Add(new ElementData() { Text = _rootEntry.Title, ID = _rootEntry.ObjectId });
                }

                lstBox.DataSource = null;
                lstBox.DisplayMember = "Text";
                lstBox.DataSource = data;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Invalid rest api credentials", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// if a root object was selected load the assets/measurementdata underlying
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox rootList = ((ListBox)sender);
            if (rootList == null || rootList.SelectedValue == null) return;
            int idx = rootList.SelectedIndex;
            string id = ((ElementData)rootList.SelectedValue).ID;

            //load the list of measurement data points from the selected object
            var assetObject = linemetrics_api.LoadObject(id);
            var assetList = linemetrics_api.LoadAssets("attribute", assetObject.ObjectId);

            List<ElementData> data = new List<ElementData>();
            foreach (DataStream _lmDataStream in assetList)
            {
                data.Add(new ElementData() { Text = _lmDataStream.Title, ParentID = _lmDataStream.ParentId, ID = _lmDataStream.ObjectId, Alias=_lmDataStream.Alias });
            }

            listBoxAsset.DataSource = null;
            listBoxAsset.DataSource = data;
        }

        private void listBoxAsset_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox assetList = ((ListBox)sender);
            if (assetList == null || assetList.SelectedValue == null) return;
            if (assetList.Items.Count > 0)
            {
                ElementData element = (ElementData)assetList.SelectedValue;
                string values = LoadLineMetricsValues(element);
                txtValues.Text = values;
            }
        }

        private void btGetFrom_Click(object sender, EventArgs e)
        {
            if (listBoxAsset == null || listBoxAsset.SelectedValue == null) return;
            if (listBoxAsset.Items.Count > 0)
            {
                ElementData element = (ElementData)listBoxAsset.SelectedValue;
                string values = LoadLineMetricsValues(element, dtpFrom.Value, dtpTo.Value);
                txtValues.Text = values;
            }
        }

        /// <summary>
        /// load the measurement data of the given element, if datetime from and to are null load only the last value
        /// </summary>
        /// <param name="_element"></param>
        /// <param name="_from"></param>
        /// <param name="_to"></param>
        /// <returns></returns>
        private string LoadLineMetricsValues(ElementData _element, DateTime? _from = null, DateTime? _to = null)
        {
            string retValue = "";

            string parentID = _element.ParentID;
            string id = _element.ID;

            try
            {
                //Select the parent object to get a list of available datastreams
                var assetList = linemetrics_api.LoadAssets("attribute", parentID);

                //Go through all available measurement values
                foreach (DataStream _lmDataStream in assetList)
                {
                    //if we found the correct id, then request the data
                    if (_lmDataStream.ObjectId == id)
                    {
                        //Check if we have to request a timespan or only the last value
                        if (_from != null || _to != null)
                        {

                            DateTime dateFrom = _from ?? DateTime.Now.Subtract(new TimeSpan(1, 0, 0)); //if from or to is null, request at least data from 1 day
                            DateTime dateTo = _to ?? DateTime.Now;

                            //Run LoadLastValues prevent using API Objekt Keys and Aliases => force to request via UID
                            var lastValues = _lmDataStream.LoadData(dateFrom, dateTo, "Europe/Vienna", "PT1M", LineMetrics.API.RequestTypes.Function.RAW,!_lmDataStream.Alias.IsNullOrEmptyString() );

                            if (lastValues != null)
                            {
                                foreach (var _value in lastValues)
                                {
                                    retValue += _value.ToString() + Environment.NewLine;
                                }
                            }
                        }
                        else //load last value of the measuruement point
                        {
                            //Run LoadLastValues prevent using API Objekt Keys and Aliases => force to request via UID
                            var lastValue = _lmDataStream.LoadLastValue(!_lmDataStream.Alias.IsNullOrEmptyString());
                            if (lastValue != null)
                            {
                                retValue = lastValue.ToString() + Environment.NewLine;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }

            return retValue;
        }
    }
}
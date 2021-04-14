using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace LM_RestAPI_Example_1
{
    public partial class Frm_Main : Form
    {
        // Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Wird vom Windows Form-Designer benötigt.
        private System.ComponentModel.IContainer components;

        // Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        // Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        // Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this._txt_clientID = new System.Windows.Forms.TextBox();
            this._Label1 = new System.Windows.Forms.Label();
            this._btn_connect = new System.Windows.Forms.Button();
            this._txt_clientSecret = new System.Windows.Forms.TextBox();
            this._Label2 = new System.Windows.Forms.Label();
            this.lstBox = new System.Windows.Forms.ListBox();
            this.listBoxAsset = new System.Windows.Forms.ListBox();
            this.txtValues = new System.Windows.Forms.TextBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btGetFrom = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _txt_clientID
            // 
            this._txt_clientID.Location = new System.Drawing.Point(83, 6);
            this._txt_clientID.Name = "_txt_clientID";
            this._txt_clientID.Size = new System.Drawing.Size(227, 20);
            this._txt_clientID.TabIndex = 0;
            this._txt_clientID.Text = "<your LineMetrics Client-ID>";
            // 
            // _Label1
            // 
            this._Label1.AutoSize = true;
            this._Label1.Location = new System.Drawing.Point(12, 9);
            this._Label1.Name = "_Label1";
            this._Label1.Size = new System.Drawing.Size(49, 13);
            this._Label1.TabIndex = 1;
            this._Label1.Text = "client_id:";
            // 
            // _btn_connect
            // 
            this._btn_connect.Location = new System.Drawing.Point(165, 58);
            this._btn_connect.Name = "_btn_connect";
            this._btn_connect.Size = new System.Drawing.Size(145, 22);
            this._btn_connect.TabIndex = 2;
            this._btn_connect.Text = "Connect and load objects";
            this._btn_connect.UseVisualStyleBackColor = true;
            this._btn_connect.Click += new System.EventHandler(this._btn_connect_Click);
            // 
            // _txt_clientSecret
            // 
            this._txt_clientSecret.Location = new System.Drawing.Point(83, 32);
            this._txt_clientSecret.Name = "_txt_clientSecret";
            this._txt_clientSecret.Size = new System.Drawing.Size(227, 20);
            this._txt_clientSecret.TabIndex = 0;
            this._txt_clientSecret.Text = "<your LineMetrics Client-Secret>";
            // 
            // _Label2
            // 
            this._Label2.AutoSize = true;
            this._Label2.Location = new System.Drawing.Point(12, 35);
            this._Label2.Name = "_Label2";
            this._Label2.Size = new System.Drawing.Size(70, 13);
            this._Label2.TabIndex = 1;
            this._Label2.Text = "client_secret:";
            // 
            // lstBox
            // 
            this.lstBox.FormattingEnabled = true;
            this.lstBox.Location = new System.Drawing.Point(12, 111);
            this.lstBox.Name = "lstBox";
            this.lstBox.Size = new System.Drawing.Size(298, 264);
            this.lstBox.TabIndex = 4;
            this.lstBox.SelectedIndexChanged += new System.EventHandler(this.lstBox_SelectedIndexChanged);
            // 
            // listBoxAsset
            // 
            this.listBoxAsset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxAsset.FormattingEnabled = true;
            this.listBoxAsset.Location = new System.Drawing.Point(316, 111);
            this.listBoxAsset.Name = "listBoxAsset";
            this.listBoxAsset.Size = new System.Drawing.Size(573, 264);
            this.listBoxAsset.TabIndex = 5;
            this.listBoxAsset.SelectedIndexChanged += new System.EventHandler(this.listBoxAsset_SelectedIndexChanged);
            // 
            // txtValues
            // 
            this.txtValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValues.Location = new System.Drawing.Point(201, 402);
            this.txtValues.Multiline = true;
            this.txtValues.Name = "txtValues";
            this.txtValues.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtValues.Size = new System.Drawing.Size(688, 333);
            this.txtValues.TabIndex = 6;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd.MM.yyy HH:mm";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(58, 402);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(137, 20);
            this.dtpFrom.TabIndex = 7;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd.MM.yyy HH:mm";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(58, 431);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(137, 20);
            this.dtpTo.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "from:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 437);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "to:";
            // 
            // btGetFrom
            // 
            this.btGetFrom.Location = new System.Drawing.Point(97, 458);
            this.btGetFrom.Name = "btGetFrom";
            this.btGetFrom.Size = new System.Drawing.Size(98, 23);
            this.btGetFrom.TabIndex = 8;
            this.btGetFrom.Text = "Load Data ...";
            this.btGetFrom.UseVisualStyleBackColor = true;
            this.btGetFrom.Click += new System.EventHandler(this.btGetFrom_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Object tree:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Measurement values:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 386);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Loaded data:";
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 747);
            this.Controls.Add(this.btGetFrom);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.txtValues);
            this.Controls.Add(this.listBoxAsset);
            this.Controls.Add(this.lstBox);
            this.Controls.Add(this._btn_connect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._Label2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._Label1);
            this.Controls.Add(this._txt_clientSecret);
            this.Controls.Add(this._txt_clientID);
            this.Name = "Frm_Main";
            this.Text = "LineMetrics - RestAPI Example";
            this.Load += new System.EventHandler(this.Frm_Main_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private TextBox _txt_clientID;

        internal TextBox txt_clientID
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txt_clientID;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txt_clientID != null)
                {
                }

                _txt_clientID = value;
                if (_txt_clientID != null)
                {
                }
            }
        }

        private Label _Label1;

        internal Label Label1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Label1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Label1 != null)
                {
                }

                _Label1 = value;
                if (_Label1 != null)
                {
                }
            }
        }

        private Button _btn_connect;

        internal Button btn_connect
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _btn_connect;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_btn_connect != null)
                {
                    _btn_connect.Click -= _btn_connect_Click;
                }

                _btn_connect = value;
                if (_btn_connect != null)
                {
                    _btn_connect.Click += _btn_connect_Click;
                }
            }
        }

        private TextBox _txt_clientSecret;

        internal TextBox txt_clientSecret
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _txt_clientSecret;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_txt_clientSecret != null)
                {
                }

                _txt_clientSecret = value;
                if (_txt_clientSecret != null)
                {
                }
            }
        }

        private Label _Label2;

        internal Label Label2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Label2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Label2 != null)
                {
                }

                _Label2 = value;
                if (_Label2 != null)
                {
                }
            }
        }

 
        private ListBox lstBox;
        private ListBox listBoxAsset;
        private TextBox txtValues;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Label label1;
        private Label label2;
        private Button btGetFrom;
        private Label label3;
        private Label label4;
        private Label label5;


    }
}
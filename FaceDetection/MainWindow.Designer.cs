namespace FaceDetection
{
    partial class MainWindow
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Wymagana metoda obsługi projektanta — nie należy modyfikować 
        /// zawartość tej metody z edytorem kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.leftPicBox = new Emgu.CV.UI.PanAndZoomPictureBox();
            this.rightPicBox = new Emgu.CV.UI.PanAndZoomPictureBox();
            this.btnNextImage = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnCensor = new System.Windows.Forms.Button();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnPrevImage = new System.Windows.Forms.Button();
            this.cbCensorType = new System.Windows.Forms.ComboBox();
            this.btnXML = new System.Windows.Forms.Button();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPicBox)).BeginInit();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPicBox
            // 
            this.leftPicBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.leftPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPicBox.Location = new System.Drawing.Point(0, 0);
            this.leftPicBox.Name = "leftPicBox";
            this.leftPicBox.Size = new System.Drawing.Size(445, 393);
            this.leftPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPicBox.TabIndex = 0;
            this.leftPicBox.TabStop = false;
            // 
            // rightPicBox
            // 
            this.rightPicBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rightPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPicBox.Location = new System.Drawing.Point(0, 0);
            this.rightPicBox.Name = "rightPicBox";
            this.rightPicBox.Size = new System.Drawing.Size(441, 393);
            this.rightPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPicBox.TabIndex = 1;
            this.rightPicBox.TabStop = false;
            // 
            // btnNextImage
            // 
            this.btnNextImage.Enabled = false;
            this.btnNextImage.Location = new System.Drawing.Point(237, 3);
            this.btnNextImage.Name = "btnNextImage";
            this.btnNextImage.Size = new System.Drawing.Size(40, 23);
            this.btnNextImage.TabIndex = 0;
            this.btnNextImage.Text = "->";
            this.btnNextImage.UseVisualStyleBackColor = true;
            this.btnNextImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnNextImage_MouseUp);
            // 
            // lblInfo
            // 
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Location = new System.Drawing.Point(3, 434);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(890, 19);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Idle";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCensor
            // 
            this.btnCensor.Enabled = false;
            this.btnCensor.Location = new System.Drawing.Point(410, 3);
            this.btnCensor.Name = "btnCensor";
            this.btnCensor.Size = new System.Drawing.Size(75, 23);
            this.btnCensor.TabIndex = 3;
            this.btnCensor.Text = "Censor";
            this.btnCensor.UseVisualStyleBackColor = true;
            this.btnCensor.Click += new System.EventHandler(this.BtnCensor_Click);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.lblInfo, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.flowLayoutPanelButtons, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(896, 453);
            this.tableLayoutPanelMain.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.leftPicBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rightPicBox);
            this.splitContainer1.Size = new System.Drawing.Size(890, 393);
            this.splitContainer1.SplitterDistance = 445;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.SizeChanged += new System.EventHandler(this.SplitContainer1_SizeChanged);
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Controls.Add(this.btnLoad);
            this.flowLayoutPanelButtons.Controls.Add(this.btnProcess);
            this.flowLayoutPanelButtons.Controls.Add(this.btnPrevImage);
            this.flowLayoutPanelButtons.Controls.Add(this.btnNextImage);
            this.flowLayoutPanelButtons.Controls.Add(this.cbCensorType);
            this.flowLayoutPanelButtons.Controls.Add(this.btnCensor);
            this.flowLayoutPanelButtons.Controls.Add(this.btnXML);
            this.flowLayoutPanelButtons.Controls.Add(this.btnBrowseOutput);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(890, 27);
            this.flowLayoutPanelButtons.TabIndex = 3;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(3, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(101, 23);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Load images...";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(110, 3);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 8;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.BtnProcess_Click);
            // 
            // btnPrevImage
            // 
            this.btnPrevImage.Enabled = false;
            this.btnPrevImage.Location = new System.Drawing.Point(191, 3);
            this.btnPrevImage.Name = "btnPrevImage";
            this.btnPrevImage.Size = new System.Drawing.Size(40, 23);
            this.btnPrevImage.TabIndex = 4;
            this.btnPrevImage.Text = "<-";
            this.btnPrevImage.UseVisualStyleBackColor = true;
            this.btnPrevImage.Click += new System.EventHandler(this.BtnPrevImage_Click);
            // 
            // cbCensorType
            // 
            this.cbCensorType.FormattingEnabled = true;
            this.cbCensorType.Location = new System.Drawing.Point(283, 3);
            this.cbCensorType.Name = "cbCensorType";
            this.cbCensorType.Size = new System.Drawing.Size(121, 21);
            this.cbCensorType.TabIndex = 9;
            this.cbCensorType.Text = "Whole face";
            this.cbCensorType.SelectedIndexChanged += new System.EventHandler(this.CBCensorType_SelectedIndexChanged);
            // 
            // btnXML
            // 
            this.btnXML.Enabled = false;
            this.btnXML.Location = new System.Drawing.Point(491, 3);
            this.btnXML.Name = "btnXML";
            this.btnXML.Size = new System.Drawing.Size(99, 23);
            this.btnXML.TabIndex = 6;
            this.btnXML.Text = "Export to XML";
            this.btnXML.UseVisualStyleBackColor = true;
            this.btnXML.Click += new System.EventHandler(this.BtnXML_Click);
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Location = new System.Drawing.Point(596, 3);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(129, 23);
            this.btnBrowseOutput.TabIndex = 7;
            this.btnBrowseOutput.Text = "Browse output folder";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.BtnBrowseOutput_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 453);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(640, 350);
            this.Name = "MainWindow";
            this.Text = "FaceDetection";
            ((System.ComponentModel.ISupportInitialize)(this.leftPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPicBox)).EndInit();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.PanAndZoomPictureBox leftPicBox;
        private Emgu.CV.UI.PanAndZoomPictureBox rightPicBox;
        private System.Windows.Forms.Button btnNextImage;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnCensor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnPrevImage;
        private System.Windows.Forms.Button btnXML;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ComboBox cbCensorType;
    }
}


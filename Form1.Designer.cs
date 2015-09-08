namespace RescaleAllImages
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chooseFolderButton = new System.Windows.Forms.Button();
            this.resSlider = new System.Windows.Forms.TrackBar();
            this.qualitySlider = new System.Windows.Forms.TrackBar();
            this.resSliderLabel = new System.Windows.Forms.Label();
            this.qualitySliderLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.goButton = new System.Windows.Forms.Button();
            this.summaryLabel = new System.Windows.Forms.Label();
            this.resSliderLabelLow = new System.Windows.Forms.Label();
            this.resSliderLabelHigh = new System.Windows.Forms.Label();
            this.qualitySliderLabelLow = new System.Windows.Forms.Label();
            this.qualitySliderLabelHigh = new System.Windows.Forms.Label();
            this.keepBackupsCheckbox = new System.Windows.Forms.CheckBox();
            this.subFolderCheckbox = new System.Windows.Forms.CheckBox();
            this.previewScrollContainer = new System.Windows.Forms.Panel();
            this.previewImage = new System.Windows.Forms.PictureBox();
            this.logWindow = new System.Windows.Forms.TextBox();
            this.selectImageFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.resSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).BeginInit();
            this.previewScrollContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // chooseFolderButton
            // 
            resources.ApplyResources(this.chooseFolderButton, "chooseFolderButton");
            this.chooseFolderButton.Name = "chooseFolderButton";
            this.chooseFolderButton.UseVisualStyleBackColor = true;
            this.chooseFolderButton.Click += new System.EventHandler(this.onSelectFolderButtonPress);
            // 
            // resSlider
            // 
            resources.ApplyResources(this.resSlider, "resSlider");
            this.resSlider.Maximum = 5;
            this.resSlider.Name = "resSlider";
            this.resSlider.Value = 2;
            this.resSlider.ValueChanged += new System.EventHandler(this.onSliderValueChange);
            // 
            // qualitySlider
            // 
            resources.ApplyResources(this.qualitySlider, "qualitySlider");
            this.qualitySlider.Maximum = 5;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Value = 2;
            this.qualitySlider.ValueChanged += new System.EventHandler(this.qualSlider_ValueChanged);
            // 
            // resSliderLabel
            // 
            resources.ApplyResources(this.resSliderLabel, "resSliderLabel");
            this.resSliderLabel.Name = "resSliderLabel";
            // 
            // qualitySliderLabel
            // 
            resources.ApplyResources(this.qualitySliderLabel, "qualitySliderLabel");
            this.qualitySliderLabel.Name = "qualitySliderLabel";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // goButton
            // 
            resources.ApplyResources(this.goButton, "goButton");
            this.goButton.Name = "goButton";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.onGoButtonPressed);
            // 
            // summaryLabel
            // 
            resources.ApplyResources(this.summaryLabel, "summaryLabel");
            this.summaryLabel.Name = "summaryLabel";
            // 
            // resSliderLabelLow
            // 
            resources.ApplyResources(this.resSliderLabelLow, "resSliderLabelLow");
            this.resSliderLabelLow.Name = "resSliderLabelLow";
            // 
            // resSliderLabelHigh
            // 
            resources.ApplyResources(this.resSliderLabelHigh, "resSliderLabelHigh");
            this.resSliderLabelHigh.Name = "resSliderLabelHigh";
            // 
            // qualitySliderLabelLow
            // 
            resources.ApplyResources(this.qualitySliderLabelLow, "qualitySliderLabelLow");
            this.qualitySliderLabelLow.Name = "qualitySliderLabelLow";
            // 
            // qualitySliderLabelHigh
            // 
            resources.ApplyResources(this.qualitySliderLabelHigh, "qualitySliderLabelHigh");
            this.qualitySliderLabelHigh.Name = "qualitySliderLabelHigh";
            // 
            // keepBackupsCheckbox
            // 
            resources.ApplyResources(this.keepBackupsCheckbox, "keepBackupsCheckbox");
            this.keepBackupsCheckbox.Checked = true;
            this.keepBackupsCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keepBackupsCheckbox.Name = "keepBackupsCheckbox";
            this.keepBackupsCheckbox.UseVisualStyleBackColor = true;
            // 
            // subFolderCheckbox
            // 
            resources.ApplyResources(this.subFolderCheckbox, "subFolderCheckbox");
            this.subFolderCheckbox.Name = "subFolderCheckbox";
            this.subFolderCheckbox.UseVisualStyleBackColor = true;
            this.subFolderCheckbox.CheckedChanged += new System.EventHandler(this.subFolderCheckbox_CheckedChanged);
            // 
            // previewScrollContainer
            // 
            resources.ApplyResources(this.previewScrollContainer, "previewScrollContainer");
            this.previewScrollContainer.Controls.Add(this.previewImage);
            this.previewScrollContainer.Name = "previewScrollContainer";
            // 
            // previewImage
            // 
            resources.ApplyResources(this.previewImage, "previewImage");
            this.previewImage.Name = "previewImage";
            this.previewImage.TabStop = false;
            // 
            // logWindow
            // 
            this.logWindow.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.logWindow, "logWindow");
            this.logWindow.Name = "logWindow";
            this.logWindow.ReadOnly = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logWindow);
            this.Controls.Add(this.previewScrollContainer);
            this.Controls.Add(this.subFolderCheckbox);
            this.Controls.Add(this.keepBackupsCheckbox);
            this.Controls.Add(this.qualitySliderLabelHigh);
            this.Controls.Add(this.qualitySliderLabelLow);
            this.Controls.Add(this.resSliderLabelHigh);
            this.Controls.Add(this.resSliderLabelLow);
            this.Controls.Add(this.summaryLabel);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.qualitySliderLabel);
            this.Controls.Add(this.resSliderLabel);
            this.Controls.Add(this.qualitySlider);
            this.Controls.Add(this.resSlider);
            this.Controls.Add(this.chooseFolderButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.onMainFormEnter);
            this.Leave += new System.EventHandler(this.Form1_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.resSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).EndInit();
            this.previewScrollContainer.ResumeLayout(false);
            this.previewScrollContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button chooseFolderButton;
        private System.Windows.Forms.TrackBar resSlider;
        private System.Windows.Forms.TrackBar qualitySlider;
        private System.Windows.Forms.Label resSliderLabel;
        private System.Windows.Forms.Label qualitySliderLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.Label resSliderLabelLow;
        private System.Windows.Forms.Label resSliderLabelHigh;
        private System.Windows.Forms.Label qualitySliderLabelLow;
        private System.Windows.Forms.Label qualitySliderLabelHigh;
        private System.Windows.Forms.CheckBox keepBackupsCheckbox;
        private System.Windows.Forms.CheckBox subFolderCheckbox;
        private System.Windows.Forms.Panel previewScrollContainer;
        private System.Windows.Forms.PictureBox previewImage;
        private System.Windows.Forms.TextBox logWindow;
        private System.Windows.Forms.FolderBrowserDialog selectImageFolderDialog;
    }
}


using RescaleAllImages.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace RescaleAllImages
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            setPreview = new SetPreview(setPreviewMethod);
            log = new Log(logMethod);
            registerTempFile = new RegisterTempFile(registerTempFileMethod);
            onDone = new OnDone(onDoneMethod);
        }

        private List<string> tempFiles = new List<string>();


        public delegate void SetPreview(string arg);
        public SetPreview setPreview;
        public void setPreviewMethod(string arg)
        {
            previewImage.ImageLocation = arg;
        }

        public delegate void Log(string arg);
        public Log log;
        private void logMethod(string arg)
        {
            logWindow.AppendText(arg);
        }

        public delegate void RegisterTempFile(string arg);
        public RegisterTempFile registerTempFile;
        private void registerTempFileMethod(string arg)
        {
            tempFiles.Add(arg);
        }

        public delegate void OnDone();
        public OnDone onDone;
        public void onDoneMethod()
        {
            Enabled = true;
        }
        
        private void onSelectFolderButtonPress(object sender, EventArgs e)
        {
            selectImageFolderDialog.ShowDialog();
            updateSummary();
        }

        private void onSliderValueChange(object sender, EventArgs e)
        {
            updateSummary();

        }

        private void onGoButtonPressed(object sender, EventArgs e)
        {
            processFolder();
        }

        private void genPreview()
        {
            var thread = new Thread(new ParameterizedThreadStart(genPreviewThread));
            var args = new DirectorCompressorSettings();
            args.selectedQuality = qualitySlider.Value;
            args.selectedResolution = resSlider.Value;
            args.selectedPath = selectImageFolderDialog.SelectedPath;
            args.processSubfolders = subFolderCheckbox.Checked;
            args.createBackups = keepBackupsCheckbox.Checked;
            thread.Start((object)args);
        }

        private void genPreviewThread(object arg)
        {
            var compressor = new DirectoryCompressor(this);
            var args = arg as DirectorCompressorSettings;
            compressor.qualityIndex = args.selectedQuality;
            compressor.resolutionIndex = args.selectedResolution;
            compressor.selectedPath = args.selectedPath;
            compressor.processSubfolders = args.processSubfolders;
            compressor.createBackups = args.createBackups;
            compressor.genPreview();
        }

        private void processFolder()
        {
            Enabled = false;
            var thread = new Thread(new ParameterizedThreadStart(processFolderThread));
            //thread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            var args = new DirectorCompressorSettings();
            args.selectedQuality = qualitySlider.Value;
            args.selectedResolution = resSlider.Value;
            args.selectedPath = selectImageFolderDialog.SelectedPath;
            args.processSubfolders = subFolderCheckbox.Checked;
            args.createBackups = keepBackupsCheckbox.Checked;
            thread.Start((object)args);
        }

        private void processFolderThread(object arg)
        {
            var compressor = new DirectoryCompressor(this);
            var args = arg as DirectorCompressorSettings;
            compressor.qualityIndex = args.selectedQuality;
            compressor.resolutionIndex = args.selectedResolution;
            compressor.selectedPath = args.selectedPath;
            compressor.processSubfolders = args.processSubfolders;
            compressor.createBackups = args.createBackups;
            compressor.compressFolder();
        }

        private void purgePreviews()
        {
            for (var i = tempFiles.Count - 1; i >= 0; i--)
            {
                try
                {
                    File.Delete(tempFiles[i]);
                    tempFiles.RemoveAt(i);
                }
                catch (Exception) { }
            }
        }

        private void onMainFormEnter(object sender, EventArgs e)
        {
            updateSummary();
        }

        public void updateSummary()
        {
            var resReadable="";

            switch (resSlider.Value){
                case 0:resReadable=RescaleAllImagesStrings.size_stamp+" (320x240)"; break;
                case 1:resReadable=RescaleAllImagesStrings.size_mini+" (640x480)"; break;
                case 2:resReadable=RescaleAllImagesStrings.size_smartphone+" (1280x960)"; break;
                case 3:resReadable=RescaleAllImagesStrings.size_screen+" (1920x1440)"; break;
                case 4:resReadable=RescaleAllImagesStrings.size_quad+" (3840x2880)"; break;
                case 5:resReadable=RescaleAllImagesStrings.size_poster+" (7680x5760)"; break;
            }

            var qualReadable = "";
            switch (qualitySlider.Value)
            {
                case 0: qualReadable = RescaleAllImagesStrings.quality_0; break;
                case 1: qualReadable = RescaleAllImagesStrings.quality_1; break;
                case 2: qualReadable = RescaleAllImagesStrings.quality_2; break;
                case 3: qualReadable = RescaleAllImagesStrings.quality_3; break;
                case 4: qualReadable = RescaleAllImagesStrings.quality_4; break;
                case 5: qualReadable = RescaleAllImagesStrings.quality_5; break;
            }

            var pathReadable = "";
            if (selectImageFolderDialog.SelectedPath != "")
            {
                pathReadable = selectImageFolderDialog.SelectedPath;
                goButton.Enabled = true;
            }
            else
            {
                pathReadable = RescaleAllImagesStrings.path_notselected;
                goButton.Enabled = false;
            }
            summaryLabel.Text = resReadable + ", " + qualReadable + ", " + pathReadable;
            
            genPreview();
        }

        private void qualSlider_ValueChanged(object sender, EventArgs e)
        {
            updateSummary();

        }

        private void subFolderCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            genPreview();
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            purgePreviews();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            purgePreviews();

        }
    }
}

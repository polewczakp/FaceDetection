//using: emgucv-windesktop 3.1.0.2504 
//https://sourceforge.net/projects/emgucv/?source=typ_redirect

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace FaceDetection
{
    public partial class MainWindow : Form
    {
        const bool debugMode = true;
        const string outputFolderPath = "output\\";

        List<ImageProcessor> images;
        int currentID;
        string[] fileList;

        public MainWindow()
        {
            InitializeComponent();
            if (debugMode)
                AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private void BringConsoleToFront()
        {
            SetForegroundWindow(GetConsoleWindow());
        }

        private void ProcessTheImages()
        {
            if (debugMode)
                BringConsoleToFront();

            if (fileList == null)
            {
                fileList = Directory.GetFiles("input");
                Console.WriteLine("No files selected - parsing default input\\ folder.");
            }
            Console.WriteLine("Processing selected images...");
            ImageProcessor.ResetCounters();
            images = new List<ImageProcessor>();
            foreach (var f in fileList)
            {
                if (f.Contains(".jpg"))
                {
                    images.Add(new ImageProcessor(f));
                    currentID = images.Count - 1;
                    ShowImage(currentID);
                    images[currentID].image.Save($"{outputFolderPath}detected_{Path.GetFileName(f)}");
                }
            }
            ShowImage(0);
            Console.WriteLine($"\nDetection summary: \nfaces: {ImageProcessor.faceCount} \nleft eyes: {ImageProcessor.leftEyeCount} " +
                $"\nright eyes: {ImageProcessor.rightEyeCount} \nnoses: {ImageProcessor.noseCount} \nmouths: {ImageProcessor.mouthCount}\n");
            Console.WriteLine("Waiting for decision...");
        }

        private void BtnNextImage_MouseUp(object sender, MouseEventArgs e)
        {
            currentID++;
            ShowImage(currentID);
        }

        private void BtnPrevImage_Click(object sender, EventArgs e)
        {
            currentID--;
            ShowImage(currentID);
        }

        private void ShowImage(int ID)
        {
            HandleButtonEnabling(ref ID);
            try
            {
                lblInfo.Text = "No. " + (ID + 1) + " out of " + images.Count + ". File: " + images[ID].FilePath + ".";
            }
            catch { }
            leftPicBox.Image = Image.FromFile(images[ID].FilePath);
            rightPicBox.Image = images[ID].image.Bitmap;
        }

        private void HandleButtonEnabling(ref int ID)
        {
            currentID = ID;

            var lastID = images.Count - 1;
            var firstID = 0;

            if (ID < lastID && !btnNextImage.Enabled)
                btnNextImage.Enabled = true;
            if (ID >= lastID)
            {
                ID = lastID;
                btnNextImage.Enabled = false;
            }

            if (ID > firstID && !btnPrevImage.Enabled)
                btnPrevImage.Enabled = true;
            if (ID <= firstID)
            {
                ID = firstID;
                btnPrevImage.Enabled = false;
            }

            if (images.Count <= 1)
            {
                btnNextImage.Enabled = false;
                btnPrevImage.Enabled = false;
            }
        }

        private void BtnCensor_Click(object sender, EventArgs e)
        {
            btnCensor.Enabled = false;
            if (debugMode)
                BringConsoleToFront();
            lblInfo.Text = $"Processing...";
            foreach (var i in images)
            {
                lblInfo.Text = $"Censoring {i.FilePath}..."; //doesn't work yet
                i.BeginCensorship();
            }

            lblInfo.Text = $"Processing done. Check output folder for results.";

            Console.WriteLine($"Success!");
            btnCensor.Enabled = true;
        }

        public void ShowCensoredImage(Bitmap censoredImage)
        {
            rightPicBox.Image = censoredImage; //doesn't work yet
        }

        private void SplitContainer1_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainer1.Size.Width > 1)//condition needed because it throws an exception when width = 0 (minimized)
            {
                splitContainer1.SplitterDistance = (int)(0.5 * splitContainer1.Size.Width);
                leftPicBox.SetZoomScale(1.0, new Point(0, 0));
                rightPicBox.SetZoomScale(1.0, new Point(0, 0));
            }
        }

        private void BtnXML_Click(object sender, EventArgs e)
        {
            if (debugMode)
                BringConsoleToFront();
            XmlProcessor.Begin("output\\images.xml");
            foreach (var i in images)
            {
                XmlProcessor.AddImageInfo(i);
            }
            if (XmlProcessor.Finish())
                lblInfo.Text += " Xml written.";
        }

        private void BtnBrowseOutput_Click(object sender, EventArgs e)
        {
            Process.Start(outputFolderPath);
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "Processing...";

            Task.Factory.StartNew(ProcessTheImages);
            //ProcessTheImages();

            btnCensor.Enabled = true;
            btnXML.Enabled = true;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "Choose images you want to process...";
            btnCensor.Enabled = false;
            btnXML.Enabled = false;

            var ofd = new OpenFileDialog()
            {
                InitialDirectory = "input",
                Filter = "JPEG images (*.jpg)|*.jpg",
                //FilterIndex = 2,
                //RestoreDirectory = true,
                Multiselect = true,
                Title = "Choose images you want to process"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (images != null)
                        images.Clear();

                    fileList = ofd.FileNames;
                    lblInfo.Text = $"Selected {ofd.FileNames.Length} files. You can start processing now.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}

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
            cbCensorType.Items.AddRange(GetAreaNameRange());
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

        /// <summary>
        /// Method processing the images from chosen files. Makes sure the user selected .jpg files and starts processing
        /// each of the images. Original images are seen on the left of the window and processed images are on the right.
        /// Saves the processed images to /output folder.
        /// </summary>
        private void ProcessTheImages()
        {
            if (debugMode)
                BringConsoleToFront();

            if (fileList == null)
            {
                try
                {
                    Console.WriteLine("No files selected - parsing default input\\ folder.");
                    fileList = Directory.GetFiles("input");
                }
                catch
                {
                    Console.WriteLine("input\\ folder not found.");
                    MessageBox.Show(new Form { TopMost = true },
                        "Please choose images first.", "Warning");
                    return;
                }
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
                    try
                    {
                        images[currentID].image.Save($"{outputFolderPath}detected_{Path.GetFileName(f)}");
                    }
                    catch (ExternalException)
                    {
                        Console.WriteLine("Folder output\\ not found. Created new folder.");
                        Directory.CreateDirectory(outputFolderPath);
                        images[currentID].image.Save($"{outputFolderPath}detected_{Path.GetFileName(f)}");
                    }
                }
            }
            ShowImage(0);
            ImageProcessor.PrintSummary();
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

        /// <summary>
        /// Takes an ID of an image and shows the image in window.
        /// </summary>
        /// <param name="ID"></param>
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

        /// <summary>
        /// Handles "arrow" buttons. Eg. if the last image is shown, you can't go further, so the ">" button is disabled etc.
        /// </summary>
        /// <param name="ID"></param>
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

        /// <summary>
        /// Handling censor button functionality which starts censoring each image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Resetting image view when resizing window. Useful if user messes up too much.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplitContainer1_SizeChanged(object sender, EventArgs e)
        {
            if (splitContainer1.Size.Width > 1)//condition needed because it throws an exception when width = 0 (window minimized)
            {
                splitContainer1.SplitterDistance = (int)(0.5 * splitContainer1.Size.Width);
                leftPicBox.SetZoomScale(1.0, new Point(0, 0));
                rightPicBox.SetZoomScale(1.0, new Point(0, 0));
                leftPicBox.VerticalScrollBar.Value = 0;
                rightPicBox.VerticalScrollBar.Value = 0;
                leftPicBox.HorizontalScrollBar.Value = 0;
                rightPicBox.HorizontalScrollBar.Value = 0;
            }
        }

        /// <summary>
        /// Handling XML button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnXML_Click(object sender, EventArgs e)
        {
            if (debugMode)
                BringConsoleToFront();
            XmlProcessor.Begin("output\\images.xml");
            foreach (var i in images)
            {
                XmlProcessor.AddImageInfo(i, ImageProcessor.CensorType);
            }
            if (XmlProcessor.Finish())
                lblInfo.Text += " Xml written.";
        }

        /// <summary>
        /// Shows output folder from File Explorer. If the folder doesn't exist, it's created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseOutput_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(outputFolderPath);
            }
            catch (ExternalException)
            {
                Console.WriteLine("Folder output\\ not found. Created new folder.");
                Directory.CreateDirectory(outputFolderPath);
                Process.Start(outputFolderPath);
            }
        }

        /// <summary>
        /// Starts processing chosen images.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnProcess_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "Processing...";

            Task.Factory.StartNew(ProcessTheImages);
            //ProcessTheImages();

            btnCensor.Enabled = true;
            btnXML.Enabled = true;
        }

        /// <summary>
        /// Opens file dialog for chosing images to process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void CBCensorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageProcessor.CensorType = cbCensorType.SelectedIndex;
        }

        /// <summary>
        /// Describes a list of censor area types for ComboBox.
        /// </summary>
        /// <returns></returns>
        public object[] GetAreaNameRange()
        {
            return new object[]
            {
                new AreaCB(ImageProcessor.CensorAreas.Face,"Whole face"),
                new AreaCB(ImageProcessor.CensorAreas.Stripe,"Stripe"),
                new AreaCB(ImageProcessor.CensorAreas.Separate,"Separate eyes")
            };
        }


        /// <summary>
        /// Handling of the ComboBox items.
        /// </summary>
        public struct AreaCB
        {
            public ImageProcessor.CensorAreas _type;
            public string _caption;

            public AreaCB(ImageProcessor.CensorAreas type, string caption)
            {
                _type = type;
                _caption = caption;
            }

            public ImageProcessor.CensorAreas GetNumber()
            {
                return _type;
            }

            public override string ToString()
            {
                return _caption;
            }
        }
    }
}

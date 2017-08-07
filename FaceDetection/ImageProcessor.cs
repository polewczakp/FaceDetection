//using: emgucv-windesktop 3.1.0.2504 (other versions might also work)
//https://sourceforge.net/projects/emgucv/?source=typ_redirect

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceDetection
{
    public class ImageProcessor
    {
        /// <summary>
        /// Stores an image and takes care of face detection and censorship.
        /// </summary>
        public double censorPixelSize = 5;
        const int lineThickness = 2;

        public string FilePath { get; set; }
        public IImage image;
        public Bitmap censoredImage;
        public uint ID;
        public static int CensorType { get; set; } = (int)CensorAreas.Face;
        public static int CensorMode { get; set; } = (int)CensorModes.FillWithBlack;

        public enum CensorAreas { Face, Stripe, Separate }
        public enum CensorModes { FillWithBlack, Pixelize }

        public List<Rectangle> faces;
        List<Rectangle> eyePairs;
        List<Rectangle> leftEyes;
        List<Rectangle> rightEyes;
        List<Rectangle> noses;
        List<Rectangle> mouths;

        private static int imageCount;

        private static int faceCount;
        private static int eyePairCount;
        private static int leftEyeCount;
        private static int rightEyeCount;
        private static int noseCount;
        private static int mouthCount;

        /// <summary>
        /// Takes path of the image file, retrieves an image and starts detection process.
        /// </summary>
        /// <param name="path"></param>
        public ImageProcessor(string path)
        {
            imageCount++;
            ID = (uint)imageCount;
            FilePath = path;

            image = new UMat(FilePath, ImreadModes.Color);
            Detect();
        }

        /// <summary>
        /// Detects objects from an image and stores them as rectangles.
        /// </summary>
        private void Detect()
        {
            //areas of detected elements are described as rectangles
            faces = new List<Rectangle>();
            eyePairs = new List<Rectangle>();
            leftEyes = new List<Rectangle>();
            rightEyes = new List<Rectangle>();
            noses = new List<Rectangle>();
            mouths = new List<Rectangle>();
            try
            {
                DetectFace.Detect(
                    image, "haarcascades\\frontalface_default.xml", "haarcascades\\eyes.xml", "haarcascades\\lefteye_2splits.xml",
                    "haarcascades\\righteye_2splits.xml", "haarcascades\\nose.xml", "haarcascades\\mouth.xml",
                    faces, eyePairs, leftEyes, rightEyes, noses, mouths,
                    out long detectionTime);

                Console.WriteLine($"Image {FilePath} finished, detection time: {detectionTime} ms");
            }
            catch (Emgu.CV.Util.CvException e)
            {
                Console.WriteLine("\n" + e.Message);
                Console.WriteLine("ERROR: Out of memory! Try to load smaller images!");
                System.Windows.Forms.MessageBox.Show(new System.Windows.Forms.Form { TopMost = true }, 
                    "Out of memory! Try to load smaller images!", "Warning");                
            }

            foreach (Rectangle f in faces)
            {
                CvInvoke.Rectangle(image, f, new Bgr(Color.Red).MCvScalar, lineThickness);
                faceCount++;
            }
            foreach (Rectangle e in eyePairs)
            {
                CvInvoke.Rectangle(image, e, new Bgr(Color.Green).MCvScalar, lineThickness);
                eyePairCount++;
            }
            foreach (Rectangle le in leftEyes)
            {
                CvInvoke.Rectangle(image, le, new Bgr(Color.Pink).MCvScalar, lineThickness);
                leftEyeCount++;
            }
            foreach (Rectangle re in rightEyes)
            {
                CvInvoke.Rectangle(image, re, new Bgr(Color.Blue).MCvScalar, lineThickness);
                rightEyeCount++;
            }
            foreach (Rectangle n in noses)
            {
                CvInvoke.Rectangle(image, n, new Bgr(Color.White).MCvScalar, lineThickness);
                noseCount++;
            }
            foreach (Rectangle m in mouths)
            {
                CvInvoke.Rectangle(image, m, new Bgr(Color.Black).MCvScalar, lineThickness);
                mouthCount++;
            }
        }

        public void BeginCensorship()
        {
            Console.WriteLine($"Censoring {FilePath}...");
            CensorTheArea();
        }

        /// <summary>
        /// Censors an image by chosen mode and saves the file to \output folder.
        /// </summary>
        private void CensorTheArea()
        {
            censoredImage = (new UMat(FilePath, ImreadModes.Color)).Bitmap;
            switch (CensorMode)
            {
                case (int)CensorModes.FillWithBlack:
                    FillWithBlack(censoredImage, Color.Black);
                    break;
                case (int)CensorModes.Pixelize:
                    Pixelize(censoredImage);
                    break;
                default:
                    throw new NotImplementedException();
            }

            string newPath = $"output\\censored_{Path.GetFileName(FilePath)}";
            censoredImage.Save(newPath);
        }

        /// <summary>
        /// A simplified version of censorship - censoring the area with black rectangle.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="color"></param>
        private void FillWithBlack(Bitmap bitmap, Color color)
        {
            var areas = GetAreaOf(CensorType);
            using (Graphics gfx = Graphics.FromImage(bitmap))
            using (SolidBrush brush = new SolidBrush(color))
                foreach (Rectangle a in areas)
                    gfx.FillRectangle(brush, a);
        }

        /// <summary>
        /// TODO: Pixelization - more complex way of censoring.
        /// </summary>
        /// <param name="bitmap"></param>
        private void Pixelize(Bitmap bitmap)
        {
            throw new NotImplementedException("TODO: Pixelize(censor mode)");
        }

        /// <summary>
        /// Returns needed rectangles according to chosen censorship mode.
        /// </summary>
        /// <param name="areaMode"></param>
        /// <returns></returns>
        private List<Rectangle> GetAreaOf(int areaMode)
        {
            var areas = new List<Rectangle>();
            switch (areaMode)
            {
                case (int)CensorAreas.Face:
                    foreach (Rectangle f in faces)
                        areas.Add(f);
                    break;
                case (int)CensorAreas.Stripe:
                    foreach (Rectangle e in eyePairs)
                        areas.Add(e);
                    break;
                case (int)CensorAreas.Separate:
                    foreach (Rectangle le in leftEyes)
                        areas.Add(le);
                    foreach (Rectangle re in rightEyes)
                        areas.Add(re);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return areas;
        }

        public static void ResetCounters()
        {
            faceCount = 0;
            eyePairCount = 0;
            leftEyeCount = 0;
            rightEyeCount = 0;
            noseCount = 0;
            mouthCount = 0;
        }

        public static void PrintSummary()
        {
            Console.WriteLine($"\nDetection summary: \nfaces: {faceCount} \nleft eyes: {leftEyeCount} " +
                $"\nright eyes: {rightEyeCount} \nnoses: {noseCount} \nmouths: {mouthCount}\n");
        }
    }
}

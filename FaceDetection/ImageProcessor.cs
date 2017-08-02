using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace FaceDetection
{
    public class ImageProcessor
    {
        public double censorPixelSize = 5;
        const int lineThickness = 2;

        public string FilePath { get; set; }
        public IImage image;
        public uint ID;
        public int censorType = (int)CensorArea.Face;
        public int censorMode = (int)CensorMode.FillWithColor;

        public enum CensorArea { Face, Stripe, Separate }
        public enum CensorMode { FillWithColor, Pixelize }

        public List<Rectangle> faces;
        List<Rectangle> eyePairs;
        List<Rectangle> leftEyes;
        List<Rectangle> rightEyes;
        List<Rectangle> noses;
        List<Rectangle> mouths;

        public static int imageCount;

        public static int faceCount;
        public static int eyePairCount;
        public static int leftEyeCount;
        public static int rightEyeCount;
        public static int noseCount;
        public static int mouthCount;

        public ImageProcessor(string path)
        {
            imageCount++;
            ID = (uint)imageCount;
            FilePath = path;

            image = new UMat(FilePath, ImreadModes.Color);
            Detect();
        }

        private void Detect()
        {
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
                Console.WriteLine("ERROR: Out of memory! Try load smaller images!");
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

        private void CensorTheArea()
        {
            Bitmap newImage = (new UMat(FilePath, ImreadModes.Color)).Bitmap;
            string prefix;
            switch (censorMode)
            {
                case (int)CensorMode.FillWithColor:
                    FillWithColor(newImage, Color.Black);
                    prefix = "filled";
                    break;
                case (int)CensorMode.Pixelize:
                    Pixelize(newImage);
                    prefix = "pixelized";
                    break;
                default:
                    throw new NotImplementedException();
            }

            string newPath = $"output\\{prefix}_{Path.GetFileName(FilePath)}";
            newImage.Save(newPath);
        }

        private void FillWithColor(Bitmap bitmap, Color color)
        {
            var areas = GetAreaOf(censorType);
            using (Graphics gfx = Graphics.FromImage(bitmap))
            using (SolidBrush brush = new SolidBrush(color))
                foreach (Rectangle a in areas)
                    gfx.FillRectangle(brush, a);
        }

        private void Pixelize(Bitmap bitmap)
        {
            throw new NotImplementedException("TODO: Pixelize(censor mode)");
        }

        private List<Rectangle> GetAreaOf(int areaMode)
        {
            var areas = new List<Rectangle>();
            switch (areaMode)
            {
                case (int)CensorArea.Face:
                    foreach (Rectangle f in faces)
                        areas.Add(f);
                    break;
                case (int)CensorArea.Stripe:
                    foreach (Rectangle e in eyePairs)
                        areas.Add(e);
                    break;
                case (int)CensorArea.Separate:
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
    }
}

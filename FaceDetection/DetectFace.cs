using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Emgu.CV;

namespace FaceDetection
{
    public static class DetectFace
    {
        public static void Detect(IInputArray image,
           String facePath, String eyePairPath, String leftEyeFileName, String rightEyeFileName, String noseFileName, String mouthFileName,
           List<Rectangle> faces, List<Rectangle> eyePairs, List<Rectangle> leftEyes, List<Rectangle> rightEyes, List<Rectangle> noses, List<Rectangle> mouths,
           out long detectionTime)
        {
            Stopwatch watch = Stopwatch.StartNew();

            using (CascadeClassifier face = new CascadeClassifier(facePath))
            using (CascadeClassifier eyePair = new CascadeClassifier(eyePairPath))
            using (CascadeClassifier leftEye = new CascadeClassifier(leftEyeFileName))
            using (CascadeClassifier rightEye = new CascadeClassifier(rightEyeFileName))
            using (CascadeClassifier nose = new CascadeClassifier(noseFileName))
            using (CascadeClassifier mouth = new CascadeClassifier(mouthFileName))
            using (UMat ugray = new UMat())
            {
                CvInvoke.CvtColor(image, ugray, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
                CvInvoke.EqualizeHist(ugray, ugray);
                Rectangle[] facesFound = face.DetectMultiScale(ugray, 1.1, 15, new Size(60, 60));
                faces.AddRange(facesFound);

                foreach (Rectangle f in facesFound)
                {
                    using (UMat faceRegion = new UMat(ugray, f))
                    {
                        var sizes = new PartsSizes(f);

                        Rectangle[] eyePairsFound = eyePair.DetectMultiScale(faceRegion, 1.03, 0, sizes.eyePairMin, sizes.eyePairMax);
                        Rectangle[] leftEyesFound = leftEye.DetectMultiScale(faceRegion, 1.03, 0, sizes.eyeMin, sizes.eyeMax);
                        Rectangle[] rightEyesFound = rightEye.DetectMultiScale(faceRegion, 1.03, 0, sizes.eyeMin, sizes.eyeMax);
                        Rectangle[] nosesFound = nose.DetectMultiScale(faceRegion, 1.05, 0, sizes.noseMin, sizes.noseMax);
                        Rectangle[] mouthsFound = mouth.DetectMultiScale(faceRegion, 1.05, 0, sizes.mouthMin, sizes.mouthMax);

                        FindValidRectInFace(f, eyePairsFound, eyePairs, 0.1f, 0.5f, 0.1f, 0.35f);
                        FindValidRectInFace(f, leftEyesFound, leftEyes, 0.5f, 0.8f, 0.25f, 0.4f);
                        FindValidRectInFace(f, rightEyesFound, rightEyes, 0.1f, 0.5f, 0.25f, 0.4f);
                        FindValidRectInFace(f, nosesFound, noses, 0.3f, 0.6f, 0.35f, 0.5f);
                        FindValidRectInFace(f, mouthsFound, mouths, 0.3f, 0.5f, 0.7f, 0.9f);
                    }
                }
            }
            watch.Stop();
            detectionTime = watch.ElapsedMilliseconds;
        }

        private static void FindValidRectInFace(Rectangle face, Rectangle[] inputRects, List<Rectangle> outputRects, float minX, float maxX, float minY, float maxY)
        {
            foreach (Rectangle rect in inputRects)
            {
                Rectangle temp = rect;
                temp.Offset(face.X, face.Y);
                if (rect.X > minX * face.Size.Width && rect.X < maxX * face.Size.Width)
                    if (rect.Y > minY * face.Size.Height && rect.Y < maxY * face.Size.Height)
                    {
                        outputRects.Add(temp);
                        break;
                    }
            }
        }

        public struct PartsSizes
        {
            //min/max sizes of face parts defined by size of face [%]
            const float eyePairMinX = 40;
            const float eyePairMinY = 15;
            const float eyePairMaxX = 80;
            const float eyePairMaxY = 30;

            const float eyeMinX = 15;
            const float eyeMinY = 15;
            const float eyeMaxX = 20;
            const float eyeMaxY = 20;

            const float noseMinX = 20;
            const float noseMinY = 20;
            const float noseMaxX = 50;
            const float noseMaxY = 50;

            const float mouthMinX = 25;
            const float mouthMinY = 15;
            const float mouthMaxX = 50;
            const float mouthMaxY = 30;

            public Size eyePairMin, eyePairMax;
            public Size eyeMin, eyeMax;
            public Size noseMin, noseMax;
            public Size mouthMin, mouthMax;

            public PartsSizes(Rectangle face)
            {
                eyePairMin = new Size((int)(eyePairMinX * 0.01 * face.Size.Width), (int)(eyePairMinY * 0.01 * face.Size.Height));
                eyePairMax = new Size((int)(eyePairMaxX * 0.01 * face.Size.Width), (int)(eyePairMaxY * 0.01 * face.Size.Height));

                eyeMin = new Size((int)(eyeMinX * 0.01 * face.Size.Width), (int)(eyeMinY * 0.01 * face.Size.Height));
                eyeMax = new Size((int)(eyeMaxX * 0.01 * face.Size.Width), (int)(eyeMaxY * 0.01 * face.Size.Height));

                noseMin = new Size((int)(noseMinX * 0.01 * face.Size.Width), (int)(noseMinY * 0.01 * face.Size.Height));
                noseMax = new Size((int)(noseMaxX * 0.01 * face.Size.Width), (int)(noseMaxY * 0.01 * face.Size.Height));

                mouthMin = new Size((int)(mouthMinX * 0.01 * face.Size.Width), (int)(mouthMinY * 0.01 * face.Size.Height));
                mouthMax = new Size((int)(mouthMaxX * 0.01 * face.Size.Width), (int)(mouthMaxY * 0.01 * face.Size.Height));
            }
        }
    }
}

using System;
using System.Drawing;
using System.Text;
using System.Xml;

namespace FaceDetection
{
    public static class XmlProcessor
    {
        private static string XmlPath { get; set; }
        private static XmlWriter xml;
        private static XmlWriterSettings settings = new XmlWriterSettings()
        {
            Indent = true,
            IndentChars = ("\t"),
            Encoding = Encoding.GetEncoding("Windows-1250")
        };

        public static void Begin(string path = "output\\output.xml")
        {
            XmlPath = path;
            Console.WriteLine($"Writing XML to {XmlPath}.");
            xml = XmlWriter.Create(path, settings);
            xml.WriteStartDocument();
            xml.WriteStartElement("faceDetection");
        }

        public static void AddImageInfo(ImageProcessor image)
        {

            xml.WriteStartElement("image");
            xml.WriteAttributeString("name", image.FilePath);

            xml.WriteStartElement("width");
            xml.WriteValue(image.image.Bitmap.Width);
            xml.WriteEndElement();

            xml.WriteStartElement("height");
            xml.WriteValue(image.image.Bitmap.Height);
            xml.WriteEndElement();

            xml.WriteStartElement("censoredObject");
            switch (image.censorType)
            {
                case (int)ImageProcessor.CensorArea.Face:
                    xml.WriteValue("face");
                    break;
                case (int)ImageProcessor.CensorArea.Stripe:
                    xml.WriteValue("eyes (stripe)");
                    break;
                case (int)ImageProcessor.CensorArea.Separate:
                    xml.WriteValue("eyes (separate)");
                    break;
                default:
                    throw new NotImplementedException();
            }
            xml.WriteEndElement();

            xml.WriteStartElement("censorPixelSize");
            xml.WriteValue(image.censorPixelSize);
            xml.WriteEndElement();

            xml.WriteStartElement("faces");
            foreach (Rectangle face in image.faces)
            {
                xml.WriteStartElement("face");
                xml.WriteAttributeString("area", $"[{face.X},{face.Y},{face.Width},{face.Height}]");
                xml.WriteEndElement();
            }
            xml.WriteEndElement();//faces

            xml.WriteEndElement(); //image
        }

        public static bool Finish()
        {
            xml.WriteEndDocument();
            xml.Close();
            Console.WriteLine($"Xml written.");
            return true;
        }


    }
}

using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Emgu.CV;
using System.IO;
using System;
using System.Xml.Serialization;

namespace PDI_Final
{
    class FaceDetector
    {
        private CascadeClassifier _classifier;

        public FaceDetector(string xmlPath)
        {
            _classifier = new CascadeClassifier(xmlPath);
        }

        public Rectangle[] Detect(Mat image)
        {
            var scaleFactor = 1.2f;
            var minNeighbors = 5;
            var minSize = new Size(30, 30);
            return _classifier.DetectMultiScale(image, scaleFactor, minNeighbors, minSize);
        }

        private List<Mat> CutFaces(Mat image, Rectangle[] facesCoord)
        {
            List<Mat> faces = new List<Mat>();

            foreach (var faceCoord in facesCoord)
            {
                var w_rm = (int)(0.3 * faceCoord.Width / 2);
                var roi = new Rectangle(
                        x: faceCoord.X + w_rm,
                        y: faceCoord.Y,
                        width: faceCoord.Width - w_rm,
                        height: faceCoord.Height
                        );
                faces.Add(new Mat(image, roi));
            }

            return faces;
        }

        private List<Mat> Resize(List<Mat> images, Size? size = null)
        {
            size = size ?? new Size(224, 224);

            List<Mat> images_norm = new List<Mat>();
            foreach (var image in images)
            {
                var image_norm = new Mat();
                if (image.Size.Width < size?.Width && image.Size.Height < size?.Height)
                    CvInvoke.Resize(image, image_norm, size.Value, interpolation: Emgu.CV.CvEnum.Inter.Area);
                else
                    CvInvoke.Resize(image, image_norm, size.Value, interpolation: Emgu.CV.CvEnum.Inter.Cubic);
                images_norm.Add(image_norm);
            }

            return images_norm;
        }

        public List<Mat> NormalizeFaces(Mat image, Rectangle[] facesCoord)
        {
            var faces = CutFaces(image, facesCoord);
            faces = Resize(faces);

            return faces;
        }

        public (List<Mat>, string[], Dictionary<int, string>) CollectDataset(string rootPath, bool train = false)
        {
            var images = new List<Mat>();
            var labels = new List<string>();
            var labels_dic = new Dictionary<int, string>();
            var aux = train ? "_allign" : "";
            var people = Directory.GetDirectories(rootPath + Path.DirectorySeparatorChar + "people"+aux)
                .Select((value, index) => new { index, value })
                .ToDictionary(pair => pair.index, pair => Path.GetFileName(pair.value));

            var mode = train ? Emgu.CV.CvEnum.ImreadModes.Grayscale : Emgu.CV.CvEnum.ImreadModes.Color;

            for (int i = 0; i < people.Count; i++)
            {
                foreach (var image in Directory.GetFiles(rootPath + Path.DirectorySeparatorChar + "people"+aux
                    + Path.DirectorySeparatorChar + people[i]))
                {
                    images.Add(CvInvoke.Imread(image, mode));
                    labels.Add(people[i]);
                    labels_dic.Add(labels_dic.Count, people[i]);
                }
            }

            return (images, labels.ToArray(), labels_dic);
        }

        public string[] GetLabels(string rootPath)
        {
            using (var stream = new StreamReader(rootPath + Path.DirectorySeparatorChar + "recognizers"
                + Path.DirectorySeparatorChar + "labels.xml"))
            {
                var serializer = new XmlSerializer(typeof(string[]));
                var labels = (string[])serializer.Deserialize(stream);
                return labels;
            }
        }
    }
}

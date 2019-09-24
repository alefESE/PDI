using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Emgu.CV;
using System.IO;
using System;

namespace PDI_Final
{
    class FaceDetector
    {
        private CascadeClassifier _classifier;
        private static readonly string _projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

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

        private static List<Mat> CutFaces(Mat image, Rectangle[] facesCoord)
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

        private static List<Mat> Resize(List<Mat> images, Size? size = null)
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

        private static List<Mat> NormalizeFaces(Mat image, Rectangle[] facesCoord)
        {
            var faces = CutFaces(image, facesCoord);
            faces = Resize(faces);

            return faces;
        }

        private static (List<Mat>, string[]) CollectDataset()
        {
            var images = new List<Mat>();
            var labels = new List<string>();

            var people = Directory.GetDirectories(_projectPath + Path.DirectorySeparatorChar + "people")
                .Select((value, index) => new { index, value })
                .ToDictionary(pair => pair.index, pair => Path.GetFileName(pair.value));

            foreach (var person in people)
            {
                foreach (var image in Directory.GetFiles(_projectPath + Path.DirectorySeparatorChar + "people"
                    + Path.DirectorySeparatorChar + person.Value))
                {
                    images.Add(CvInvoke.Imread(image));
                    labels.Add(person.Value);
                }
            }

            return (images, labels.ToArray());
        }

        public static void Run(string xmlPath)
        {
            (var images, var labels) = CollectDataset();

            if (!Directory.Exists(_projectPath + Path.DirectorySeparatorChar + "people_allign"))
                Directory.CreateDirectory(_projectPath + Path.DirectorySeparatorChar + "people_allign");

            for (int i = 0; i < images.Count; i++)
            {
                var detector = new FaceDetector(xmlPath);
                var faces_coord = detector.Detect(images[i]);
                var faces = NormalizeFaces(images[i], faces_coord);
                for (int j = 0; j < faces.Count; j++)
                {
                    if (!Directory.Exists(_projectPath + Path.DirectorySeparatorChar + "people_allign"
                        + Path.DirectorySeparatorChar + labels[i]))
                        Directory.CreateDirectory(_projectPath + Path.DirectorySeparatorChar + "people_allign"
                        + Path.DirectorySeparatorChar + labels[i]);

                    CvInvoke.Imwrite(_projectPath + Path.DirectorySeparatorChar + "people_allign"
                        + Path.DirectorySeparatorChar + labels[i] + Path.DirectorySeparatorChar
                        + labels[i] + (i + j) + ".jpg", faces[j]);
                }
                    
            }
        }
    }
}

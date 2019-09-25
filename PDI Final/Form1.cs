using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PDI_Final
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture;
        private Mat _frame;
        private Mat _gray;
        private static readonly string _projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        private FaceDetector _faceDetector;
        private Emgu.CV.Face.FaceRecognizer _recognizer;
        private string[] _labels;
        private List<Mat> _images;
        private Dictionary<int, string> _labels_dic;
        private uint tempCount = 0;

        public Form1()
        {
            InitializeComponent();
            _faceDetector = new FaceDetector(_projectPath);
            _recognizer = new Emgu.CV.Face.LBPHFaceRecognizer(1, 8, 8, 8, 2000);

            if (Directory.Exists(_projectPath + Path.DirectorySeparatorChar + "recognizers"))
                _recognizer.Read(_projectPath + Path.DirectorySeparatorChar + "recognizers"
                + Path.DirectorySeparatorChar + "face-trainner.yml");


            if (!Directory.Exists(_projectPath + Path.DirectorySeparatorChar + "temp"))
                Directory.CreateDirectory(_projectPath + Path.DirectorySeparatorChar + "temp");

            _capture = new VideoCapture(0);
            _capture.ImageGrabbed += ProcessFrame;
            _frame = new Mat();
            _gray = new Mat();
            if (_capture != null)
            {
                try
                {
                    _capture.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                if (_capture != null && _capture.Ptr != IntPtr.Zero)
                {
                    _capture.Retrieve(_frame, 0);
                    
                    CvInvoke.Imwrite(_projectPath + Path.DirectorySeparatorChar + "temp" 
                        + Path.DirectorySeparatorChar + tempCount + ".jpg", _frame);

                    pictureBox1.Image = _frame.Bitmap;
                    tempCount++;
                }
            }
            else if (checkBox1.Checked)
            {
                if (_capture != null && _capture.Ptr != IntPtr.Zero)
                {
                    _capture.Retrieve(_frame, 0);
                    CvInvoke.CvtColor(_frame, _gray, Emgu.CV.CvEnum.ColorConversion.Rgb2Gray);
                    var faces = _faceDetector.Detect(_gray);
                    foreach (var face in faces)
                    {
                        var w_rm = (int)(0.3 * face.Width / 2);
                        var roi = new Rectangle(
                                x: face.X + w_rm,
                                y: face.Y,
                                width: face.Width - w_rm,
                                height: face.Height
                                );
                        
                        var result = _recognizer.Predict(new Mat(_gray, roi));
                        _labels = _faceDetector.GetLabels(_projectPath);
                        var label = result.Distance <= 84 ? _labels[result.Label] : "Unknown";
                        var boxColor = result.Distance <= 84 ? new MCvScalar(0, 255, 0) : new MCvScalar(0, 0, 255);

                        CvInvoke.PutText(_frame, label, face.Location,
                               Emgu.CV.CvEnum.FontFace.HersheySimplex, 1, new MCvScalar(255, 255, 255),
                               2, Emgu.CV.CvEnum.LineType.AntiAlias);

                        CvInvoke.Rectangle(_frame, face, boxColor);
                    }
                    pictureBox1.Image = _frame.Bitmap;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            (_images, _labels, _labels_dic) = _faceDetector.CollectDataset(_projectPath);

            if (Directory.Exists(_projectPath + Path.DirectorySeparatorChar + "people_allign"))
                Directory.Delete(_projectPath + Path.DirectorySeparatorChar + "people_allign", true);

            Directory.CreateDirectory(_projectPath + Path.DirectorySeparatorChar + "people_allign");

            for (int i = 0; i < _images.Count; i++)
            {
                var faces_coord = _faceDetector.Detect(_images[i]);
                var faces = _faceDetector.NormalizeFaces(_images[i], faces_coord);
                for (int j = 0; j < faces.Count; j++)
                {
                    if (!Directory.Exists(_projectPath + Path.DirectorySeparatorChar + "people_allign"
                        + Path.DirectorySeparatorChar + _labels[i]))
                        Directory.CreateDirectory(_projectPath + Path.DirectorySeparatorChar + "people_allign"
                        + Path.DirectorySeparatorChar + _labels[i]);
                    
                    CvInvoke.Imwrite(_projectPath + Path.DirectorySeparatorChar + "people_allign"
                        + Path.DirectorySeparatorChar + _labels[i] + Path.DirectorySeparatorChar
                        + _labels[i] + (i + j) + ".jpg", faces[j]);
                }

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            (_images, _labels, _labels_dic) = _faceDetector.CollectDataset(_projectPath, train: true);
            
            _recognizer.Train(_images.ToArray(), _labels_dic.Keys.ToArray());
            if (!Directory.Exists(_projectPath + Path.DirectorySeparatorChar + "recognizers"))
                Directory.CreateDirectory(_projectPath + Path.DirectorySeparatorChar + "recognizers");

            _recognizer.Write(_projectPath + Path.DirectorySeparatorChar + "recognizers"
                + Path.DirectorySeparatorChar + "face-trainner.yml");

            using (var stream = File.Create(_projectPath + Path.DirectorySeparatorChar + "recognizers"
                + Path.DirectorySeparatorChar + "labels.xml"))
            {
                var serializer = new XmlSerializer(typeof(string[]));
                serializer.Serialize(stream, _labels);
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
                _capture.Stop();
            else
                _capture.Start();
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
            {
                checkBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                _capture.Stop();
                tempCount = 0;
            }
            else
            {
                checkBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                _capture.Start();
            }
        }
    }
}

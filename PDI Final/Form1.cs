using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDI_Final
{
    public partial class Form1 : Form
    {
        private VideoCapture _capture;
        private Mat _frame;
        private Mat _gray;
        private CascadeClassifier _faceClassifier;
        private readonly string _projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

        public Form1()
        {
            InitializeComponent();

            _faceClassifier = new CascadeClassifier(_projectPath + Path.DirectorySeparatorChar + "haarcascade_frontalface_default.xml");
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
            if (_capture != null && _capture.Ptr != IntPtr.Zero)
            {
                _capture.Retrieve(_frame, 0);
                CvInvoke.CvtColor(_frame, _gray, Emgu.CV.CvEnum.ColorConversion.Rgb2Gray);
                var faces = _faceClassifier.DetectMultiScale(_gray, 1.1, 10, Size.Empty); //the actual face detection happens here
                foreach (var face in faces)
                    CvInvoke.Rectangle(_frame, face, new Emgu.CV.Structure.MCvScalar(0, 255, 0), 1);
                pictureBox1.Image = _frame.Bitmap;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FaceDetector.Run(_projectPath + Path.DirectorySeparatorChar + "haarcascade_frontalface_default.xml");
        }
    }
}

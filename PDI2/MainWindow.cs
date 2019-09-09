using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDI2
{
    public partial class MainWindow : Form
    {
        private Bitmap bitmap { get; set; }
        private float[,] colorMatrix { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            pictureBox1.Paint += new PaintEventHandler(RGB_Paint);
        }

        /// <summary>
        /// Close program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SairToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        /// <summary>
        /// Callback of the button "imagem..."
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event args</param>
        private void ImagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                bitmap = new Bitmap(openFileDialogImage.FileName);
                colorMatrix = GetColorMatrixFromBitmap(bitmap);

                // Collect trash
                GC.Collect();
                GC.WaitForPendingFinalizers();

                pictureBox1.Invalidate();
            }
        }

        private float[,] GetColorMatrixFromBitmap(Bitmap bitmap)
        {
            BitmapData imageData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format24bppRgb
                    );

            int stride = imageData.Stride;
            int width = imageData.Width;
            int height = imageData.Height;
            int size = Math.Abs(stride) * height;

            byte[] imageBytes = new byte[size];
            IntPtr scan0 = imageData.Scan0;

            Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);

            bitmap.UnlockBits(imageData);
            
            float[,] colorMatrix = new float[width, height];
            int idx = 0;
            Parallel.For(0, width, i =>
            {
                Parallel.For(0, height, j =>
                {
                    colorMatrix[i, j] = (imageBytes[idx] // B
                    + imageBytes[idx+1] // G
                    + imageBytes[idx+2] // R
                    ) / 3; //gray
                    idx += 3;
                });
            });

            imageBytes = null;

            return colorMatrix;
        }

        private void RGB_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(
                                        bitmap,
                                        new Rectangle(new Point(0, 0), pictureBox1.Size),
                                        0, 0,
                                        bitmap.Width, bitmap.Height,
                                        GraphicsUnit.Pixel);
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private float[] GetAudioVector()
        {
            float[] audio = new float[1];

            return audio;
        }

        private void AudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();

            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                waveControl.Filename = fileDlg.FileName;

                waveControl.Read();
            }
        }
    }
}

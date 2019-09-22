using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using NWaves.Audio;
using NWaves.Signals;
using System.Linq;
using Emgu.CV;

namespace PDI2
{
    public partial class MainWindow : Form
    {
        private Bitmap _bitmap;
        private float[,] _colorMatrix;
        private float[,] _transformMatrix;
        
        private SoundPlayer _player;
        private WaveFile _waveFile;
        private WaveFile _transformFile;
        private DiscreteSignal _originalWave;
        private DiscreteSignal _transformWave;

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
            openFileDialog.Filter = "Imagem (*jpeg, *.png, *.bmp) | *jpeg; *.png; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _bitmap = new Bitmap(openFileDialog.FileName);
                _colorMatrix = GetColorMatrixFromBitmap(_bitmap);

                // Collect trash
                GC.Collect();
                GC.WaitForPendingFinalizers();

                pictureBox1.Invalidate();

                btnDCTImage.Enabled = true;
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
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    colorMatrix[i, j] = (imageBytes[idx] // B
                    + imageBytes[idx + 1] // G
                    + imageBytes[idx + 2] // R
                    ) / 3; //gray
                    idx += 3;
                }
                idx += stride - (width * 3);
            }
            return colorMatrix;
        }

        private void RGB_Paint(object sender, PaintEventArgs e)
        {
            
            try
            {
                e.Graphics.DrawImage(_bitmap,
                    new Rectangle(new Point(0, 0), pictureBox1.Size),
                    0, 0,
                    _bitmap.Width, _bitmap.Height,
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

        private void AudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Áudio (*.wav) | *.wav";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var stream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    _waveFile = new WaveFile(stream, normalized: false);
                    _originalWave = _waveFile[Channels.Sum];

                    originalAudioChart.Series["Frequencies"].Points.Clear();
                    for (int x = 0; x < _originalWave.Length; x++)
                        originalAudioChart.Series["Frequencies"].Points.AddXY(x, _originalWave[x]);
                    originalAudioChart.Titles["Title"].Text = "N: " + _originalWave.Length;


                    btnPlay.Enabled = true;
                    btnDCTAudio.Enabled = true;
                    btnCompress.Enabled = true;
                    numericNCompress.Enabled = true;
                }
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            var stream = new MemoryStream();
            var wave = new WaveFile(_originalWave, _waveFile.WaveFmt.BitsPerSample);
            wave.SaveTo(stream, normalized: false);

            stream = new MemoryStream(stream.ToArray());

            _player?.Dispose();
            _player = new SoundPlayer(stream);
            _player.Stream.Seek(0, SeekOrigin.Begin);
            _player.Play();
        }

        private void BtnDCTAudio_Click(object sender, EventArgs e)
        {
            if(checkBoxOptimum.Checked)
            {
                var waveArray = new Emgu.CV.Util.VectorOfFloat(_originalWave.Samples);
                var copyArray = new Emgu.CV.Util.VectorOfFloat(_originalWave.Length);
                CvInvoke.Dct(waveArray, copyArray, Emgu.CV.CvEnum.DctType.Forward);
                float[] copy = copyArray.ToArray();
                _transformWave = new DiscreteSignal(_originalWave.SamplingRate, copy, allocateNew: true);
                _transformFile = new WaveFile(_transformWave);

                transformAudioChart.Series["Frequencies"].Points.Clear();
                for (int x = 0; x < _transformWave.Length; x++)
                    transformAudioChart.Series["Frequencies"].Points.AddXY(x, _transformWave[x]);
                transformAudioChart.Titles["Title"].Text = "N: " + _transformWave.Length + "; DC: " + _transformWave[0];
            }
            else
            {
                _transformWave = new DiscreteSignal(_originalWave.SamplingRate, Transform.DCT(_originalWave.Samples), allocateNew: true);
                _transformFile = new WaveFile(_transformWave);

                transformAudioChart.Series["Frequencies"].Points.Clear();
                for (int x = 0; x < _transformWave.Length; x++)
                    transformAudioChart.Series["Frequencies"].Points.AddXY(x, _transformWave[x]);
                transformAudioChart.Titles["Title"].Text = "N: " + _transformWave.Length + "; DC: " + _transformWave[0];
            }

            btnIDCTAudio.Enabled = true;
            btnDCTAudio.Enabled = false;
            btnNFilter.Enabled = true;
            btnPlayIdct.Enabled = false;
            btnCompress.Enabled = true;
            numericN.Enabled = true;
            numericN.Maximum = _transformWave.Length;
            numericNCompress.Enabled = true;
            numericNCompress.Maximum = 100;
        }

        private void BtnIDCTAudio_Click(object sender, EventArgs e)
        {
            if(checkBoxOptimum.Checked)
            {
                var waveArray = new Emgu.CV.Util.VectorOfFloat(_transformWave.Samples);
                var copyArray = new Emgu.CV.Util.VectorOfFloat(_transformWave.Length);
                CvInvoke.Dct(waveArray, copyArray, Emgu.CV.CvEnum.DctType.Inverse);
                float[] copy = copyArray.ToArray();
                _transformWave = new DiscreteSignal(_originalWave.SamplingRate, copy, allocateNew: true);
                _transformFile = new WaveFile(_transformWave);

                transformAudioChart.Series["Frequencies"].Points.Clear();
                for (int x = 0; x < _transformWave.Length; x++)
                    transformAudioChart.Series["Frequencies"].Points.AddXY(x, _transformWave[x]);
                transformAudioChart.Titles["Title"].Text = "N: " + _transformWave.Length;
            }
            else
            {
                _transformWave = new DiscreteSignal(_originalWave.SamplingRate, Transform.IDCT(_transformWave.Samples), allocateNew: true);
                _transformFile = new WaveFile(_transformWave);

                transformAudioChart.Series["Frequencies"].Points.Clear();
                for (int x = 0; x < _transformWave.Length; x++)
                    transformAudioChart.Series["Frequencies"].Points.AddXY(x, _transformWave[x]);
                transformAudioChart.Titles["Title"].Text = "N: " + _transformWave.Length;
            }
            

            btnIDCTAudio.Enabled = false;
            btnDCTAudio.Enabled = true;
            btnNFilter.Enabled = false;
            numericN.Enabled = false;
            btnPlayIdct.Enabled = true;
            btnCompress.Enabled = false;
            numericN.Enabled = false;
            numericN.Maximum = 0;
            numericNCompress.Enabled = false;
            numericNCompress.Maximum = 0;
        }

        private void BtnNFilter_Click(object sender, EventArgs e)
        {
            int n = Decimal.ToInt32(numericN.Value);

            int[] index = Enumerable.Range(0, _transformWave.Length).ToArray();
            Array.Sort(index, (a, b) => Math.Abs(_transformWave.Samples[b]).CompareTo(Math.Abs(_transformWave.Samples[a])));

            for (int i = n; i < _transformWave.Length; i++)
                _transformWave[index[i]] = 0f;

            _transformFile = new WaveFile(_transformWave);

            transformAudioChart.Series["Frequencies"].Points.Clear();
            for (int x = 0; x < _transformWave.Length; x++)
                transformAudioChart.Series["Frequencies"].Points.AddXY(x, _transformWave[x]);
            transformAudioChart.Titles["Title"].Text = "N: " + _transformWave.Length + "; DC: " + _transformWave[0];
        }

        private void BtnPlayIdct_Click(object sender, EventArgs e)
        {
            var stream = new MemoryStream();
            var wave = new WaveFile(_transformWave, _transformFile.WaveFmt.BitsPerSample);
            wave.SaveTo(stream, normalized: false);

            stream = new MemoryStream(stream.ToArray());

            _player?.Dispose();
            _player = new SoundPlayer(stream);
            _player.Stream.Seek(0, SeekOrigin.Begin);
            _player.Play();
        }

        private void BtnDCTImage_Click(object sender, EventArgs e)
        {
            if(checkBoxOptimum.Checked)
            {
                var imageArray = new Matrix<float>(_colorMatrix);
                var copyArray = new Matrix<float>(_colorMatrix.GetLength(0), _colorMatrix.GetLength(1));
                CvInvoke.Dct(imageArray, copyArray, Emgu.CV.CvEnum.DctType.Forward);
                _transformMatrix = copyArray.Data;
                dctPictureBox.Image = DCTPlotGenerate(_transformMatrix);
                dctPictureBox.Invalidate();
            }
            else
            {
                _transformMatrix = Transform.DCT(_colorMatrix);
                dctPictureBox.Image = DCTPlotGenerate(_transformMatrix);
            }

            btnDCTImage.Enabled = false;
            btnIDCTImage.Enabled = true;
            btnFilterImage.Enabled = true;
            numericNImage.Enabled = true;
            numericNImage.Maximum = _transformMatrix.Length;
        }

        private void BtnIDCTImage_Click(object sender, EventArgs e)
        {
            if (checkBoxOptimum.Checked)
            {
                var imageArray = new Matrix<float>(_transformMatrix);
                var copyArray = new Matrix<float>(_transformMatrix.GetLength(0), _transformMatrix.GetLength(1));
                CvInvoke.Dct(imageArray, copyArray, Emgu.CV.CvEnum.DctType.Inverse);
                _transformMatrix = copyArray.Data;
                dctPictureBox.Image = Displayimage(_transformMatrix);
                dctPictureBox.Invalidate();
            }
            else
            {
                _transformMatrix = Transform.IDCT(_transformMatrix);
                dctPictureBox.Image = Displayimage(_transformMatrix);
            }

            btnDCTImage.Enabled = true;
            btnIDCTImage.Enabled = false;
            btnFilterImage.Enabled = false;
            numericNImage.Enabled = false;
            numericNImage.Maximum = 0;
        }

        public Bitmap Displayimage(float[,] image)
        {
            int i, j;
            Bitmap output = new Bitmap(image.GetLength(0), image.GetLength(1));
            BitmapData bitmapData1 = output.LockBits(new Rectangle(0, 0, image.GetLength(0), image.GetLength(1)),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        imagePointer1[0] = (byte)image[j, i];
                        imagePointer1[1] = (byte)image[j, i];
                        imagePointer1[2] = (byte)image[j, i];
                        imagePointer1[3] = 255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//end for i
            }//end unsafe
            output.UnlockBits(bitmapData1);
            return output;// col;

        }

        private Bitmap DCTPlotGenerate(float[,] DCTCoefficients)
        {
            int i, j;
            int width = DCTCoefficients.GetLength(0);
            int height = DCTCoefficients.GetLength(1);
            int[,] temp = new int[width, height];
            double[,] DCTLog = new double[width, height];

            // Compressing Range By taking Log    
            for (i = 0; i <= width - 1; i++)
                for (j = 0; j <= height - 1; j++)
                {
                    DCTLog[i, j] = Math.Log(1 + Math.Abs((int)DCTCoefficients[i, j]));

                }

            //Normalizing Array
            double min, max;
            min = max = DCTLog[1, 1];

            for (i = 1; i <= width - 1; i++)
                for (j = 1; j <= height - 1; j++)
                {
                    if (DCTLog[i, j] > max)
                        max = DCTLog[i, j];
                    if (DCTLog[i, j] < min)
                        min = DCTLog[i, j];
                }
            for (i = 0; i <= width - 1; i++)
                for (j = 0; j <= height - 1; j++)
                {
                    temp[i, j] = (int)(((float)(DCTLog[i, j] - min) / (float)(max - min)) * 750);
                }

            return Displaymap(temp);
        }

        public Bitmap Displaymap(int[,] output)
        {
            int i, j;
            Bitmap image = new Bitmap(output.GetLength(0), output.GetLength(1));
            BitmapData bitmapData1 = image.LockBits(new Rectangle(0, 0, output.GetLength(0), output.GetLength(1)),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        if (output[j, i] < 0)
                        {
                            // Changing to Red Color
                            // Changing to Green Color
                            imagePointer1[0] = 0; //(byte)output[j, i];
                            imagePointer1[1] = 255;
                            imagePointer1[2] = 0; //(byte)output[j, i];
                        }
                        else if ((output[j, i] >= 0) && (output[j, i] < 50))
                        {   // Changing to Green Color
                            imagePointer1[0] = (byte)((output[j, i]) * 4);  //(byte)output[j, i];
                            imagePointer1[1] = 0;
                            imagePointer1[2] = 0;// 0; //(byte)output[j, i];
                        }
                        else if ((output[j, i] >= 50) && (output[j, i] < 100))
                        {
                            imagePointer1[0] = 0;//(byte)(-output[j, i]);
                            imagePointer1[1] = (byte)(output[j, i] * 2);// (byte)(output[j, i]);
                            imagePointer1[2] = (byte)(output[j, i] * 2);
                        }
                        else if ((output[j, i] >= 100) && (output[j, i] < 255))
                        {   // Changing to Green Color
                            imagePointer1[0] = 0; //(byte)output[j, i];
                            imagePointer1[1] = (byte)(output[j, i]);// (byte)(output[j, i]);
                            imagePointer1[2] = 0;  //(byte)output[j, i];
                        }
                        else if ((output[j, i] > 255))
                        {   // Changing to Green Color
                            imagePointer1[0] = 0;  //(byte)output[j, i];
                            imagePointer1[1] = 0; //(byte)(output[j, i]);
                            imagePointer1[2] = (byte)((output[j, i]) * 0.7);
                        }
                        imagePointer1[3] = 255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//end for i
            }//end unsafe
            image.UnlockBits(bitmapData1);
            return image;// col;

        }

        private void BtnFilterImage_Click(object sender, EventArgs e)
        {
            int n = Decimal.ToInt32(numericNImage.Value);
            int rows = _transformMatrix.GetLength(0);
            int cols = _transformMatrix.GetLength(1);
            var array1d = new float[rows * cols];
            var current = 0;
            foreach (var value in _transformMatrix)
                array1d[current++] = value;

            int[] index = Enumerable.Range(0, array1d.Length).ToArray();
            Array.Sort(index, (a, b) => Math.Abs(array1d[b]).CompareTo(Math.Abs(array1d[a])));

            for (int i = n; i < array1d.Length; i++)
                array1d[index[i]] = 0f;

            var ret = new float[rows, cols];
            Buffer.BlockCopy(array1d, 0, ret, 0, array1d.Length * sizeof(float));

            _transformMatrix = ret;
            pictureBox1.Image = DCTPlotGenerate(_transformMatrix);
        }

        private void BtnCompress_Click(object sender, EventArgs e)
        {
            //if (_transformWave == null)
            //{
            //    _transformFile = _waveFile;
            //    _transformWave = _originalWave;
            //}
                
            float c = Decimal.ToSingle(numericNCompress.Value);
            float[] clone = new float[_transformWave.Samples.Length];
            Buffer.BlockCopy(_transformWave.Samples, 0, clone, 0, _transformWave.Samples.Length);
            for (int i = 0; i < _transformWave.Length; i++)
            {
                if ((i * c) >= _transformWave.Length)
                    clone[i] = 0;
                else
                    clone[(int)Math.Round(i * c)] = _transformWave.Samples[i];
            }

            _transformWave = new DiscreteSignal(_transformFile.WaveFmt.SamplingRate, clone, allocateNew: true);
            _transformFile = new WaveFile(_transformWave);

            transformAudioChart.Series["Frequencies"].Points.Clear();
            for (int x = 0; x < _transformWave.Length; x++)
                transformAudioChart.Series["Frequencies"].Points.AddXY(x, _transformWave[x]);
            transformAudioChart.Titles["Title"].Text = "N: " + _transformWave.Length + "; DC: " + _transformWave[0];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDI1
{
    public partial class MainWindow : Form
    {
        private Bitmap bitmap;
        private float[] imagePixels;
        private float[][] rMask, gMask, bMask, brightCM;
        private bool isYIQ = false;
        private bool isNeg = false;
        private bool act = false;

        public Bitmap Bitmap { get => bitmap; set => bitmap = value; }

        //private float factorA = 1;
        //private float factorB = 1;

        public MainWindow()
        {
            InitializeComponent();
            pictureBoxRGB.Paint += new PaintEventHandler(RGB_Paint);
            pictureBoxR.Paint += new PaintEventHandler(R_Paint);
            pictureBoxG.Paint += new PaintEventHandler(G_Paint);
            pictureBoxB.Paint += new PaintEventHandler(B_Paint);

            dataGridView.DataSource = bindingSource;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            InitializeMasks();
        }

        public void InitializeMasks()
        {
            rMask = new float[5][]{
                new float[] {1, 0,  0,  0,  0},     // Unchange Red color
                new float[] {0, 0,  0,  0,  0},     // Remove Green color
                new float[] {0, 0,  0,  0,  0},     // Remove Blue color
                new float[] {0, 0,  0,  1,  0},     // Unchange Alpha channel
                new float[] {0, 0,  0,  0,  1}};    // Unchange W channel

            gMask = new float[5][]{
                new float[] {0, 0,  0,  0,  0},     // Remove Red color
                new float[] {0, 1,  0,  0,  0},     // Unchange Green color
                new float[] {0, 0,  0,  0,  0},     // Remove Blue color
                new float[] {0, 0,  0,  1,  0},     // Unchange Alpha channel
                new float[] {0, 0,  0,  0,  1}};    // Unchange W channel

            bMask = new float[5][]{
                new float[] {0, 0,  0,  0,  0},     // Remove Red color
                new float[] {0, 0,  0,  0,  0},     // Remove Green color
                new float[] {0, 0,  1,  0,  0},     // Unchange Blue color
                new float[] {0, 0,  0,  1,  0},     // Unchange Alpha channel
                new float[] {0, 0,  0,  0,  1}};    // Unchange W channel

            brightCM = new float[5][]{
                new float[]{1, 0, 0, 0, 0 },
                new float[]{0, 1, 0, 0, 0 },
                new float[]{0, 0, 1, 0, 0 },
                new float[]{0, 0, 0, 1, 0 },
                new float[]{0, 0, 0, 0, 1 }
                };
        }

        /// <summary>
        /// Callback of the button "sair"
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event args</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        /// <summary>
        /// Callback of the button "imagem..."
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event args</param>
        private void ImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetState();
            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                Bitmap = new Bitmap(openFileDialogImage.FileName);
                imagePixels = GetPixelsFromBitmap(Bitmap);

                // Collect trash
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Call paint functions
                pictureBoxRGB.Invalidate();
                pictureBoxR.Invalidate();
                pictureBoxG.Invalidate();
                pictureBoxB.Invalidate();
            }
        }

        private void ResetState()
        {
            checkBoxNegative.Checked = checkBoxYIQ.Checked
                = isNeg = isYIQ = false;
            act = true;
        }

        /// <summary>
        /// Callback of the button "Filtro..."
        /// </summary>
        private void FilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogFilter.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialogFilter.FileName))
                {
                    string line = reader.ReadLine();

                    if (!line.Contains('x')) throw new FormatException("Incorrect format archive");

                    string[] values = line.ToLower().Split('x');

                    DataTable table = new DataTable();
                    int width = int.Parse(values[1]);
                    int height = int.Parse(values[0]);

                    for (int i = 0; i < width; i++)
                        table.Columns.Add(i.ToString(), typeof(float));

                    float[] tableRow = new float[width];
                    DataRow newRow = null;
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        values = line.Split(',');

                        for (int i = 0; i < width; i++)
                            tableRow[i] = float.Parse(values[i], CultureInfo.InvariantCulture);

                        newRow = table.NewRow();
                        newRow.ItemArray = tableRow.Cast<object>().ToArray();
                        table.Rows.Add(newRow);
                    }

                    bindingSource.DataSource = table;
                }
            }
        }

        /// <summary>
        /// Callback of the button "Aplicar"
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event args</param>
        private void ApplyButton_Click(object sender, EventArgs e)
        {

            //factorB = trackBarBrightness.Value / 10.0f;

            brightCM = GetBrightness(trackBarBrightness.Value / 10.0f);

            act = true;

            ProcessImage();

            pictureBoxRGB.Invalidate();
            pictureBoxR.Invalidate();
            pictureBoxG.Invalidate();
            pictureBoxB.Invalidate();
        }

        private void ProcessImage()
        {
            if (!act)
            {
                act = true;
                return;
            }
            else
            {
                try
                {
                    int width = Bitmap.Width;
                    int height = Bitmap.Height;

                    if (checkBoxYIQ.Checked && checkBoxNegative.Checked)
                    {
                        if (!isYIQ && !isNeg)
                        {
                            RGB2YIQ(imagePixels);
                            NegativeYIQ(imagePixels);
                            YIQ2RGB(imagePixels);
                            isNeg = true;
                            isYIQ = true;
                        }
                        else if (!isYIQ && isNeg)
                        {
                            Negative(imagePixels);
                            RGB2YIQ(imagePixels);
                            NegativeYIQ(imagePixels);
                            YIQ2RGB(imagePixels);
                            isNeg = true;
                            isYIQ = true;
                        }
                        else if (isYIQ && !isNeg)
                        {
                            NegativeYIQ(imagePixels);
                            YIQ2RGB(imagePixels);
                            isNeg = true;
                        }
                        else
                        {
                            //Nothing
                        }
                    }
                    else if (!checkBoxYIQ.Checked && checkBoxNegative.Checked)
                    {
                        if (!isYIQ && !isNeg)
                        {
                            Negative(imagePixels);
                            isNeg = true;
                        }
                        else if (!isYIQ && isNeg)
                        {
                            // Nothing
                        }
                        else if (isYIQ && !isNeg)
                        {
                            NegativeYIQ(imagePixels);
                            YIQ2RGB(imagePixels);
                            isNeg = true;
                        }
                        else
                        {
                            RGB2YIQ(imagePixels);
                            NegativeYIQ(imagePixels);
                            YIQ2RGB(imagePixels);
                            Negative(imagePixels);
                            isYIQ = false;
                        }
                    }
                    else if (checkBoxYIQ.Checked && !checkBoxNegative.Checked)
                    {
                        if (!isYIQ && !isNeg)
                        {
                            RGB2YIQ(imagePixels);
                            isYIQ = true;
                        }
                        else if (!isYIQ && isNeg)
                        {
                            Negative(imagePixels);
                            RGB2YIQ(imagePixels);
                            isNeg = false;
                            isYIQ = true;
                        }
                        else if (isYIQ && !isNeg)
                        {
                            // Nothing
                        }
                        else
                        {
                            RGB2YIQ(imagePixels);
                            NegativeYIQ(imagePixels);
                            isNeg = false;
                        }
                    }
                    else
                    {
                        if (!isYIQ && !isNeg)
                        {
                            // Nothing
                        }
                        else if (!isYIQ && isNeg)
                        {
                            Negative(imagePixels);
                            isNeg = false;
                        }
                        else if (isYIQ && !isNeg)
                        {
                            YIQ2RGB(imagePixels);
                            isYIQ = false;
                        }
                        else
                        {
                            RGB2YIQ(imagePixels);
                            NegativeYIQ(imagePixels);
                            YIQ2RGB(imagePixels);
                            isNeg = false;
                            isYIQ = false;
                        }
                    }

                    //if (factorB > factorA)
                    //{
                    //    BrightMultiply(imagePixels, factorB);
                    //    factorA = factorB;
                    //}
                    //else if(factorB < factorA)
                    //{
                    //    BrightMultiply(imagePixels, (factorA - factorB)/factorA);
                    //    factorA = factorB;
                    //}

                    Bitmap = GetBitmapFromPixels(imagePixels);
                }
                catch (ArgumentNullException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    act = false;
                }
            }
        }

        //private void BrightMultiply(float[] imagePixels, float factor)
        //{
        //    Parallel.For(0, imagePixels.Length / 5, i => 
        //    {
        //        int idx = i * 5;

        //        imagePixels[idx] *= factor;
        //        imagePixels[idx + 1] *= factor;
        //        imagePixels[idx + 2] *= factor;
        //    });
        //}

        //private void BrightDivision(float[] imagePixels, float factor)
        //{
        //    Parallel.For(0, imagePixels.Length / 5, i =>
        //    {
        //        int idx = i * 5;

        //        imagePixels[idx] /= factor;
        //        imagePixels[idx + 1] /= factor;
        //        imagePixels[idx + 2] /= factor;
        //    });
        //}

        /// <summary>
        /// Callback of the button "Sobre"
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event args</param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox aboutBox = new AboutBox())
                aboutBox.ShowDialog();
        }

        /// <summary>
        /// Callback of the pictureBoxRGB.Paint
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event args</param>
        private void RGB_Paint(object sender, PaintEventArgs e)
        {
            using (ImageAttributes attr = new ImageAttributes())
            {
                try
                {
                    int width = Bitmap.Width;
                    int height = Bitmap.Height;

                    attr.SetColorMatrix(new ColorMatrix(brightCM));

                    e.Graphics.DrawImage(
                        Bitmap,
                        new Rectangle(new Point(0, 0), pictureBoxRGB.Size),
                        0, 0,
                        width, height,
                        GraphicsUnit.Pixel,
                        attr);
                }
                catch (ArgumentNullException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Callback of the pictureBoxB.Paint with Green channel mask
        /// [1, 0, 0, 0, 0]
        /// [0, 0, 0, 0, 0]
        /// [0, 0, 0, 0, 0]
        /// [0, 0, 0, 1, 0]
        /// [0, 0, 0, 0, 1]
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event args</param>
        private void R_Paint(object sender, PaintEventArgs e)
        {
            using (ImageAttributes attr = new ImageAttributes())
            {
                try
                {
                    int width = Bitmap.Width;
                    int height = Bitmap.Height;

                    //attr.SetColorMatrix(new ColorMatrix(rMask)));
                    attr.SetColorMatrix(new ColorMatrix(MultiplyMatrix(rMask, brightCM)));

                    e.Graphics.DrawImage(
                        Bitmap,
                        new Rectangle(new Point(0, 0), pictureBoxR.Size),
                        0, 0,
                        width, height,
                        GraphicsUnit.Pixel,
                        attr);
                }
                catch (ArgumentNullException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Callback of the pictureBoxB.Paint with Green channel mask
        /// [0, 0, 0, 0, 0]
        /// [0, 1, 0, 0, 0]
        /// [0, 0, 0, 0, 0]
        /// [0, 0, 0, 1, 0]
        /// [0, 0, 0, 0, 1]
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event args</param>
        private void G_Paint(object sender, PaintEventArgs e)
        {
            using (ImageAttributes attr = new ImageAttributes())
            {
                try
                {
                    int width = Bitmap.Width;
                    int height = Bitmap.Height;

                    //attr.SetColorMatrix(new ColorMatrix(gMask)));
                    attr.SetColorMatrix(new ColorMatrix(MultiplyMatrix(gMask, brightCM)));

                    e.Graphics.DrawImage(
                        Bitmap,
                        new Rectangle(new Point(0, 0), pictureBoxG.Size),
                        0, 0,
                        width, height,
                        GraphicsUnit.Pixel,
                        attr);
                }
                catch (ArgumentNullException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Callback of the pictureBoxB.Paint with Blue channel mask
        /// [0, 0, 0, 0, 0]
        /// [0, 0, 0, 0, 0]
        /// [0, 0, 1, 0, 0]
        /// [0, 0, 0, 1, 0]
        /// [0, 0, 0, 0, 1]
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="e">Event args</param>
        private void B_Paint(object sender, PaintEventArgs e)
        {
            using (ImageAttributes attr = new ImageAttributes())
            {
                try
                {
                    int width = Bitmap.Width;
                    int height = Bitmap.Height;

                    //attr.SetColorMatrix(new ColorMatrix(bMask));
                    attr.SetColorMatrix(new ColorMatrix(MultiplyMatrix(bMask, brightCM)));

                    e.Graphics.DrawImage(
                        Bitmap,
                        new Rectangle(new Point(0, 0), pictureBoxB.Size),
                        0, 0,
                        width, height,
                        GraphicsUnit.Pixel,
                        attr);
                }
                catch (ArgumentNullException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Convert <paramref name="bitmapImage"/> to YIQ and return the matrix of pixels
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns>Matrix with YIQ pixels</returns>
        public void RGB2YIQ(float[] rgbPixels)
        {
            Parallel.For(0, rgbPixels.Length / 5, i =>
            {
                int idx = i * 5;
                float R = rgbPixels[idx];
                float G = rgbPixels[idx + 1];
                float B = rgbPixels[idx + 2];

                rgbPixels[idx] = (float)Math.Round((.299f * R) + (.587f * G) + (.114f * B), 2); // Y
                rgbPixels[idx + 1] = (float)Math.Round((.596f * R) + (-.274f * G) + (-.322f * B), 2); // I
                rgbPixels[idx + 2] = (float)Math.Round((.211f * R) + (-.523f * G) + (.312f * B), 2); // Q

            });
            isYIQ = true;
        }

        /// <summary>
        /// Convert <paramref name="bitmapImage"/> to RGB and return the matrix of pixels
        /// </summary>
        /// <param name="bitmapImage"></param>
        /// <returns>Matrix with RGB pixels</returns>
        public void YIQ2RGB(float[] yiqPixels)
        {
            Parallel.For(0, yiqPixels.Length / 5, (Action<int>)(i =>
            {
                int idx = i * 5;
                float Y = yiqPixels[idx];
                float I = yiqPixels[idx + 1];
                float Q = yiqPixels[idx + 2];

                yiqPixels[idx] = (float)Math.Round((1f * Y) + (.956f * I) + (.621f * Q)); // R
                yiqPixels[idx + 1] = (float)Math.Round((1f * Y) + (-.272f * I) + (-.647f * Q)); // G
                yiqPixels[idx + 2] = (float)Math.Round((1f * Y) + (-1.106f * I) + (1.703f * Q)); // B

            }));
        }

        public void Negative(float[] imagePixels)
        {
            Parallel.For(0, imagePixels.Length / 5, i =>
            {
                int idx = i * 5;

                float R = imagePixels[idx];
                float G = imagePixels[idx + 1];
                float B = imagePixels[idx + 2];

                imagePixels[idx] = 255 - R;
                imagePixels[idx + 1] = 255 - G;
                imagePixels[idx + 2] = 255 - B;
                imagePixels[idx + 3] = imagePixels[idx + 3];
                imagePixels[idx + 4] = imagePixels[idx + 4];
            });
        }

        public void NegativeYIQ(float[] imagePixels)
        {
            Parallel.For(0, imagePixels.Length / 5, i =>
            {
                int idx = i * 5;

                float Y = imagePixels[idx];

                imagePixels[idx] = 255 - Y;
                imagePixels[idx + 3] = imagePixels[idx + 3];
                imagePixels[idx + 4] = imagePixels[idx + 4];
            });
        }

        /// <summary>
        /// Multiply the identity color matrix by <paramref name="factor"/>
        /// </summary>
        /// <param name="factor">Factor to multiply</param>
        public float[][] GetBrightness(float factor)
        {
            return new float[5][]{
                new float[]{factor, 0, 0, 0, 0 },
                new float[]{0, factor, 0, 0, 0 },
                new float[]{0, 0, factor, 0, 0 },
                new float[]{0, 0, 0, 1, 0 },
                new float[]{0, 0, 0, 0, 1 }
                };
        }

        /// <summary>
        /// Multiply <paramref name="m1"/> and <paramref name="m2"/> identities and return the 
        /// </summary>
        /// <param name="m1">Matrix operand</param>
        /// <param name="m2">Matrix operand</param>
        /// <returns></returns>
        public float[][] MultiplyMatrix(float[][] m1, float[][] m2)
        {
            // If N != M
            if (m2.Length != m1[0].Length)
                throw new ArgumentException("The matrices aren't multiplyable");

            // The result matrix has to be with M1 rows
            float[][] res = new float[m1.Length][];

            // Append m2 columns times float arrays
            for (int i = 0; i < m1.Length; i++)
                res[i] = new float[m2[0].Length];

            int rows = res.Length;
            int cols = res[0].Length;

            for (int m = 0; m < rows; m++)
                Parallel.For(0, cols, (k) =>
                {
                    // Matrix product size: MxN * NxK = MxK
                    for (int n = 0; n < cols; n++)
                        res[m][k] += // Need to reduce the precision
                        (float)Math.Round((Decimal)(m1[m][n] * m2[n][k]), 4);
                    //m1[m][n] * m2[n][k];

                });

            return res;
        }

        private void ApplyFilterButton_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)bindingSource.DataSource;
            imagePixels = Convolve(imagePixels, Bitmap.Width, Bitmap.Height, ConvertDataTableToMatrix(dt));
            Bitmap = GetBitmapFromPixels(imagePixels);

            pictureBoxRGB.Invalidate();
            pictureBoxR.Invalidate();
            pictureBoxG.Invalidate();
            pictureBoxB.Invalidate();
        }

        public float[] GetPixelsFromBitmap(Bitmap bitmap)
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

            float[] imagePixels = new float[width * height * 5];

            Parallel.For(0, imagePixels.Length / 5, i =>
            {
                int idb = i * 3;
                int idp = i * 5;
                imagePixels[idp] = imageBytes[idb + 2]; // R
                imagePixels[idp + 1] = imageBytes[idb + 1]; // G
                imagePixels[idp + 2] = imageBytes[idb]; // B
                imagePixels[idp + 3] = 1; // A
                imagePixels[idp + 4] = 1; // W
            });

            imageBytes = null;

            return imagePixels;
        }

        private void ApplyMedianButton_Click(object sender, EventArgs e)
        {
            int size = (int)(numericUpDownM.Value * numericUpDownN.Value);
            Bitmap = MedianFilter(Bitmap, size);
            imagePixels = GetPixelsFromBitmap(Bitmap);

            pictureBoxRGB.Invalidate();
            pictureBoxR.Invalidate();
            pictureBoxG.Invalidate();
            pictureBoxB.Invalidate();
        }

        public Bitmap GetBitmapFromPixels(float[] imagePixels)
        {
            int size = imagePixels.Length / 5;
            // 3bpp
            Bitmap bmp = new Bitmap(Bitmap.Width, Bitmap.Height);

            var data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);

            IntPtr ptr = data.Scan0;
            byte[] bytes = new byte[data.Stride * data.Height];

            Parallel.For(0, size, i =>
            {
                int idb = i * 3;
                int idp = i * 5;

                float R = imagePixels[idp];
                float G = imagePixels[idp + 1];
                float B = imagePixels[idp + 2];

                bytes[idb + 2] = (byte)((R < 0) ? 0 : ((R > 255) ? 255 : R)); // R
                bytes[idb + 1] = (byte)((G < 0) ? 0 : ((G > 255) ? 255 : G)); // G
                bytes[idb] = (byte)((B < 0) ? 0 : ((B > 255) ? 255 : B)); // B
            });

            Marshal.Copy(bytes, 0, ptr, bytes.Length);

            bmp.UnlockBits(data);

            return bmp;
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            toolTipBrightness.SetToolTip(trackBarBrightness, (trackBarBrightness.Value / 10.0f).ToString());
        }

        private void ResetFilterButton_Click(object sender, EventArgs e)
        {
            bindingSource.DataSource = null;
        }

        private float[][] ConvertDataTableToMatrix(DataTable dt)
        {
            float[][] matrix = new float[dt.Rows.Count][];
            Converter<object, float> converter = Convert.ToSingle;

            Parallel.For(0, dt.Rows.Count, i =>
            {
                matrix[i] = Array.ConvertAll(dt.Rows[i].ItemArray, converter);
            });

            return matrix;
        }

        private float[] Convolve(float[] imagePixels, int width, int height, float[][] mask)
        {
            ///
            /// ------------> 
            /// |           x
            /// |
            /// |
            /// |
            /// |
            /// v y
            ///

            float[] convImage = new float[imagePixels.Length];


            float red, green, blue;

            int maskOffsetX = (mask[0].Length - 1) / 2;
            int maskOffsetY = (mask.Length - 1) / 2;
            int stride = width * 5;

            for (int offsetY = maskOffsetY; offsetY < height - maskOffsetY; offsetY++)
            {
                for (int offsetX = maskOffsetX; offsetX < width - maskOffsetX; offsetX++)
                {
                    red = 0;
                    green = 0;
                    blue = 0;

                    int pixelsOffset = offsetY * stride + offsetX * 5;

                    for (int maskY = -maskOffsetY; maskY <= maskOffsetY; maskY++)
                    {
                        for (int maskX = -maskOffsetX; maskX <= maskOffsetX; maskX++)
                        {

                            int calcOffset = pixelsOffset + (maskX * 5) + (maskY * stride);
                            red += (imagePixels[calcOffset])
                                * mask[maskY + maskOffsetY]
                             [maskX + maskOffsetX];

                            green += (imagePixels[calcOffset + 1])
                                * mask[maskY + maskOffsetY][maskX + maskOffsetX];

                            blue += (imagePixels[calcOffset + 2])
                                * mask[maskY + maskOffsetY][maskX + maskOffsetX];
                        }
                    }

                    convImage[pixelsOffset] = red;
                    convImage[pixelsOffset + 1] = green;
                    convImage[pixelsOffset + 2] = blue;
                }
            }

            return convImage;
        }

        private Bitmap MedianFilter(Bitmap sourceBitmap, int matrixSize)
        {
            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);

            int filterOffset = (matrixSize - 1) / 2;
            List<int> neighbourPixels = new List<int>();
            byte[] middlePixel;


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    int byteOffset = offsetY *
                     sourceData.Stride +
                     offsetX * 4;
                    neighbourPixels.Clear();


                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {


                            int calcOffset = byteOffset +
                             (filterX * 4) +
                    (filterY * sourceData.Stride);
                            neighbourPixels.Add(BitConverter.ToInt32(
                                             pixelBuffer, calcOffset));
                        }
                    }


                    neighbourPixels.Sort();

                    middlePixel = BitConverter.GetBytes(
                                       neighbourPixels[filterOffset]);


                    resultBuffer[byteOffset] = middlePixel[0];
                    resultBuffer[byteOffset + 1] = middlePixel[1];
                    resultBuffer[byteOffset + 2] = middlePixel[2];
                    resultBuffer[byteOffset + 3] = middlePixel[3];
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                     sourceBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);


            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

    }
}
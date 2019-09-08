using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace PDI2
{
    public partial class MainWindow : Form
    {
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

                // Collect trash
                GC.Collect();
                GC.WaitForPendingFinalizers();

                pictureBox1.Invalidate();
            }
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
    }
}

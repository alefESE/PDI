using System;
using System.Drawing;
using System.Windows.Forms;

namespace AudioUtils
{
    public partial class WaveController : UserControl
    {
        public WaveController()
        {
            InitializeComponent();
        }

        public WaveFile GetWaveFile() => m_Wavefile;

        public void Read()
        {
            m_Wavefile = new WaveFile(m_Filename);

            m_Wavefile.Read();

            m_DrawWave = true;

            Refresh();
        }

        private void WaveControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Yellow);

            if (m_DrawWave)
            {
                Draw(e, pen);
            }

            int regionStartX = Math.Min(m_StartX, m_EndX);
            int regionEndX = Math.Max(m_StartX, m_EndX);
            SolidBrush brush = new SolidBrush(Color.FromArgb(128, 0, 0, 0));
            e.Graphics.FillRectangle(brush, regionStartX, 0, regionEndX - regionStartX, e.Graphics.VisibleClipBounds.Height);
        }

        protected override void OnMouseWheel(MouseEventArgs mea)
        {
            if (mea.Delta * SystemInformation.MouseWheelScrollLines / 120 > 0)
                ZoomIn();
            else
                ZoomOut();

            Refresh();
        }

        private void Draw(PaintEventArgs pea, Pen pen)
        {
            Graphics grfx = pea.Graphics;

            RectangleF visBounds = grfx.VisibleClipBounds;

            if (m_SamplesPerPixel == 0.0)
            {
                this.SamplesPerPixel = (m_Wavefile.Data.NumSamples / visBounds.Width);
            }

            grfx.DrawLine(pen, 0, visBounds.Height / 2, visBounds.Width, visBounds.Height / 2);

            grfx.TranslateTransform(0, visBounds.Height);
            grfx.ScaleTransform(1, -1);

            if (m_Wavefile.Format.BitsPerSample == 16)
                Draw16Bit(grfx, pen, visBounds);
        }

        private void Draw16Bit(Graphics grfx, Pen pen, RectangleF visBounds)
        {
            int prevX = 0;
            int prevY = 0;

            int i = 0;

            // index is how far to offset into the data array
            int index = m_OffsetInSamples;
            int maxSampleToShow = (int)((m_SamplesPerPixel * visBounds.Width) + m_OffsetInSamples);

            maxSampleToShow = Math.Min(maxSampleToShow, m_Wavefile.Data.NumSamples);

            while (index < maxSampleToShow)
            {
                short maxVal = -32767;
                short minVal = 32767;

                // finds the max & min peaks for this pixel 
                for (int x = 0; x < m_SamplesPerPixel; x++)
                {
                    maxVal = Math.Max(maxVal, m_Wavefile.Data[x + index]);
                    minVal = Math.Min(minVal, m_Wavefile.Data[x + index]);
                }

                // scales based on height of window
                int scaledMinVal = (int)(((minVal + 32768) * visBounds.Height) / 65536);
                int scaledMaxVal = (int)(((maxVal + 32768) * visBounds.Height) / 65536);

                //  if samples per pixel is small or less than zero, we are out of zoom range, so don't display anything
                if (m_SamplesPerPixel > 0.0000000001)
                {
                    // if the max/min are the same, then draw a line from the previous position,
                    // otherwise we will not see anything
                    if (scaledMinVal == scaledMaxVal)
                    {
                        if (prevY != 0)
                            grfx.DrawLine(pen, prevX, prevY, i, scaledMaxVal);
                    }
                    else
                    {
                        grfx.DrawLine(pen, i, scaledMinVal, i, scaledMaxVal);
                    }
                }
                else
                    return;

                prevX = i;
                prevY = scaledMaxVal;

                i++;
                index = (int)(i * m_SamplesPerPixel) + m_OffsetInSamples;
            }
        }

        private void ZoomIn()
        {
            m_SamplesPerPixel -= m_ZoomFactor;
        }

        private void ZoomOut()
        {
            m_SamplesPerPixel += m_ZoomFactor;
        }

        private void ZoomToRegion()
        {
            int regionStartX = Math.Min(m_StartX, m_EndX);
            int regionEndX = Math.Max(m_StartX, m_EndX);

            // if they are negative, make them zero
            regionStartX = Math.Max(0, regionStartX);
            regionEndX = Math.Max(0, regionEndX);

            m_OffsetInSamples += (int)(regionStartX * m_SamplesPerPixel);

            int numSamplesToShow = (int)((regionEndX - regionStartX) * m_SamplesPerPixel);

            if (numSamplesToShow > 0)
            {
                this.SamplesPerPixel = (double)numSamplesToShow / this.Width; ;

                m_ResetRegion = true;
            }
        }

        private void ZoomOutFull()
        {
            this.SamplesPerPixel = (m_Wavefile.Data.NumSamples / this.Width);
            m_OffsetInSamples = 0;

            m_ResetRegion = true;
        }

        private void Scroll(int newXValue)
        {
            m_OffsetInSamples -= (int)((newXValue - m_PrevX) * m_SamplesPerPixel);

            if (m_OffsetInSamples < 0)
                m_OffsetInSamples = 0;
        }

        private void WaveControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_AltKeyDown)
                {
                    m_PrevX = e.X;
                }
                else
                {
                    m_StartX = e.X;
                    m_ResetRegion = true;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (e.Clicks == 2)
                    ZoomOutFull();
                else
                    ZoomToRegion();
            }
        }

        private void WaveControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (m_AltKeyDown)
                {
                    Scroll(e.X);
                }
                else
                {
                    m_EndX = e.X;
                    m_ResetRegion = false;
                }

                m_PrevX = e.X;

                Refresh();
            }
        }

        private void WaveControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (m_AltKeyDown)
                return;

            if (m_ResetRegion)
            {
                m_StartX = 0;
                m_EndX = 0;

                Refresh();
            }
            else
            {
                m_EndX = e.X;
            }
        }

        private void WaveControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Alt)
            {
                m_AltKeyDown = true;
            }
        }

        private void WaveControl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Menu)
            {
                m_AltKeyDown = false;
            }
        }

    }
}

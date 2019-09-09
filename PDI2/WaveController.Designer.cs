namespace AudioUtils
{
    partial class WaveController
    {
        /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // WaveControl
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "WaveControl";
            this.Size = new System.Drawing.Size(50, 50);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WaveControl_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WaveControl_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WaveControl_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WaveControl_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WaveControl_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WaveControl_MouseDown);

        }
        #endregion

        #region Private attrs

        /// <summary>
        /// This is the WaveFile class variable that describes the internal structures of the .WAV
        /// </summary>
        public WaveFile m_Wavefile;

        /// <summary>
        /// Boolean for whether the .WAV should draw or not.  So that the control doesnt draw the .WAV until after it is read
        /// </summary>
        private bool m_DrawWave = false;

        /// <summary>
        /// Filename string
        /// </summary>
        private string m_Filename;

        /// <summary>
        /// Each pixel value (X direction) represents this many samples in the wavefile
        /// Starting value is based on control width so that the .WAV will cover the entire width.
        /// </summary>
        private double m_SamplesPerPixel = 0.0;

        /// <summary>
        /// This value is the amount to increase/decrease the m_SamplesPerPixel.  This creates a 'Zoom' affect.
        /// Starting value is m_SamplesPerPixel / 25    so that it is scaled for the size of the .WAV
        /// </summary>
        private double m_ZoomFactor;

        /// <summary>
        /// This is the starting x value of a mouse drag
        /// </summary>
        private int m_StartX = 0;

        /// <summary>
        /// This is the ending x value of a mouse drag
        /// </summary>
        private int m_EndX = 0;

        /// <summary>
        /// This is the value of the previous mouse move event
        /// </summary>
        private int m_PrevX = 0;


        /// <summary>
        /// This boolean value gets rid of the currently active region and also refreshes the wave
        /// </summary>
        private bool m_ResetRegion;

        /// <summary>
        /// Boolean for whether the Alt key is down
        /// </summary>
        private bool m_AltKeyDown = false;

        /// <summary>
        /// Offset from the beginning of the wave for where to start drawing
        /// </summary>
        private int m_OffsetInSamples = 0;

        public string Filename
        {
            set { m_Filename = value; }
            get { return m_Filename; }
        }

        private double SamplesPerPixel
        {
            set
            {
                m_SamplesPerPixel = value;
                m_ZoomFactor = m_SamplesPerPixel / 25;
            }
        }

        #endregion
    }
}

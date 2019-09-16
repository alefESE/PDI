namespace PDI2
{
    partial class MainWindow
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea13 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title13 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea14 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title14 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxOptimum = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCompress = new System.Windows.Forms.Button();
            this.numericNCompress = new System.Windows.Forms.NumericUpDown();
            this.btnPlayIdct = new System.Windows.Forms.Button();
            this.numericN = new System.Windows.Forms.NumericUpDown();
            this.btnNFilter = new System.Windows.Forms.Button();
            this.btnIDCTAudio = new System.Windows.Forms.Button();
            this.btnDCTAudio = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFilterImage = new System.Windows.Forms.Button();
            this.numericNImage = new System.Windows.Forms.NumericUpDown();
            this.btnIDCTImage = new System.Windows.Forms.Button();
            this.btnDCTImage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.transformAudioChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.originalAudioChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dctPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNCompress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericN)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transformAudioChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalAudioChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dctPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.sobreToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.arquivoToolStripMenuItem.Text = "Arquivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.audioToolStripMenuItem,
            this.imagemToolStripMenuItem});
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.abrirToolStripMenuItem.Text = "Abrir...";
            // 
            // audioToolStripMenuItem
            // 
            this.audioToolStripMenuItem.Name = "audioToolStripMenuItem";
            this.audioToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.audioToolStripMenuItem.Text = "Áudio...";
            this.audioToolStripMenuItem.Click += new System.EventHandler(this.AudioToolStripMenuItem_Click);
            // 
            // imagemToolStripMenuItem
            // 
            this.imagemToolStripMenuItem.Name = "imagemToolStripMenuItem";
            this.imagemToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.imagemToolStripMenuItem.Text = "Imagem...";
            this.imagemToolStripMenuItem.Click += new System.EventHandler(this.ImagemToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.SairToolStripMenuItem_Click);
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.sobreToolStripMenuItem.Text = "Ajuda";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 190);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Imagem";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxOptimum);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(595, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 427);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controles";
            // 
            // checkBoxOptimum
            // 
            this.checkBoxOptimum.AutoSize = true;
            this.checkBoxOptimum.Location = new System.Drawing.Point(134, 264);
            this.checkBoxOptimum.Name = "checkBoxOptimum";
            this.checkBoxOptimum.Size = new System.Drawing.Size(66, 17);
            this.checkBoxOptimum.TabIndex = 2;
            this.checkBoxOptimum.Text = "Optimize";
            this.checkBoxOptimum.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCompress);
            this.groupBox3.Controls.Add(this.numericNCompress);
            this.groupBox3.Controls.Add(this.btnPlayIdct);
            this.groupBox3.Controls.Add(this.numericN);
            this.groupBox3.Controls.Add(this.btnNFilter);
            this.groupBox3.Controls.Add(this.btnIDCTAudio);
            this.groupBox3.Controls.Add(this.btnDCTAudio);
            this.groupBox3.Controls.Add(this.btnPlay);
            this.groupBox3.Location = new System.Drawing.Point(8, 112);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 146);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Áudio";
            // 
            // btnCompress
            // 
            this.btnCompress.Enabled = false;
            this.btnCompress.Location = new System.Drawing.Point(116, 117);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(64, 23);
            this.btnCompress.TabIndex = 7;
            this.btnCompress.Text = "Compress";
            this.btnCompress.UseVisualStyleBackColor = true;
            this.btnCompress.Click += new System.EventHandler(this.BtnCompress_Click);
            // 
            // numericNCompress
            // 
            this.numericNCompress.DecimalPlaces = 1;
            this.numericNCompress.Enabled = false;
            this.numericNCompress.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericNCompress.Location = new System.Drawing.Point(54, 117);
            this.numericNCompress.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericNCompress.Name = "numericNCompress";
            this.numericNCompress.Size = new System.Drawing.Size(56, 20);
            this.numericNCompress.TabIndex = 6;
            // 
            // btnPlayIdct
            // 
            this.btnPlayIdct.Enabled = false;
            this.btnPlayIdct.Location = new System.Drawing.Point(7, 87);
            this.btnPlayIdct.Name = "btnPlayIdct";
            this.btnPlayIdct.Size = new System.Drawing.Size(64, 23);
            this.btnPlayIdct.TabIndex = 5;
            this.btnPlayIdct.Text = "Play iDCT";
            this.btnPlayIdct.UseVisualStyleBackColor = true;
            this.btnPlayIdct.Click += new System.EventHandler(this.BtnPlayIdct_Click);
            // 
            // numericN
            // 
            this.numericN.Enabled = false;
            this.numericN.Location = new System.Drawing.Point(77, 90);
            this.numericN.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericN.Name = "numericN";
            this.numericN.Size = new System.Drawing.Size(56, 20);
            this.numericN.TabIndex = 4;
            // 
            // btnNFilter
            // 
            this.btnNFilter.Enabled = false;
            this.btnNFilter.Location = new System.Drawing.Point(139, 87);
            this.btnNFilter.Name = "btnNFilter";
            this.btnNFilter.Size = new System.Drawing.Size(41, 23);
            this.btnNFilter.TabIndex = 3;
            this.btnNFilter.Text = "Filter";
            this.btnNFilter.UseVisualStyleBackColor = true;
            this.btnNFilter.Click += new System.EventHandler(this.BtnNFilter_Click);
            // 
            // btnIDCTAudio
            // 
            this.btnIDCTAudio.Enabled = false;
            this.btnIDCTAudio.Location = new System.Drawing.Point(139, 32);
            this.btnIDCTAudio.Name = "btnIDCTAudio";
            this.btnIDCTAudio.Size = new System.Drawing.Size(41, 25);
            this.btnIDCTAudio.TabIndex = 2;
            this.btnIDCTAudio.Text = "IDCT";
            this.btnIDCTAudio.UseVisualStyleBackColor = true;
            this.btnIDCTAudio.Click += new System.EventHandler(this.BtnIDCTAudio_Click);
            // 
            // btnDCTAudio
            // 
            this.btnDCTAudio.Enabled = false;
            this.btnDCTAudio.Location = new System.Drawing.Point(92, 32);
            this.btnDCTAudio.Name = "btnDCTAudio";
            this.btnDCTAudio.Size = new System.Drawing.Size(41, 25);
            this.btnDCTAudio.TabIndex = 1;
            this.btnDCTAudio.Text = "DCT";
            this.btnDCTAudio.UseVisualStyleBackColor = true;
            this.btnDCTAudio.Click += new System.EventHandler(this.BtnDCTAudio_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Location = new System.Drawing.Point(6, 32);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(48, 25);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFilterImage);
            this.groupBox2.Controls.Add(this.numericNImage);
            this.groupBox2.Controls.Add(this.btnIDCTImage);
            this.groupBox2.Controls.Add(this.btnDCTImage);
            this.groupBox2.Location = new System.Drawing.Point(7, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 86);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Imagem";
            // 
            // btnFilterImage
            // 
            this.btnFilterImage.Enabled = false;
            this.btnFilterImage.Location = new System.Drawing.Point(139, 57);
            this.btnFilterImage.Name = "btnFilterImage";
            this.btnFilterImage.Size = new System.Drawing.Size(41, 23);
            this.btnFilterImage.TabIndex = 6;
            this.btnFilterImage.Text = "Filter";
            this.btnFilterImage.UseVisualStyleBackColor = true;
            this.btnFilterImage.Click += new System.EventHandler(this.BtnFilterImage_Click);
            // 
            // numericNImage
            // 
            this.numericNImage.Enabled = false;
            this.numericNImage.Location = new System.Drawing.Point(77, 60);
            this.numericNImage.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericNImage.Name = "numericNImage";
            this.numericNImage.Size = new System.Drawing.Size(56, 20);
            this.numericNImage.TabIndex = 6;
            // 
            // btnIDCTImage
            // 
            this.btnIDCTImage.Enabled = false;
            this.btnIDCTImage.Location = new System.Drawing.Point(133, 19);
            this.btnIDCTImage.Name = "btnIDCTImage";
            this.btnIDCTImage.Size = new System.Drawing.Size(47, 23);
            this.btnIDCTImage.TabIndex = 1;
            this.btnIDCTImage.Text = "IDCT";
            this.btnIDCTImage.UseVisualStyleBackColor = true;
            this.btnIDCTImage.Click += new System.EventHandler(this.BtnIDCTImage_Click);
            // 
            // btnDCTImage
            // 
            this.btnDCTImage.Enabled = false;
            this.btnDCTImage.Location = new System.Drawing.Point(80, 19);
            this.btnDCTImage.Name = "btnDCTImage";
            this.btnDCTImage.Size = new System.Drawing.Size(47, 23);
            this.btnDCTImage.TabIndex = 0;
            this.btnDCTImage.Text = "DCT";
            this.btnDCTImage.UseVisualStyleBackColor = true;
            this.btnDCTImage.Click += new System.EventHandler(this.BtnDCTImage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(335, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Áudio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Transformada";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Transformada";
            // 
            // transformAudioChart
            // 
            this.transformAudioChart.BackColor = System.Drawing.Color.Transparent;
            chartArea13.AxisX.LabelStyle.Enabled = false;
            chartArea13.AxisX.MajorGrid.Enabled = false;
            chartArea13.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea13.AxisY.LabelStyle.Enabled = false;
            chartArea13.AxisY.MajorGrid.Enabled = false;
            chartArea13.BackColor = System.Drawing.Color.Transparent;
            chartArea13.CursorX.IsUserSelectionEnabled = true;
            chartArea13.Name = "ChartArea1";
            this.transformAudioChart.ChartAreas.Add(chartArea13);
            this.transformAudioChart.Location = new System.Drawing.Point(338, 285);
            this.transformAudioChart.Name = "transformAudioChart";
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series13.Color = System.Drawing.Color.Blue;
            series13.Name = "Frequencies";
            this.transformAudioChart.Series.Add(series13);
            this.transformAudioChart.Size = new System.Drawing.Size(251, 190);
            this.transformAudioChart.TabIndex = 11;
            this.transformAudioChart.Text = "Transfom Audio";
            title13.Name = "Title";
            this.transformAudioChart.Titles.Add(title13);
            // 
            // originalAudioChart
            // 
            this.originalAudioChart.BackColor = System.Drawing.Color.Transparent;
            chartArea14.AxisX.LabelStyle.Enabled = false;
            chartArea14.AxisX.MajorGrid.Enabled = false;
            chartArea14.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Silver;
            chartArea14.AxisY.LabelStyle.Enabled = false;
            chartArea14.AxisY.MajorGrid.Enabled = false;
            chartArea14.BackColor = System.Drawing.Color.Transparent;
            chartArea14.CursorX.IsUserSelectionEnabled = true;
            chartArea14.Name = "ChartArea1";
            this.originalAudioChart.ChartAreas.Add(chartArea14);
            this.originalAudioChart.Location = new System.Drawing.Point(338, 48);
            this.originalAudioChart.Name = "originalAudioChart";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series14.Color = System.Drawing.Color.Red;
            series14.Legend = "Legend1";
            series14.Name = "Frequencies";
            this.originalAudioChart.Series.Add(series14);
            this.originalAudioChart.Size = new System.Drawing.Size(251, 190);
            this.originalAudioChart.TabIndex = 12;
            this.originalAudioChart.Text = "Original Áudio";
            title14.Name = "Title";
            this.originalAudioChart.Titles.Add(title14);
            // 
            // dctPictureBox
            // 
            this.dctPictureBox.Location = new System.Drawing.Point(12, 282);
            this.dctPictureBox.Name = "dctPictureBox";
            this.dctPictureBox.Size = new System.Drawing.Size(256, 193);
            this.dctPictureBox.TabIndex = 3;
            this.dctPictureBox.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 487);
            this.Controls.Add(this.dctPictureBox);
            this.Controls.Add(this.originalAudioChart);
            this.Controls.Add(this.transformAudioChart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericNCompress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericN)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericNImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transformAudioChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalAudioChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dctPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Private Components

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDCTAudio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDCTImage;
        private System.Windows.Forms.Button btnIDCTAudio;
        private System.Windows.Forms.Button btnIDCTImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog;

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart transformAudioChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart originalAudioChart;
        private System.Windows.Forms.Button btnNFilter;
        private System.Windows.Forms.NumericUpDown numericN;
        private System.Windows.Forms.Button btnPlayIdct;
        private System.Windows.Forms.CheckBox checkBoxOptimum;
        private System.Windows.Forms.PictureBox dctPictureBox;
        private System.Windows.Forms.Button btnFilterImage;
        private System.Windows.Forms.NumericUpDown numericNImage;
        private System.Windows.Forms.Button btnCompress;
        private System.Windows.Forms.NumericUpDown numericNCompress;
    }
}


using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PDI
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSeparator = new PDI.ExtendedToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxRGB = new System.Windows.Forms.GroupBox();
            this.pictureBoxRGB = new System.Windows.Forms.PictureBox();
            this.groupBoxR = new System.Windows.Forms.GroupBox();
            this.pictureBoxR = new System.Windows.Forms.PictureBox();
            this.groupBoxG = new System.Windows.Forms.GroupBox();
            this.pictureBoxG = new System.Windows.Forms.PictureBox();
            this.groupBoxB = new System.Windows.Forms.GroupBox();
            this.pictureBoxB = new System.Windows.Forms.PictureBox();
            this.groupBoxControls = new System.Windows.Forms.GroupBox();
            this.labelX = new System.Windows.Forms.Label();
            this.medianNLabel = new System.Windows.Forms.Label();
            this.medianMLabel = new System.Windows.Forms.Label();
            this.numericUpDownN = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownM = new System.Windows.Forms.NumericUpDown();
            this.applyMedianButton = new System.Windows.Forms.Button();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.resetFilterButton = new System.Windows.Forms.Button();
            this.brightnessLabel = new System.Windows.Forms.Label();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.checkBoxNegative = new System.Windows.Forms.CheckBox();
            this.checkBoxYIQ = new System.Windows.Forms.CheckBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.filterLabel = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogFilter = new System.Windows.Forms.OpenFileDialog();
            this.toolTipBrightness = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxRGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRGB)).BeginInit();
            this.groupBoxR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR)).BeginInit();
            this.groupBoxG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxG)).BeginInit();
            this.groupBoxB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxB)).BeginInit();
            this.groupBoxControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archiveToolStripMenuItem,
            this.ajudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(828, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archiveToolStripMenuItem
            // 
            this.archiveToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            this.archiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItemSeparator,
            this.exitToolStripMenuItem});
            this.archiveToolStripMenuItem.Name = "archiveToolStripMenuItem";
            this.archiveToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.archiveToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.archiveToolStripMenuItem.Text = "Arquivo";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.openToolStripMenuItem.Text = "Abir...";
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.imageToolStripMenuItem.Text = "Imagem...";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.ImageToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.filterToolStripMenuItem.Text = "Filtro...";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.FilterToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSeparator
            // 
            this.toolStripMenuItemSeparator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripMenuItemSeparator.Name = "toolStripMenuItemSeparator";
            this.toolStripMenuItemSeparator.Size = new System.Drawing.Size(138, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt + F4";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.exitToolStripMenuItem.Text = "Sair";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // ajudaToolStripMenuItem
            // 
            this.ajudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new System.Drawing.Size(50, 24);
            this.ajudaToolStripMenuItem.Text = "Ajuda";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.aboutToolStripMenuItem.Text = "Sobre...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxRGB, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxR, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxG, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxB, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 29);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(571, 418);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // groupBoxRGB
            // 
            this.groupBoxRGB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRGB.Controls.Add(this.pictureBoxRGB);
            this.groupBoxRGB.Location = new System.Drawing.Point(3, 3);
            this.groupBoxRGB.Name = "groupBoxRGB";
            this.groupBoxRGB.Size = new System.Drawing.Size(279, 203);
            this.groupBoxRGB.TabIndex = 0;
            this.groupBoxRGB.TabStop = false;
            this.groupBoxRGB.Text = "Canal RGB";
            // 
            // pictureBoxRGB
            // 
            this.pictureBoxRGB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxRGB.Location = new System.Drawing.Point(7, 20);
            this.pictureBoxRGB.Name = "pictureBoxRGB";
            this.pictureBoxRGB.Size = new System.Drawing.Size(266, 177);
            this.pictureBoxRGB.TabIndex = 0;
            this.pictureBoxRGB.TabStop = false;
            // 
            // groupBoxR
            // 
            this.groupBoxR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxR.Controls.Add(this.pictureBoxR);
            this.groupBoxR.Location = new System.Drawing.Point(288, 3);
            this.groupBoxR.Name = "groupBoxR";
            this.groupBoxR.Size = new System.Drawing.Size(280, 203);
            this.groupBoxR.TabIndex = 1;
            this.groupBoxR.TabStop = false;
            this.groupBoxR.Text = "Canal R";
            // 
            // pictureBoxR
            // 
            this.pictureBoxR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxR.Location = new System.Drawing.Point(8, 20);
            this.pictureBoxR.Name = "pictureBoxR";
            this.pictureBoxR.Size = new System.Drawing.Size(266, 177);
            this.pictureBoxR.TabIndex = 1;
            this.pictureBoxR.TabStop = false;
            // 
            // groupBoxG
            // 
            this.groupBoxG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxG.Controls.Add(this.pictureBoxG);
            this.groupBoxG.Location = new System.Drawing.Point(3, 212);
            this.groupBoxG.Name = "groupBoxG";
            this.groupBoxG.Size = new System.Drawing.Size(279, 203);
            this.groupBoxG.TabIndex = 2;
            this.groupBoxG.TabStop = false;
            this.groupBoxG.Text = "Canal G";
            // 
            // pictureBoxG
            // 
            this.pictureBoxG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxG.Location = new System.Drawing.Point(7, 19);
            this.pictureBoxG.Name = "pictureBoxG";
            this.pictureBoxG.Size = new System.Drawing.Size(266, 177);
            this.pictureBoxG.TabIndex = 2;
            this.pictureBoxG.TabStop = false;
            // 
            // groupBoxB
            // 
            this.groupBoxB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxB.Controls.Add(this.pictureBoxB);
            this.groupBoxB.Location = new System.Drawing.Point(288, 212);
            this.groupBoxB.Name = "groupBoxB";
            this.groupBoxB.Size = new System.Drawing.Size(280, 203);
            this.groupBoxB.TabIndex = 3;
            this.groupBoxB.TabStop = false;
            this.groupBoxB.Text = "Canal B";
            // 
            // pictureBoxB
            // 
            this.pictureBoxB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxB.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxB.Name = "pictureBoxB";
            this.pictureBoxB.Size = new System.Drawing.Size(266, 181);
            this.pictureBoxB.TabIndex = 3;
            this.pictureBoxB.TabStop = false;
            // 
            // groupBoxControls
            // 
            this.groupBoxControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxControls.Controls.Add(this.labelX);
            this.groupBoxControls.Controls.Add(this.medianNLabel);
            this.groupBoxControls.Controls.Add(this.medianMLabel);
            this.groupBoxControls.Controls.Add(this.numericUpDownN);
            this.groupBoxControls.Controls.Add(this.numericUpDownM);
            this.groupBoxControls.Controls.Add(this.applyMedianButton);
            this.groupBoxControls.Controls.Add(this.applyFilterButton);
            this.groupBoxControls.Controls.Add(this.resetFilterButton);
            this.groupBoxControls.Controls.Add(this.brightnessLabel);
            this.groupBoxControls.Controls.Add(this.trackBarBrightness);
            this.groupBoxControls.Controls.Add(this.checkBoxNegative);
            this.groupBoxControls.Controls.Add(this.checkBoxYIQ);
            this.groupBoxControls.Controls.Add(this.applyButton);
            this.groupBoxControls.Controls.Add(this.filterLabel);
            this.groupBoxControls.Controls.Add(this.dataGridView);
            this.groupBoxControls.Location = new System.Drawing.Point(580, 32);
            this.groupBoxControls.Name = "groupBoxControls";
            this.groupBoxControls.Size = new System.Drawing.Size(236, 412);
            this.groupBoxControls.TabIndex = 2;
            this.groupBoxControls.TabStop = false;
            this.groupBoxControls.Text = "Controles";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(48, 359);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(14, 13);
            this.labelX.TabIndex = 14;
            this.labelX.Text = "X";
            // 
            // medianNLabel
            // 
            this.medianNLabel.AutoSize = true;
            this.medianNLabel.Location = new System.Drawing.Point(68, 338);
            this.medianNLabel.Name = "medianNLabel";
            this.medianNLabel.Size = new System.Drawing.Size(15, 13);
            this.medianNLabel.TabIndex = 13;
            this.medianNLabel.Text = "N";
            // 
            // medianMLabel
            // 
            this.medianMLabel.AutoSize = true;
            this.medianMLabel.Location = new System.Drawing.Point(7, 338);
            this.medianMLabel.Name = "medianMLabel";
            this.medianMLabel.Size = new System.Drawing.Size(16, 13);
            this.medianMLabel.TabIndex = 12;
            this.medianMLabel.Text = "M";
            // 
            // numericUpDownN
            // 
            this.numericUpDownN.Location = new System.Drawing.Point(68, 357);
            this.numericUpDownN.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownN.Name = "numericUpDownN";
            this.numericUpDownN.Size = new System.Drawing.Size(36, 20);
            this.numericUpDownN.TabIndex = 11;
            this.numericUpDownN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownM
            // 
            this.numericUpDownM.Location = new System.Drawing.Point(7, 357);
            this.numericUpDownM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownM.Name = "numericUpDownM";
            this.numericUpDownM.Size = new System.Drawing.Size(36, 20);
            this.numericUpDownM.TabIndex = 10;
            this.numericUpDownM.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // applyMedianButton
            // 
            this.applyMedianButton.Location = new System.Drawing.Point(6, 383);
            this.applyMedianButton.Name = "applyMedianButton";
            this.applyMedianButton.Size = new System.Drawing.Size(98, 23);
            this.applyMedianButton.TabIndex = 9;
            this.applyMedianButton.Text = "Aplicar Mediana";
            this.applyMedianButton.UseVisualStyleBackColor = true;
            this.applyMedianButton.Click += new System.EventHandler(this.ApplyMedianButton_Click);
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Location = new System.Drawing.Point(132, 225);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(98, 23);
            this.applyFilterButton.TabIndex = 8;
            this.applyFilterButton.Text = "Aplicar Filtro";
            this.applyFilterButton.UseVisualStyleBackColor = true;
            this.applyFilterButton.Click += new System.EventHandler(this.ApplyFilterButton_Click);
            // 
            // resetFilterButton
            // 
            this.resetFilterButton.Location = new System.Drawing.Point(155, 196);
            this.resetFilterButton.Name = "resetFilterButton";
            this.resetFilterButton.Size = new System.Drawing.Size(75, 23);
            this.resetFilterButton.TabIndex = 7;
            this.resetFilterButton.Text = "Limpar filtro";
            this.resetFilterButton.UseVisualStyleBackColor = true;
            this.resetFilterButton.Click += new System.EventHandler(this.ResetFilterButton_Click);
            // 
            // brightnessLabel
            // 
            this.brightnessLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.brightnessLabel.AutoSize = true;
            this.brightnessLabel.Location = new System.Drawing.Point(10, 233);
            this.brightnessLabel.Name = "brightnessLabel";
            this.brightnessLabel.Size = new System.Drawing.Size(33, 13);
            this.brightnessLabel.TabIndex = 6;
            this.brightnessLabel.Text = "Brilho";
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.LargeChange = 2;
            this.trackBarBrightness.Location = new System.Drawing.Point(10, 251);
            this.trackBarBrightness.Maximum = 50;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(220, 45);
            this.trackBarBrightness.TabIndex = 5;
            this.trackBarBrightness.Value = 10;
            this.trackBarBrightness.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // checkBoxNegative
            // 
            this.checkBoxNegative.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxNegative.AutoSize = true;
            this.checkBoxNegative.Location = new System.Drawing.Point(69, 209);
            this.checkBoxNegative.Name = "checkBoxNegative";
            this.checkBoxNegative.Size = new System.Drawing.Size(69, 17);
            this.checkBoxNegative.TabIndex = 4;
            this.checkBoxNegative.Text = "Negativo";
            this.checkBoxNegative.UseVisualStyleBackColor = true;
            // 
            // checkBoxYIQ
            // 
            this.checkBoxYIQ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxYIQ.AutoSize = true;
            this.checkBoxYIQ.Location = new System.Drawing.Point(10, 209);
            this.checkBoxYIQ.Name = "checkBoxYIQ";
            this.checkBoxYIQ.Size = new System.Drawing.Size(44, 17);
            this.checkBoxYIQ.TabIndex = 3;
            this.checkBoxYIQ.Text = "YIQ";
            this.checkBoxYIQ.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(138, 383);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(92, 23);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "Aplicar Efeitos";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // filterLabel
            // 
            this.filterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterLabel.AutoSize = true;
            this.filterLabel.Location = new System.Drawing.Point(7, 20);
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Size = new System.Drawing.Size(29, 13);
            this.filterLabel.TabIndex = 1;
            this.filterLabel.Text = "Filtro";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView.Enabled = false;
            this.dataGridView.Location = new System.Drawing.Point(6, 36);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.Size = new System.Drawing.Size(224, 154);
            this.dataGridView.TabIndex = 0;
            // 
            // openFileDialogImage
            // 
            this.openFileDialogImage.Filter = "Imagem (*.jpg, *.png) | *.jpg; *.png";
            this.openFileDialogImage.Title = "Abrir Imagem";
            // 
            // openFileDialogFilter
            // 
            this.openFileDialogFilter.Filter = "Filtro (*.csv) | *.csv";
            this.openFileDialogFilter.Title = "Abrir Filtro";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(828, 450);
            this.Controls.Add(this.groupBoxControls);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Projeto de PDI";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxRGB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRGB)).EndInit();
            this.groupBoxR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR)).EndInit();
            this.groupBoxG.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxG)).EndInit();
            this.groupBoxB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxB)).EndInit();
            this.groupBoxControls.ResumeLayout(false);
            this.groupBoxControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private ExtendedToolStripSeparator toolStripMenuItemSeparator;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxRGB;
        private System.Windows.Forms.PictureBox pictureBoxRGB;
        private System.Windows.Forms.GroupBox groupBoxR;
        private System.Windows.Forms.PictureBox pictureBoxR;
        private System.Windows.Forms.GroupBox groupBoxG;
        private System.Windows.Forms.PictureBox pictureBoxG;
        private System.Windows.Forms.GroupBox groupBoxB;
        private System.Windows.Forms.PictureBox pictureBoxB;
        private System.Windows.Forms.GroupBox groupBoxControls;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialogImage;
        private System.Windows.Forms.OpenFileDialog openFileDialogFilter;
        private DataGridView dataGridView;
        private Label filterLabel;
        private BindingSource bindingSource = new BindingSource();
        private CheckBox checkBoxYIQ;
        private Button applyButton;
        private CheckBox checkBoxNegative;
        private Label brightnessLabel;
        private TrackBar trackBarBrightness;
        private ToolTip toolTipBrightness;
        private Button resetFilterButton;
        private Button applyMedianButton;
        private Button applyFilterButton;
        private Label labelX;
        private Label medianNLabel;
        private Label medianMLabel;
        private NumericUpDown numericUpDownN;
        private NumericUpDown numericUpDownM;
    }
}


namespace SpriteGenerator
{
    partial class SpritesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.Label lbOutputCss;
            System.Windows.Forms.Label lbImagesPath;
            System.Windows.Forms.Label lbOutputImage;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpritesForm));
            this.tbInputDirectoryPath = new System.Windows.Forms.TextBox();
            this.buttonSelectOutputCSSFilePath = new System.Windows.Forms.Button();
            this.tbOutputImageFilePath = new System.Windows.Forms.TextBox();
            this.tbOutputCSSFilePath = new System.Windows.Forms.TextBox();
            this.btSelectOutputImageFilePath = new System.Windows.Forms.Button();
            this.btBrowseFolder = new System.Windows.Forms.Button();
            this.ndpMarginWidth = new System.Windows.Forms.NumericUpDown();
            this.lbDistanceBetweenImages = new System.Windows.Forms.Label();
            this.ndpDistanceBetweenImages = new System.Windows.Forms.NumericUpDown();
            this.lbMarginWidth = new System.Windows.Forms.Label();
            this.lbSprites = new System.Windows.Forms.Label();
            this.rbAutomaticLayout = new System.Windows.Forms.RadioButton();
            this.ndpImagesInColumn = new System.Windows.Forms.NumericUpDown();
            this.labelX = new System.Windows.Forms.Label();
            this.rbHorizontalLayout = new System.Windows.Forms.RadioButton();
            this.rbVerticalLayout = new System.Windows.Forms.RadioButton();
            this.ndpImagesInRow = new System.Windows.Forms.NumericUpDown();
            this.rbRectangularLayout = new System.Windows.Forms.RadioButton();
            this.btGenerate = new System.Windows.Forms.Button();
            this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdOutputImage = new System.Windows.Forms.SaveFileDialog();
            this.sfdOutputCss = new System.Windows.Forms.SaveFileDialog();
            this.progressWork = new System.Windows.Forms.ProgressBar();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.stripStatus = new System.Windows.Forms.StatusStrip();
            this.lbStatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabBase = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxLayout = new System.Windows.Forms.GroupBox();
            this.groupBoxDistances = new System.Windows.Forms.GroupBox();
            this.groupBoxPaths = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnExit = new System.Windows.Forms.ToolStripMenuItem();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            lbOutputCss = new System.Windows.Forms.Label();
            lbImagesPath = new System.Windows.Forms.Label();
            lbOutputImage = new System.Windows.Forms.Label();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpMarginWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndpDistanceBetweenImages)).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpImagesInColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndpImagesInRow)).BeginInit();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.stripStatus.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxLayout.SuspendLayout();
            this.groupBoxDistances.SuspendLayout();
            this.groupBoxPaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.Controls.Add(this.tbInputDirectoryPath, 1, 0);
            tableLayoutPanel1.Controls.Add(this.buttonSelectOutputCSSFilePath, 2, 2);
            tableLayoutPanel1.Controls.Add(this.tbOutputImageFilePath, 1, 1);
            tableLayoutPanel1.Controls.Add(this.tbOutputCSSFilePath, 1, 2);
            tableLayoutPanel1.Controls.Add(this.btSelectOutputImageFilePath, 2, 1);
            tableLayoutPanel1.Controls.Add(lbOutputCss, 0, 2);
            tableLayoutPanel1.Controls.Add(lbImagesPath, 0, 0);
            tableLayoutPanel1.Controls.Add(this.btBrowseFolder, 2, 0);
            tableLayoutPanel1.Controls.Add(lbOutputImage, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.Size = new System.Drawing.Size(527, 86);
            tableLayoutPanel1.TabIndex = 17;
            // 
            // tbInputDirectoryPath
            // 
            this.tbInputDirectoryPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInputDirectoryPath.Location = new System.Drawing.Point(122, 3);
            this.tbInputDirectoryPath.Name = "tbInputDirectoryPath";
            this.tbInputDirectoryPath.ReadOnly = true;
            this.tbInputDirectoryPath.Size = new System.Drawing.Size(282, 20);
            this.tbInputDirectoryPath.TabIndex = 0;
            // 
            // buttonSelectOutputCSSFilePath
            // 
            this.buttonSelectOutputCSSFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSelectOutputCSSFilePath.Location = new System.Drawing.Point(410, 61);
            this.buttonSelectOutputCSSFilePath.Name = "buttonSelectOutputCSSFilePath";
            this.buttonSelectOutputCSSFilePath.Size = new System.Drawing.Size(114, 23);
            this.buttonSelectOutputCSSFilePath.TabIndex = 10;
            this.buttonSelectOutputCSSFilePath.Text = "Browse";
            this.buttonSelectOutputCSSFilePath.UseVisualStyleBackColor = true;
            this.buttonSelectOutputCSSFilePath.Click += new System.EventHandler(this.BtSelectOutputCssFilePath_Click);
            // 
            // tbOutputImageFilePath
            // 
            this.tbOutputImageFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutputImageFilePath.Location = new System.Drawing.Point(122, 32);
            this.tbOutputImageFilePath.Name = "tbOutputImageFilePath";
            this.tbOutputImageFilePath.ReadOnly = true;
            this.tbOutputImageFilePath.Size = new System.Drawing.Size(282, 20);
            this.tbOutputImageFilePath.TabIndex = 1;
            // 
            // tbOutputCSSFilePath
            // 
            this.tbOutputCSSFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutputCSSFilePath.Location = new System.Drawing.Point(122, 61);
            this.tbOutputCSSFilePath.Name = "tbOutputCSSFilePath";
            this.tbOutputCSSFilePath.ReadOnly = true;
            this.tbOutputCSSFilePath.Size = new System.Drawing.Size(282, 20);
            this.tbOutputCSSFilePath.TabIndex = 2;
            // 
            // btSelectOutputImageFilePath
            // 
            this.btSelectOutputImageFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btSelectOutputImageFilePath.Location = new System.Drawing.Point(410, 32);
            this.btSelectOutputImageFilePath.Name = "btSelectOutputImageFilePath";
            this.btSelectOutputImageFilePath.Size = new System.Drawing.Size(114, 23);
            this.btSelectOutputImageFilePath.TabIndex = 9;
            this.btSelectOutputImageFilePath.Text = "Browse";
            this.btSelectOutputImageFilePath.UseVisualStyleBackColor = true;
            this.btSelectOutputImageFilePath.Click += new System.EventHandler(this.BtSelectOutputImageFilePath_Click);
            // 
            // lbOutputCss
            // 
            lbOutputCss.AutoSize = true;
            lbOutputCss.Dock = System.Windows.Forms.DockStyle.Fill;
            lbOutputCss.Location = new System.Drawing.Point(3, 58);
            lbOutputCss.Name = "lbOutputCss";
            lbOutputCss.Size = new System.Drawing.Size(113, 29);
            lbOutputCss.TabIndex = 7;
            lbOutputCss.Text = "Output CSS file path:";
            // 
            // lbImagesPath
            // 
            lbImagesPath.AutoSize = true;
            lbImagesPath.Dock = System.Windows.Forms.DockStyle.Fill;
            lbImagesPath.Location = new System.Drawing.Point(3, 0);
            lbImagesPath.Name = "lbImagesPath";
            lbImagesPath.Size = new System.Drawing.Size(113, 29);
            lbImagesPath.TabIndex = 5;
            lbImagesPath.Text = "Images directory path:";
            // 
            // btBrowseFolder
            // 
            this.btBrowseFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btBrowseFolder.Location = new System.Drawing.Point(410, 3);
            this.btBrowseFolder.Name = "btBrowseFolder";
            this.btBrowseFolder.Size = new System.Drawing.Size(114, 23);
            this.btBrowseFolder.TabIndex = 8;
            this.btBrowseFolder.Text = "Browse";
            this.btBrowseFolder.UseVisualStyleBackColor = true;
            this.btBrowseFolder.Click += new System.EventHandler(this.BtBrowseFolder_Click);
            // 
            // lbOutputImage
            // 
            lbOutputImage.AutoSize = true;
            lbOutputImage.Dock = System.Windows.Forms.DockStyle.Fill;
            lbOutputImage.Location = new System.Drawing.Point(3, 29);
            lbOutputImage.Name = "lbOutputImage";
            lbOutputImage.Size = new System.Drawing.Size(113, 29);
            lbOutputImage.TabIndex = 6;
            lbOutputImage.Text = "Output image file path:";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel3.Controls.Add(this.ndpMarginWidth, 1, 1);
            tableLayoutPanel3.Controls.Add(this.lbDistanceBetweenImages, 0, 0);
            tableLayoutPanel3.Controls.Add(this.ndpDistanceBetweenImages, 1, 0);
            tableLayoutPanel3.Controls.Add(this.lbMarginWidth, 0, 1);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel3.Size = new System.Drawing.Size(347, 140);
            tableLayoutPanel3.TabIndex = 19;
            // 
            // ndpMarginWidth
            // 
            this.ndpMarginWidth.AutoSize = true;
            this.ndpMarginWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ndpMarginWidth.Location = new System.Drawing.Point(141, 29);
            this.ndpMarginWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ndpMarginWidth.Name = "ndpMarginWidth";
            this.ndpMarginWidth.Size = new System.Drawing.Size(203, 20);
            this.ndpMarginWidth.TabIndex = 16;
            // 
            // lbDistanceBetweenImages
            // 
            this.lbDistanceBetweenImages.AutoSize = true;
            this.lbDistanceBetweenImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbDistanceBetweenImages.Enabled = false;
            this.lbDistanceBetweenImages.Location = new System.Drawing.Point(3, 0);
            this.lbDistanceBetweenImages.Name = "lbDistanceBetweenImages";
            this.lbDistanceBetweenImages.Size = new System.Drawing.Size(132, 26);
            this.lbDistanceBetweenImages.TabIndex = 14;
            this.lbDistanceBetweenImages.Text = "Distance between images:";
            // 
            // ndpDistanceBetweenImages
            // 
            this.ndpDistanceBetweenImages.AutoSize = true;
            this.ndpDistanceBetweenImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ndpDistanceBetweenImages.Enabled = false;
            this.ndpDistanceBetweenImages.Location = new System.Drawing.Point(141, 3);
            this.ndpDistanceBetweenImages.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ndpDistanceBetweenImages.Name = "ndpDistanceBetweenImages";
            this.ndpDistanceBetweenImages.Size = new System.Drawing.Size(203, 20);
            this.ndpDistanceBetweenImages.TabIndex = 13;
            this.ndpDistanceBetweenImages.Tag = "";
            // 
            // lbMarginWidth
            // 
            this.lbMarginWidth.AutoSize = true;
            this.lbMarginWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMarginWidth.Location = new System.Drawing.Point(3, 26);
            this.lbMarginWidth.Name = "lbMarginWidth";
            this.lbMarginWidth.Size = new System.Drawing.Size(132, 114);
            this.lbMarginWidth.TabIndex = 15;
            this.lbMarginWidth.Text = "Margin width:";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel2.Controls.Add(this.lbSprites, 3, 4);
            tableLayoutPanel2.Controls.Add(this.rbAutomaticLayout, 0, 0);
            tableLayoutPanel2.Controls.Add(this.ndpImagesInColumn, 2, 4);
            tableLayoutPanel2.Controls.Add(this.labelX, 1, 4);
            tableLayoutPanel2.Controls.Add(this.rbHorizontalLayout, 0, 1);
            tableLayoutPanel2.Controls.Add(this.rbVerticalLayout, 0, 2);
            tableLayoutPanel2.Controls.Add(this.ndpImagesInRow, 0, 4);
            tableLayoutPanel2.Controls.Add(this.rbRectangularLayout, 0, 3);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel2.Size = new System.Drawing.Size(170, 140);
            tableLayoutPanel2.TabIndex = 19;
            // 
            // lbSprites
            // 
            this.lbSprites.AutoSize = true;
            this.lbSprites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSprites.Enabled = false;
            this.lbSprites.Location = new System.Drawing.Point(133, 92);
            this.lbSprites.Name = "lbSprites";
            this.lbSprites.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lbSprites.Size = new System.Drawing.Size(40, 48);
            this.lbSprites.TabIndex = 22;
            this.lbSprites.Text = "images";
            // 
            // rbAutomaticLayout
            // 
            this.rbAutomaticLayout.AutoSize = true;
            this.rbAutomaticLayout.Checked = true;
            tableLayoutPanel2.SetColumnSpan(this.rbAutomaticLayout, 4);
            this.rbAutomaticLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbAutomaticLayout.Location = new System.Drawing.Point(3, 3);
            this.rbAutomaticLayout.Name = "rbAutomaticLayout";
            this.rbAutomaticLayout.Size = new System.Drawing.Size(170, 17);
            this.rbAutomaticLayout.TabIndex = 17;
            this.rbAutomaticLayout.TabStop = true;
            this.rbAutomaticLayout.Text = "Automatic";
            this.rbAutomaticLayout.UseVisualStyleBackColor = true;
            this.rbAutomaticLayout.CheckedChanged += new System.EventHandler(this.RbLayoutChecked_Changed);
            // 
            // ndpImagesInColumn
            // 
            this.ndpImagesInColumn.BackColor = System.Drawing.SystemColors.Control;
            this.ndpImagesInColumn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ndpImagesInColumn.Enabled = false;
            this.ndpImagesInColumn.Location = new System.Drawing.Point(76, 95);
            this.ndpImagesInColumn.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ndpImagesInColumn.Name = "ndpImagesInColumn";
            this.ndpImagesInColumn.Size = new System.Drawing.Size(51, 20);
            this.ndpImagesInColumn.TabIndex = 20;
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelX.Enabled = false;
            this.labelX.Location = new System.Drawing.Point(58, 92);
            this.labelX.Name = "labelX";
            this.labelX.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.labelX.Size = new System.Drawing.Size(12, 48);
            this.labelX.TabIndex = 21;
            this.labelX.Text = "x";
            // 
            // rbHorizontalLayout
            // 
            this.rbHorizontalLayout.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(this.rbHorizontalLayout, 4);
            this.rbHorizontalLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbHorizontalLayout.Enabled = false;
            this.rbHorizontalLayout.Location = new System.Drawing.Point(3, 26);
            this.rbHorizontalLayout.Name = "rbHorizontalLayout";
            this.rbHorizontalLayout.Size = new System.Drawing.Size(170, 17);
            this.rbHorizontalLayout.TabIndex = 16;
            this.rbHorizontalLayout.Text = "Horizontal";
            this.rbHorizontalLayout.UseVisualStyleBackColor = true;
            this.rbHorizontalLayout.CheckedChanged += new System.EventHandler(this.RbLayoutChecked_Changed);
            // 
            // rbVerticalLayout
            // 
            this.rbVerticalLayout.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(this.rbVerticalLayout, 4);
            this.rbVerticalLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbVerticalLayout.Enabled = false;
            this.rbVerticalLayout.Location = new System.Drawing.Point(3, 49);
            this.rbVerticalLayout.Name = "rbVerticalLayout";
            this.rbVerticalLayout.Size = new System.Drawing.Size(170, 17);
            this.rbVerticalLayout.TabIndex = 18;
            this.rbVerticalLayout.TabStop = true;
            this.rbVerticalLayout.Text = "Vertical";
            this.rbVerticalLayout.UseVisualStyleBackColor = true;
            this.rbVerticalLayout.CheckedChanged += new System.EventHandler(this.RbLayoutChecked_Changed);
            // 
            // ndpImagesInRow
            // 
            this.ndpImagesInRow.BackColor = System.Drawing.SystemColors.Window;
            this.ndpImagesInRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ndpImagesInRow.Enabled = false;
            this.ndpImagesInRow.Location = new System.Drawing.Point(3, 95);
            this.ndpImagesInRow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ndpImagesInRow.Name = "ndpImagesInRow";
            this.ndpImagesInRow.ReadOnly = true;
            this.ndpImagesInRow.Size = new System.Drawing.Size(49, 20);
            this.ndpImagesInRow.TabIndex = 19;
            this.ndpImagesInRow.ValueChanged += new System.EventHandler(this.NdpImagesInRowValue_Changed);
            // 
            // rbRectangularLayout
            // 
            this.rbRectangularLayout.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(this.rbRectangularLayout, 4);
            this.rbRectangularLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbRectangularLayout.Enabled = false;
            this.rbRectangularLayout.Location = new System.Drawing.Point(3, 72);
            this.rbRectangularLayout.Name = "rbRectangularLayout";
            this.rbRectangularLayout.Size = new System.Drawing.Size(170, 17);
            this.rbRectangularLayout.TabIndex = 15;
            this.rbRectangularLayout.Text = "Rectangular";
            this.rbRectangularLayout.UseVisualStyleBackColor = true;
            this.rbRectangularLayout.CheckedChanged += new System.EventHandler(this.RbRectangularLayoutChecked_Changed);
            // 
            // btGenerate
            // 
            this.btGenerate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btGenerate.Enabled = false;
            this.btGenerate.Location = new System.Drawing.Point(3, 3);
            this.btGenerate.Name = "btGenerate";
            this.btGenerate.Size = new System.Drawing.Size(124, 25);
            this.btGenerate.TabIndex = 3;
            this.btGenerate.Text = "Generate";
            this.btGenerate.UseVisualStyleBackColor = true;
            this.btGenerate.Click += new System.EventHandler(this.BtGenerate_Click);
            // 
            // sfdOutputImage
            // 
            this.sfdOutputImage.Filter = "PNG Image|*.png";
            // 
            // sfdOutputCss
            // 
            this.sfdOutputCss.Filter = "CSS file|*.css";
            // 
            // progressWork
            // 
            this.progressWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressWork.Location = new System.Drawing.Point(3, 3);
            this.progressWork.Name = "progressWork";
            this.progressWork.Size = new System.Drawing.Size(413, 25);
            this.progressWork.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressWork.TabIndex = 18;
            this.progressWork.Visible = false;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.stripStatus);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tabControl);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer2);
            this.toolStripContainer1.ContentPanel.Padding = new System.Windows.Forms.Padding(6);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(565, 345);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(565, 391);
            this.toolStripContainer1.TabIndex = 11;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip);
            // 
            // stripStatus
            // 
            this.stripStatus.Dock = System.Windows.Forms.DockStyle.None;
            this.stripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatusMessage});
            this.stripStatus.Location = new System.Drawing.Point(0, 0);
            this.stripStatus.Name = "stripStatus";
            this.stripStatus.Size = new System.Drawing.Size(565, 22);
            this.stripStatus.TabIndex = 0;
            // 
            // lbStatusMessage
            // 
            this.lbStatusMessage.Name = "lbStatusMessage";
            this.lbStatusMessage.Size = new System.Drawing.Size(69, 17);
            this.lbStatusMessage.Text = "<message>";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabBase);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(6, 6);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(553, 302);
            this.tabControl.TabIndex = 19;
            // 
            // tabBase
            // 
            this.tabBase.Controls.Add(this.splitContainer1);
            this.tabBase.Controls.Add(this.groupBoxPaths);
            this.tabBase.Location = new System.Drawing.Point(4, 22);
            this.tabBase.Name = "tabBase";
            this.tabBase.Padding = new System.Windows.Forms.Padding(6);
            this.tabBase.Size = new System.Drawing.Size(545, 276);
            this.tabBase.TabIndex = 1;
            this.tabBase.Text = "Base";
            this.tabBase.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(6, 111);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxLayout);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxDistances);
            this.splitContainer1.Size = new System.Drawing.Size(533, 159);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.TabIndex = 17;
            // 
            // groupBoxLayout
            // 
            this.groupBoxLayout.Controls.Add(tableLayoutPanel2);
            this.groupBoxLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLayout.Location = new System.Drawing.Point(0, 0);
            this.groupBoxLayout.Name = "groupBoxLayout";
            this.groupBoxLayout.Size = new System.Drawing.Size(176, 159);
            this.groupBoxLayout.TabIndex = 16;
            this.groupBoxLayout.TabStop = false;
            this.groupBoxLayout.Text = "Layout";
            // 
            // groupBoxDistances
            // 
            this.groupBoxDistances.Controls.Add(tableLayoutPanel3);
            this.groupBoxDistances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDistances.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDistances.Name = "groupBoxDistances";
            this.groupBoxDistances.Size = new System.Drawing.Size(353, 159);
            this.groupBoxDistances.TabIndex = 17;
            this.groupBoxDistances.TabStop = false;
            this.groupBoxDistances.Text = "Distances";
            // 
            // groupBoxPaths
            // 
            this.groupBoxPaths.Controls.Add(tableLayoutPanel1);
            this.groupBoxPaths.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPaths.Location = new System.Drawing.Point(6, 6);
            this.groupBoxPaths.Name = "groupBoxPaths";
            this.groupBoxPaths.Size = new System.Drawing.Size(533, 105);
            this.groupBoxPaths.TabIndex = 11;
            this.groupBoxPaths.TabStop = false;
            this.groupBoxPaths.Text = "Paths";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(6, 308);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.progressWork);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btGenerate);
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer2.Size = new System.Drawing.Size(553, 31);
            this.splitContainer2.SplitterDistance = 419;
            this.splitContainer2.TabIndex = 20;
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnFile});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(565, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "Menu";
            // 
            // mnFile
            // 
            this.mnFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnExit});
            this.mnFile.Name = "mnFile";
            this.mnFile.Size = new System.Drawing.Size(37, 20);
            this.mnFile.Text = "&File";
            // 
            // mnExit
            // 
            this.mnExit.Name = "mnExit";
            this.mnExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.mnExit.Size = new System.Drawing.Size(135, 22);
            this.mnExit.Text = "E&xit";
            this.mnExit.Click += new System.EventHandler(this.MnExit_Click);
            // 
            // SpritesForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 391);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SpritesForm";
            this.Text = "SpriteGenerator";
            this.Load += new System.EventHandler(this.SpritesForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SpritesForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SpritesForm_DragEnter);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpMarginWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndpDistanceBetweenImages)).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpImagesInColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndpImagesInRow)).EndInit();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.stripStatus.ResumeLayout(false);
            this.stripStatus.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabBase.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxLayout.ResumeLayout(false);
            this.groupBoxDistances.ResumeLayout(false);
            this.groupBoxPaths.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btGenerate;
        private System.Windows.Forms.FolderBrowserDialog fbDialog;
        private System.Windows.Forms.SaveFileDialog sfdOutputImage;
        private System.Windows.Forms.SaveFileDialog sfdOutputCss;
        private System.Windows.Forms.ProgressBar progressWork;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip stripStatus;
        private System.Windows.Forms.ToolStripStatusLabel lbStatusMessage;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnFile;
        private System.Windows.Forms.ToolStripMenuItem mnExit;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxLayout;
        private System.Windows.Forms.Label lbSprites;
        private System.Windows.Forms.RadioButton rbAutomaticLayout;
        private System.Windows.Forms.NumericUpDown ndpImagesInColumn;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.RadioButton rbHorizontalLayout;
        private System.Windows.Forms.RadioButton rbVerticalLayout;
        private System.Windows.Forms.NumericUpDown ndpImagesInRow;
        private System.Windows.Forms.RadioButton rbRectangularLayout;
        private System.Windows.Forms.GroupBox groupBoxDistances;
        private System.Windows.Forms.NumericUpDown ndpMarginWidth;
        private System.Windows.Forms.Label lbDistanceBetweenImages;
        private System.Windows.Forms.NumericUpDown ndpDistanceBetweenImages;
        private System.Windows.Forms.Label lbMarginWidth;
        private System.Windows.Forms.GroupBox groupBoxPaths;
        private System.Windows.Forms.TextBox tbInputDirectoryPath;
        private System.Windows.Forms.Button buttonSelectOutputCSSFilePath;
        private System.Windows.Forms.TextBox tbOutputImageFilePath;
        private System.Windows.Forms.TextBox tbOutputCSSFilePath;
        private System.Windows.Forms.Button btSelectOutputImageFilePath;
        private System.Windows.Forms.Button btBrowseFolder;
        private System.Windows.Forms.TabPage tabBase;
    }
}


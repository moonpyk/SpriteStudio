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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpritesForm));
            this.btGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbInputDirectoryPath = new System.Windows.Forms.TextBox();
            this.tbOutputImageFilePath = new System.Windows.Forms.TextBox();
            this.tbOutputCSSFilePath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialogOutputImage = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialogOutputCss = new System.Windows.Forms.SaveFileDialog();
            this.btBrowseFolder = new System.Windows.Forms.Button();
            this.btSelectOutputImageFilePath = new System.Windows.Forms.Button();
            this.buttonSelectOutputCSSFilePath = new System.Windows.Forms.Button();
            this.groupBoxPaths = new System.Windows.Forms.GroupBox();
            this.ndpDistanceBetweenImages = new System.Windows.Forms.NumericUpDown();
            this.labelDistanceBetweenImages = new System.Windows.Forms.Label();
            this.rbRectangularLayout = new System.Windows.Forms.RadioButton();
            this.groupBoxLayout = new System.Windows.Forms.GroupBox();
            this.labelSprites = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.ndpImagesInColumn = new System.Windows.Forms.NumericUpDown();
            this.ndpImagesInRow = new System.Windows.Forms.NumericUpDown();
            this.rbVerticalLayout = new System.Windows.Forms.RadioButton();
            this.rbAutomaticLayout = new System.Windows.Forms.RadioButton();
            this.rbHorizontalLayout = new System.Windows.Forms.RadioButton();
            this.groupBoxDistances = new System.Windows.Forms.GroupBox();
            this.ndpMarginWidth = new System.Windows.Forms.NumericUpDown();
            this.labelMarginWidth = new System.Windows.Forms.Label();
            this.progressWork = new System.Windows.Forms.ProgressBar();
            this.groupBoxPaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpDistanceBetweenImages)).BeginInit();
            this.groupBoxLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpImagesInColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndpImagesInRow)).BeginInit();
            this.groupBoxDistances.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpMarginWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // btGenerate
            // 
            this.btGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btGenerate.Enabled = false;
            this.btGenerate.Location = new System.Drawing.Point(434, 253);
            this.btGenerate.Name = "btGenerate";
            this.btGenerate.Size = new System.Drawing.Size(75, 23);
            this.btGenerate.TabIndex = 3;
            this.btGenerate.Text = "Generate";
            this.btGenerate.UseVisualStyleBackColor = true;
            this.btGenerate.Click += new System.EventHandler(this.ButtonGenerateClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Images directory path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Output image file path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Output CSS file path:";
            // 
            // tbInputDirectoryPath
            // 
            this.tbInputDirectoryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputDirectoryPath.Location = new System.Drawing.Point(132, 19);
            this.tbInputDirectoryPath.Name = "tbInputDirectoryPath";
            this.tbInputDirectoryPath.ReadOnly = true;
            this.tbInputDirectoryPath.Size = new System.Drawing.Size(276, 20);
            this.tbInputDirectoryPath.TabIndex = 0;
            // 
            // tbOutputImageFilePath
            // 
            this.tbOutputImageFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputImageFilePath.Location = new System.Drawing.Point(132, 45);
            this.tbOutputImageFilePath.Name = "tbOutputImageFilePath";
            this.tbOutputImageFilePath.ReadOnly = true;
            this.tbOutputImageFilePath.Size = new System.Drawing.Size(276, 20);
            this.tbOutputImageFilePath.TabIndex = 1;
            // 
            // tbOutputCSSFilePath
            // 
            this.tbOutputCSSFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputCSSFilePath.Location = new System.Drawing.Point(132, 71);
            this.tbOutputCSSFilePath.Name = "tbOutputCSSFilePath";
            this.tbOutputCSSFilePath.ReadOnly = true;
            this.tbOutputCSSFilePath.Size = new System.Drawing.Size(276, 20);
            this.tbOutputCSSFilePath.TabIndex = 2;
            // 
            // saveFileDialogOutputImage
            // 
            this.saveFileDialogOutputImage.Filter = "PNG Image|*.png";
            // 
            // saveFileDialogOutputCss
            // 
            this.saveFileDialogOutputCss.Filter = "CSS file|*.css";
            // 
            // btBrowseFolder
            // 
            this.btBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowseFolder.Location = new System.Drawing.Point(425, 17);
            this.btBrowseFolder.Name = "btBrowseFolder";
            this.btBrowseFolder.Size = new System.Drawing.Size(75, 23);
            this.btBrowseFolder.TabIndex = 8;
            this.btBrowseFolder.Text = "Browse";
            this.btBrowseFolder.UseVisualStyleBackColor = true;
            this.btBrowseFolder.Click += new System.EventHandler(this.ButtonBrowseFolderClick);
            // 
            // btSelectOutputImageFilePath
            // 
            this.btSelectOutputImageFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelectOutputImageFilePath.Location = new System.Drawing.Point(425, 43);
            this.btSelectOutputImageFilePath.Name = "btSelectOutputImageFilePath";
            this.btSelectOutputImageFilePath.Size = new System.Drawing.Size(75, 23);
            this.btSelectOutputImageFilePath.TabIndex = 9;
            this.btSelectOutputImageFilePath.Text = "Browse";
            this.btSelectOutputImageFilePath.UseVisualStyleBackColor = true;
            this.btSelectOutputImageFilePath.Click += new System.EventHandler(this.ButtonSelectOutputImageFilePathClick);
            // 
            // buttonSelectOutputCSSFilePath
            // 
            this.buttonSelectOutputCSSFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectOutputCSSFilePath.Location = new System.Drawing.Point(425, 69);
            this.buttonSelectOutputCSSFilePath.Name = "buttonSelectOutputCSSFilePath";
            this.buttonSelectOutputCSSFilePath.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectOutputCSSFilePath.TabIndex = 10;
            this.buttonSelectOutputCSSFilePath.Text = "Browse";
            this.buttonSelectOutputCSSFilePath.UseVisualStyleBackColor = true;
            this.buttonSelectOutputCSSFilePath.Click += new System.EventHandler(this.ButtonSelectOutputCssFilePathClick);
            // 
            // groupBoxPaths
            // 
            this.groupBoxPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPaths.Controls.Add(this.label1);
            this.groupBoxPaths.Controls.Add(this.tbInputDirectoryPath);
            this.groupBoxPaths.Controls.Add(this.tbOutputImageFilePath);
            this.groupBoxPaths.Controls.Add(this.buttonSelectOutputCSSFilePath);
            this.groupBoxPaths.Controls.Add(this.btBrowseFolder);
            this.groupBoxPaths.Controls.Add(this.btSelectOutputImageFilePath);
            this.groupBoxPaths.Controls.Add(this.label2);
            this.groupBoxPaths.Controls.Add(this.tbOutputCSSFilePath);
            this.groupBoxPaths.Controls.Add(this.label3);
            this.groupBoxPaths.Location = new System.Drawing.Point(9, 12);
            this.groupBoxPaths.Name = "groupBoxPaths";
            this.groupBoxPaths.Size = new System.Drawing.Size(506, 103);
            this.groupBoxPaths.TabIndex = 11;
            this.groupBoxPaths.TabStop = false;
            this.groupBoxPaths.Text = "Paths";
            // 
            // ndpDistanceBetweenImages
            // 
            this.ndpDistanceBetweenImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ndpDistanceBetweenImages.Location = new System.Drawing.Point(266, 24);
            this.ndpDistanceBetweenImages.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ndpDistanceBetweenImages.Name = "ndpDistanceBetweenImages";
            this.ndpDistanceBetweenImages.Size = new System.Drawing.Size(34, 20);
            this.ndpDistanceBetweenImages.TabIndex = 13;
            this.ndpDistanceBetweenImages.Tag = "";
            // 
            // labelDistanceBetweenImages
            // 
            this.labelDistanceBetweenImages.AutoSize = true;
            this.labelDistanceBetweenImages.Location = new System.Drawing.Point(6, 26);
            this.labelDistanceBetweenImages.Name = "labelDistanceBetweenImages";
            this.labelDistanceBetweenImages.Size = new System.Drawing.Size(132, 13);
            this.labelDistanceBetweenImages.TabIndex = 14;
            this.labelDistanceBetweenImages.Text = "Distance between images:";
            // 
            // rbRectangularLayout
            // 
            this.rbRectangularLayout.AutoSize = true;
            this.rbRectangularLayout.Enabled = false;
            this.rbRectangularLayout.Location = new System.Drawing.Point(6, 88);
            this.rbRectangularLayout.Name = "rbRectangularLayout";
            this.rbRectangularLayout.Size = new System.Drawing.Size(83, 17);
            this.rbRectangularLayout.TabIndex = 15;
            this.rbRectangularLayout.Text = "Rectangular";
            this.rbRectangularLayout.UseVisualStyleBackColor = true;
            this.rbRectangularLayout.CheckedChanged += new System.EventHandler(this.RadioButtonRectangularLayoutCheckedChanged);
            // 
            // groupBoxLayout
            // 
            this.groupBoxLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxLayout.Controls.Add(this.labelSprites);
            this.groupBoxLayout.Controls.Add(this.labelX);
            this.groupBoxLayout.Controls.Add(this.ndpImagesInColumn);
            this.groupBoxLayout.Controls.Add(this.ndpImagesInRow);
            this.groupBoxLayout.Controls.Add(this.rbVerticalLayout);
            this.groupBoxLayout.Controls.Add(this.rbAutomaticLayout);
            this.groupBoxLayout.Controls.Add(this.rbHorizontalLayout);
            this.groupBoxLayout.Controls.Add(this.rbRectangularLayout);
            this.groupBoxLayout.Location = new System.Drawing.Point(9, 121);
            this.groupBoxLayout.Name = "groupBoxLayout";
            this.groupBoxLayout.Size = new System.Drawing.Size(183, 160);
            this.groupBoxLayout.TabIndex = 16;
            this.groupBoxLayout.TabStop = false;
            this.groupBoxLayout.Text = "Layout";
            // 
            // labelSprites
            // 
            this.labelSprites.AutoSize = true;
            this.labelSprites.Enabled = false;
            this.labelSprites.Location = new System.Drawing.Point(137, 113);
            this.labelSprites.Name = "labelSprites";
            this.labelSprites.Size = new System.Drawing.Size(40, 13);
            this.labelSprites.TabIndex = 22;
            this.labelSprites.Text = "images";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Enabled = false;
            this.labelX.Location = new System.Drawing.Point(66, 113);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(12, 13);
            this.labelX.TabIndex = 21;
            this.labelX.Text = "x";
            // 
            // ndpImagesInColumn
            // 
            this.ndpImagesInColumn.BackColor = System.Drawing.SystemColors.Control;
            this.ndpImagesInColumn.Enabled = false;
            this.ndpImagesInColumn.Location = new System.Drawing.Point(80, 111);
            this.ndpImagesInColumn.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ndpImagesInColumn.Name = "ndpImagesInColumn";
            this.ndpImagesInColumn.Size = new System.Drawing.Size(51, 20);
            this.ndpImagesInColumn.TabIndex = 20;
            // 
            // ndpImagesInRow
            // 
            this.ndpImagesInRow.BackColor = System.Drawing.SystemColors.Window;
            this.ndpImagesInRow.Location = new System.Drawing.Point(11, 111);
            this.ndpImagesInRow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ndpImagesInRow.Name = "ndpImagesInRow";
            this.ndpImagesInRow.ReadOnly = true;
            this.ndpImagesInRow.Size = new System.Drawing.Size(49, 20);
            this.ndpImagesInRow.TabIndex = 19;
            this.ndpImagesInRow.ValueChanged += new System.EventHandler(this.NumericUpDownImagesInRowValueChanged);
            // 
            // rbVerticalLayout
            // 
            this.rbVerticalLayout.AutoSize = true;
            this.rbVerticalLayout.Enabled = false;
            this.rbVerticalLayout.Location = new System.Drawing.Point(6, 65);
            this.rbVerticalLayout.Name = "rbVerticalLayout";
            this.rbVerticalLayout.Size = new System.Drawing.Size(60, 17);
            this.rbVerticalLayout.TabIndex = 18;
            this.rbVerticalLayout.TabStop = true;
            this.rbVerticalLayout.Text = "Vertical";
            this.rbVerticalLayout.UseVisualStyleBackColor = true;
            this.rbVerticalLayout.CheckedChanged += new System.EventHandler(this.RadioButtonLayoutCheckedChanged);
            // 
            // rbAutomaticLayout
            // 
            this.rbAutomaticLayout.AutoSize = true;
            this.rbAutomaticLayout.Checked = true;
            this.rbAutomaticLayout.Location = new System.Drawing.Point(6, 19);
            this.rbAutomaticLayout.Name = "rbAutomaticLayout";
            this.rbAutomaticLayout.Size = new System.Drawing.Size(72, 17);
            this.rbAutomaticLayout.TabIndex = 17;
            this.rbAutomaticLayout.TabStop = true;
            this.rbAutomaticLayout.Text = "Automatic";
            this.rbAutomaticLayout.UseVisualStyleBackColor = true;
            this.rbAutomaticLayout.CheckedChanged += new System.EventHandler(this.RadioButtonLayoutCheckedChanged);
            // 
            // rbHorizontalLayout
            // 
            this.rbHorizontalLayout.AutoSize = true;
            this.rbHorizontalLayout.Enabled = false;
            this.rbHorizontalLayout.Location = new System.Drawing.Point(6, 42);
            this.rbHorizontalLayout.Name = "rbHorizontalLayout";
            this.rbHorizontalLayout.Size = new System.Drawing.Size(72, 17);
            this.rbHorizontalLayout.TabIndex = 16;
            this.rbHorizontalLayout.Text = "Horizontal";
            this.rbHorizontalLayout.UseVisualStyleBackColor = true;
            this.rbHorizontalLayout.CheckedChanged += new System.EventHandler(this.RadioButtonLayoutCheckedChanged);
            // 
            // groupBoxDistances
            // 
            this.groupBoxDistances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDistances.Controls.Add(this.ndpMarginWidth);
            this.groupBoxDistances.Controls.Add(this.labelMarginWidth);
            this.groupBoxDistances.Controls.Add(this.labelDistanceBetweenImages);
            this.groupBoxDistances.Controls.Add(this.ndpDistanceBetweenImages);
            this.groupBoxDistances.Location = new System.Drawing.Point(198, 121);
            this.groupBoxDistances.Name = "groupBoxDistances";
            this.groupBoxDistances.Size = new System.Drawing.Size(317, 126);
            this.groupBoxDistances.TabIndex = 17;
            this.groupBoxDistances.TabStop = false;
            this.groupBoxDistances.Text = "Distances";
            // 
            // ndpMarginWidth
            // 
            this.ndpMarginWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ndpMarginWidth.Location = new System.Drawing.Point(266, 54);
            this.ndpMarginWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ndpMarginWidth.Name = "ndpMarginWidth";
            this.ndpMarginWidth.Size = new System.Drawing.Size(34, 20);
            this.ndpMarginWidth.TabIndex = 16;
            // 
            // labelMarginWidth
            // 
            this.labelMarginWidth.AutoSize = true;
            this.labelMarginWidth.Location = new System.Drawing.Point(6, 54);
            this.labelMarginWidth.Name = "labelMarginWidth";
            this.labelMarginWidth.Size = new System.Drawing.Size(70, 13);
            this.labelMarginWidth.TabIndex = 15;
            this.labelMarginWidth.Text = "Margin width:";
            // 
            // progressWork
            // 
            this.progressWork.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressWork.Location = new System.Drawing.Point(198, 252);
            this.progressWork.Name = "progressWork";
            this.progressWork.Size = new System.Drawing.Size(230, 23);
            this.progressWork.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressWork.TabIndex = 18;
            this.progressWork.Visible = false;
            // 
            // SpritesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 287);
            this.Controls.Add(this.progressWork);
            this.Controls.Add(this.groupBoxDistances);
            this.Controls.Add(this.groupBoxLayout);
            this.Controls.Add(this.groupBoxPaths);
            this.Controls.Add(this.btGenerate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SpritesForm";
            this.Text = "SpriteGenerator";
            this.Load += new System.EventHandler(this.SpritesFormLoad);
            this.groupBoxPaths.ResumeLayout(false);
            this.groupBoxPaths.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpDistanceBetweenImages)).EndInit();
            this.groupBoxLayout.ResumeLayout(false);
            this.groupBoxLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpImagesInColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ndpImagesInRow)).EndInit();
            this.groupBoxDistances.ResumeLayout(false);
            this.groupBoxDistances.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ndpMarginWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbInputDirectoryPath;
        private System.Windows.Forms.TextBox tbOutputImageFilePath;
        private System.Windows.Forms.TextBox tbOutputCSSFilePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialogOutputImage;
        private System.Windows.Forms.SaveFileDialog saveFileDialogOutputCss;
        private System.Windows.Forms.Button btBrowseFolder;
        private System.Windows.Forms.Button btSelectOutputImageFilePath;
        private System.Windows.Forms.Button buttonSelectOutputCSSFilePath;
        private System.Windows.Forms.GroupBox groupBoxPaths;
        private System.Windows.Forms.NumericUpDown ndpDistanceBetweenImages;
        private System.Windows.Forms.Label labelDistanceBetweenImages;
        private System.Windows.Forms.RadioButton rbRectangularLayout;
        private System.Windows.Forms.GroupBox groupBoxLayout;
        private System.Windows.Forms.RadioButton rbHorizontalLayout;
        private System.Windows.Forms.GroupBox groupBoxDistances;
        private System.Windows.Forms.RadioButton rbAutomaticLayout;
        private System.Windows.Forms.RadioButton rbVerticalLayout;
        private System.Windows.Forms.NumericUpDown ndpMarginWidth;
        private System.Windows.Forms.Label labelMarginWidth;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.NumericUpDown ndpImagesInColumn;
        private System.Windows.Forms.NumericUpDown ndpImagesInRow;
        private System.Windows.Forms.Label labelSprites;
        private System.Windows.Forms.ProgressBar progressWork;
    }
}


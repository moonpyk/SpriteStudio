using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SpriteGenerator
{
    public partial class SpritesForm : Form
    {
        private readonly LayoutProperties _layoutProp = new LayoutProperties();
        private readonly GenerationConditions _ready = new GenerationConditions();

        public SpritesForm()
        {
            InitializeComponent();

            _ready.PropertyChanged += delegate
            {
                btGenerate.Enabled = _ready.IsOK;
            };
        }

        protected bool Working
        {
            set
            {
                progressWork.Visible = value;
            }
        }

        protected string WorkingMessage
        {
            set
            {
                lbStatusMessage.Text = value;
            }
        }

        private void SpritesForm_Load(object sender, EventArgs e) {
            _layoutProp.Layout = SpriteLayoutUtil.FromString(rbAutomaticLayout.Text);

            LoadLastSettings();
        }



        // Generate button click event. Start generating output image and CSS file.
        private void BtGenerate_Click(object sender, EventArgs e)
        {
            _layoutProp.OutputSpriteFilePath = tbOutputImageFilePath.Text;
            _layoutProp.OutputCssFilePath = tbOutputCSSFilePath.Text;
            _layoutProp.DistanceBetweenImages = (int)ndpDistanceBetweenImages.Value;
            _layoutProp.MarginWidth = (int)ndpMarginWidth.Value;

            Working = true;
            WorkingMessage = "Generating sprite...";

            Task.Factory.StartNew(() =>
            {
                using (var sprite = new Sprite(_layoutProp))
                {
                    sprite.Create();
                }
            }).ContinueWith(o =>
            {
                var settings = Properties.Settings.Default;

                settings.LastDirectory = tbInputDirectoryPath.Text;
                settings.Save();

                Working = false;
                WorkingMessage = "Spriting done";

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Browse input images folder.
        private void BtBrowseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            CheckImagesDirectory(false);
        }

        // Select output image file path.
        private void BtSelectOutputImageFilePath_Click(object sender, EventArgs e)
        {
            saveFileDialogOutputImage.ShowDialog();

            if (string.IsNullOrEmpty(saveFileDialogOutputImage.FileName))
            {
                return;
            }

            CheckOutputImagePath(false);
        }

        // Select output CSS file path.
        private void BtSelectOutputCssFilePath_Click(object sender, EventArgs e)
        {
            saveFileDialogOutputCss.ShowDialog();

            if (string.IsNullOrEmpty(saveFileDialogOutputCss.FileName))
            {
                return;
            }

            CheckOutputCssPath(false);
        }

        // Rectangular layout radiobutton checked change.
        private void RbRectangularLayoutChecked_Changed(object sender, EventArgs e)
        {
            RbLayoutChecked_Changed(sender, e);

            // Enabling numericupdowns to select layout dimension.
            if (rbRectangularLayout.Checked)
            {
                ndpImagesInRow.Enabled = true;
                ndpImagesInColumn.Enabled = true;
                labelX.Enabled = true;
                lbSprites.Enabled = true;
                ndpImagesInRow.Maximum = _layoutProp.InputFilePaths.Count;
            }
            else // Disabling numericupdowns
            {
                ndpImagesInRow.Enabled = false;
                ndpImagesInColumn.Enabled = false;
                labelX.Enabled = false;
                lbSprites.Enabled = false;
            }
        }

        // Checked change event for all layout radiobutton.
        private void RbLayoutChecked_Changed(object sender, EventArgs e)
        {
            var rd = sender as RadioButton;

            // Setting layout field value.
            if (rd != null && rd.Checked)
            {
                _layoutProp.Layout = SpriteLayoutUtil.FromString(rd.Text);
            }
        }

        // Sprites in row numericupdown value changed event
        private void NdpImagesInRowValue_Changed(object sender, EventArgs e)
        {
            var numberOfFiles = _layoutProp.InputFilePaths.Count;

            //Setting sprites in column numericupdown value
            ndpImagesInColumn.Minimum = numberOfFiles / (int)ndpImagesInRow.Value;
            ndpImagesInColumn.Minimum += (numberOfFiles % (int)ndpImagesInRow.Value) > 0 ? 1 : 0;
            ndpImagesInColumn.Maximum = ndpImagesInColumn.Minimum;

            _layoutProp.ImagesInRow = (int)ndpImagesInRow.Value;
            _layoutProp.ImagesInColumn = (int)ndpImagesInColumn.Value;
        }

        private void MnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadLastSettings()
        {
            var settings = Properties.Settings.Default;

            if (string.IsNullOrEmpty(settings.LastDirectory))
            {
                return;
            }

            tbInputDirectoryPath.Text = folderBrowserDialog.SelectedPath = settings.LastDirectory;

            if (!CheckImagesDirectory(true))
            {
                tbInputDirectoryPath.Text = folderBrowserDialog.SelectedPath = settings.LastDirectory = "";
            }
        }

        private bool CheckOutputImagePath(bool beSilent)
        {
            if (_ready.OutputCssPathOK && tbOutputCSSFilePath.Text[0] != saveFileDialogOutputImage.FileName[0])
            {
                if(!beSilent) {
                    MessageBox.Show("Output image and CSS file must be on the same drive.");                    
                }
                _ready.OutputImagePathOK = false;
                return false;
            }

            _ready.OutputImagePathOK = true;
            tbOutputImageFilePath.Text = saveFileDialogOutputImage.FileName;

            return true;
        }

        private bool CheckOutputCssPath(bool beSilent)
        {
            if (_ready.OutputImagePathOK && tbOutputImageFilePath.Text[0] != saveFileDialogOutputCss.FileName[0])
            {
                if (!beSilent)
                {
                    MessageBox.Show("Output image and CSS file must be on the same drive.");
                }
                _ready.OutputCssPathOK = false;
                return false;
            }

            tbOutputCSSFilePath.Text = saveFileDialogOutputCss.FileName;
            _ready.OutputCssPathOK = true;
            return true;
        }

        private bool CheckImagesDirectory(bool beSilent)
        {
            string[] filters = {
                ".png", ".jpg", ".jpeg", ".gif"
            };

            _layoutProp.InputFilePaths = (
                from filter in filters
                from file in Directory.GetFiles(folderBrowserDialog.SelectedPath)
                where file.EndsWith(filter)
                select file
            ).ToList();

            // If there is no file with the enabled formats in the choosen directory.
            if (_layoutProp.InputFilePaths.Count == 0)
            {
                if (!beSilent)
                {
                    MessageBox.Show("This directory does not contain image files.");
                }
                _ready.ImagePathOK = false;
                return false;
            }

            // If there are files with the enabled formats in the choosen directory.
            tbInputDirectoryPath.Text = folderBrowserDialog.SelectedPath;

            _ready.ImagePathOK = true;

            rbAutomaticLayout.Checked = true;

            bool canVertical = false,
                 canHorizontal = false;

            var sched = TaskScheduler.FromCurrentSynchronizationContext();

            Working = true;
            WorkingMessage = "Scanning directory for images...";

            // Maybe long operation, depends of how many images you have
            Task.Factory.StartNew(() =>
            {
                int width, height;

                using (var firstImage = Image.FromFile(_layoutProp.InputFilePaths[0]))
                {
                    width = firstImage.Width;
                    height = firstImage.Height;
                }
#if PARA
                var allImages = _layoutProp.InputFilePaths.AsParallel()
                    .Select(Image.FromFile)
                    .ToList();
#else
                var allImages = _layoutProp.InputFilePaths
                    .Select(Image.FromFile)
                    .ToList();
#endif

                // Horizontal layout radiobutton is enabled only when all image heights are the same.                    
                canHorizontal = allImages.All(_ => _.Height == height);

                // Vertical layout radiobutton is enabled only when all image widths are the same.
                canVertical = allImages.All(_ => _.Width == width);

#if PARA
                allImages.AsParallel().ForAll(i => i.Dispose());
#else
                allImages.ForEach(i => i.Dispose());
#endif
            })
            .ContinueWith(obj =>
            {
                rbHorizontalLayout.Enabled = canHorizontal;
                rbVerticalLayout.Enabled = canVertical;

                // Rectangular layout radiobutton is enabled only when all image heights and all image widths are the same.
                rbRectangularLayout.Enabled = canHorizontal && canVertical;
                rbAutomaticLayout.Enabled = !rbRectangularLayout.Enabled;

                // Setting rectangular layout dimensions.
                if (rbRectangularLayout.Enabled)
                {
                    ndpImagesInRow.Minimum = 1;
                    ndpImagesInRow.Maximum = _layoutProp.InputFilePaths.Count;
                    _layoutProp.ImagesInRow = (int)ndpImagesInRow.Value;
                    _layoutProp.ImagesInColumn = (int)ndpImagesInColumn.Value;
                }
                else
                {
                    ndpImagesInRow.Minimum = 0;
                    ndpImagesInColumn.Minimum = 0;
                    ndpImagesInRow.Value = 0;
                    ndpImagesInColumn.Value = 0;
                }

                Working = false;
                WorkingMessage = string.Empty;
            }, sched);

            return true;
        }
    }
}

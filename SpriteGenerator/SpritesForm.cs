using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpriteGenerator.Properties;

namespace SpriteGenerator
{
    public partial class SpritesForm : Form
    {
        private readonly LayoutProperties _layoutProp = new LayoutProperties();
        private readonly GenerationConditions _ready = new GenerationConditions();
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public SpritesForm()
        {
            InitializeComponent();
            WorkingMessage = "";

            _ready.PropertyChanged += delegate
            {
                btRefresh.Enabled = mnRefresh.Enabled = _ready.ImagePathOK;
                btGenerate.Enabled = mnGenerate.Enabled = _ready.IsOK;
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

        private void SpritesForm_Load(object sender, EventArgs e)
        {
            _layoutProp.Layout = SpriteLayoutUtil.FromString(rbAutomaticLayout.Text);

            LoadLastSettings();
        }

        private void SpritesForm_DragEnter(object sender, DragEventArgs e)
        {
            var dir = HandleDirectoryDrag(e);

            if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void SpritesForm_DragDrop(object sender, DragEventArgs e)
        {
            var dir = HandleDirectoryDrag(e);

            if (string.IsNullOrEmpty(dir))
            {
                return;
            }
            tbInputDirectoryPath.Text = fbDialog.SelectedPath = dir;
            ValidateImagesDirectory(false);
        }

        // Generate button click event. Start generating output image and CSS file.
        private void BtGenerate_Click(object sender, EventArgs e)
        {
            _layoutProp.OutputSpriteFilePath = tbOutputImageFilePath.Text;
            _layoutProp.OutputCssFilePath = tbOutputCSSFilePath.Text;
            _layoutProp.Padding = (int)ndpDistanceBetweenImages.Value;
            _layoutProp.Margin = (int)ndpMarginWidth.Value;

            Working = true;
            WorkingMessage = "Generating sprite...";

            Task.Factory.StartNew(() =>
            {
                _stopwatch.Start();
                using (var sprite = new Sprite(_layoutProp))
                {
                    sprite.Generate();
                }
                _stopwatch.Stop();
            }).ContinueWith(o =>
            {
                var s = Settings.Default;

                s.LastDirectory = tbInputDirectoryPath.Text;
                s.LastOutputCssFile = tbOutputCSSFilePath.Text;
                s.LastOutputImageFile = tbOutputImageFilePath.Text;
                s.Save();

                Working = false;
                WorkingMessage = string.Format("Spriting done [{0}]", _stopwatch.Elapsed);
                _stopwatch.Reset();

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Browse input images folder.
        private void BtBrowseFolder_Click(object sender, EventArgs e)
        {
            if (fbDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ValidateImagesDirectory(false);
        }

        // Select output image file path.
        private void BtSelectOutputImageFilePath_Click(object sender, EventArgs e)
        {
            sfdOutputImage.ShowDialog();

            if (string.IsNullOrEmpty(sfdOutputImage.FileName))
            {
                return;
            }

            ValidateOutputImagePath(false);
        }

        // Select output CSS file path.
        private void BtSelectOutputCssFilePath_Click(object sender, EventArgs e)
        {
            sfdOutputCss.ShowDialog();

            if (string.IsNullOrEmpty(sfdOutputCss.FileName))
            {
                return;
            }

            ValidateOutputCssPath(false);
        }

        private void BtRefresh_Click(object sender, EventArgs e)
        {
            ValidateImagesDirectory(false);
        }

        private void BtSquare_Click(object sender, EventArgs e)
        {
            ndpImagesInRow.Value = (int)Math.Round(
                Math.Sqrt(_layoutProp.InputFilePaths.Count),
                0
            );
        }

        // Rectangular layout radiobutton checked change.
        private void RbRectangularLayoutChecked_Changed(object sender, EventArgs e)
        {
            RbLayoutChecked_Changed(sender, e);

            var enableAutomaticProps = rbRectangularLayout.Checked;

            ndpImagesInRow.Enabled =
                ndpImagesInColumn.Enabled =
                labelX.Enabled =
                lbSprites.Enabled =
                btSquare.Enabled = enableAutomaticProps;

            // Enabling numericupdowns to select layout dimension.
            if (rbRectangularLayout.Checked)
            {
                ndpImagesInRow.Maximum = _layoutProp.InputFilePaths.Count;
            }
        }

        // Checked change event for all layout radiobutton.
        private void RbLayoutChecked_Changed(object sender, EventArgs e)
        {
            var rd = sender as RadioButton;

            // Setting layout field value.
            if (rd == null || !rd.Checked)
            {
                return;
            }

            _ready.IsLayoutOK = true;
            _layoutProp.Layout = SpriteLayoutUtil.FromString(rd.Text);
        }

        // Sprites in row numericupdown value changed event
        private void NdpImagesInRowValue_Changed(object sender, EventArgs e)
        {
            var numberOfFiles = _layoutProp.InputFilePaths.Count;

            // Setting sprites in column numericupdown value
            var imagesInRow = (int)ndpImagesInRow.Value;

            if (imagesInRow > 0)
            {
                // ReSharper disable PossibleLossOfFraction : Wanted behaviour
                ndpImagesInColumn.Minimum = numberOfFiles / imagesInRow;
                // ReSharper restore PossibleLossOfFraction
                ndpImagesInColumn.Minimum += (numberOfFiles % imagesInRow) > 0
                    ? 1
                    : 0;
            }
            else
            {
                ndpImagesInColumn.Minimum = 0;
            }

            ndpImagesInColumn.Maximum = ndpImagesInColumn.Minimum;

            _layoutProp.ImagesInRow = imagesInRow;
            _layoutProp.ImagesInColumn = (int)ndpImagesInColumn.Value;
        }

        private void MnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadLastSettings()
        {
            var settings = Settings.Default;

            if (!string.IsNullOrEmpty(settings.LastDirectory))
            {
                tbInputDirectoryPath.Text = fbDialog.SelectedPath
                    = settings.LastDirectory;

                if (!ValidateImagesDirectory(true))
                {
                    tbInputDirectoryPath.Text = fbDialog.SelectedPath = settings.LastDirectory = "";
                }
            }

            if (!string.IsNullOrEmpty(settings.LastOutputCssFile))
            {
                tbOutputCSSFilePath.Text = sfdOutputCss.FileName
                    = settings.LastOutputCssFile;

                if (!ValidateOutputCssPath(true))
                {
                    tbOutputCSSFilePath.Text = sfdOutputCss.FileName
                        = settings.LastOutputCssFile = "";
                }
            }

            if (!string.IsNullOrEmpty(settings.LastOutputImageFile))
            {
                tbOutputImageFilePath.Text = sfdOutputImage.FileName
                    = settings.LastOutputImageFile;

                if (!ValidateOutputImagePath(true))
                {
                    tbOutputImageFilePath.Text = sfdOutputImage.FileName
                        = settings.LastOutputImageFile = "";
                }
            }
        }

        private static bool ValidateSameDrive(string a, string b)
        {
            if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b))
            {
                return false;
            }

            return a[0] == b[0];
        }

        private bool ValidateOutputImagePath(bool beSilent)
        {
            if (_ready.OutputCssPathOK && !ValidateSameDrive(tbOutputCSSFilePath.Text, sfdOutputImage.FileName))
            {
                if (!beSilent)
                {
                    MessageBox.Show(Resources.ErrorMessageOutputImageAndOutCssNotSameDrive);
                }
                _ready.OutputImagePathOK = false;
                return false;
            }

            _ready.OutputImagePathOK = true;
            tbOutputImageFilePath.Text = sfdOutputImage.FileName;

            return true;
        }

        private bool ValidateOutputCssPath(bool beSilent)
        {
            if (_ready.OutputImagePathOK && !ValidateSameDrive(tbOutputImageFilePath.Text, sfdOutputCss.FileName))
            {
                if (!beSilent)
                {
                    MessageBox.Show(Resources.ErrorMessageOutputImageAndOutCssNotSameDrive);
                }
                _ready.OutputCssPathOK = false;
                return false;
            }

            tbOutputCSSFilePath.Text = sfdOutputCss.FileName;
            _ready.OutputCssPathOK = true;
            return true;
        }

        private bool ValidateImagesDirectory(bool beSilent)
        {
            string[] filters = {
                ".png", ".jpg", ".jpeg", ".gif"
            };

            try
            {
                _layoutProp.InputFilePaths = (
                    from filter in filters
                    from file in Directory.GetFiles(fbDialog.SelectedPath).AsParallel()
                    where file.EndsWith(filter)
                    select file
                ).ToList();
            }
            catch (Exception ex)
            {
                if (!beSilent)
                {
                    MessageBox.Show(ex.Message);
                }
                return false;
            }

            // If there is no file with the enabled formats in the choosen directory.
            var imageFiles = _layoutProp.InputFilePaths;

            if (imageFiles.Count == 0)
            {
                if (!beSilent)
                {
                    MessageBox.Show(Resources.ErrorDirectoryNoImageFile);
                }
                _ready.ImagePathOK = false;
                return false;
            }

            // If there are files with the enabled formats in the choosen directory.
            tbInputDirectoryPath.Text = fbDialog.SelectedPath;

            _ready.ImagePathOK = _ready.IsLayoutOK = true;

            rbAutomaticLayout.Enabled = rbAutomaticLayout.Checked = true;

            bool canVertical = true,
                 canHorizontal = true;

            var sched = TaskScheduler.FromCurrentSynchronizationContext();

            Working = true;
            WorkingMessage = "Scanning directory for images...";

            // Maybe long operation, depends of how many images you have
            Task.Factory.StartNew(() =>
            {
                _stopwatch.Start();

                using (var scan = Scanner.ScanImages(new List<string>(imageFiles)))
                {
                    var av = scan.AvailableLayouts;
                    canHorizontal = av.Contains(SpriteLayout.Horizontal);
                    canVertical = av.Contains(SpriteLayout.Vertical);
                }

                _stopwatch.Stop();
            })
            .ContinueWith(obj =>
            {
                rbHorizontalLayout.Enabled = canHorizontal;
                rbVerticalLayout.Enabled = canVertical;

                // Rectangular layout is enabled only when all image heights and all image widths are the same.
                rbRectangularLayout.Enabled = canHorizontal && canVertical;

                // Setting rectangular layout dimensions.
                if (rbRectangularLayout.Enabled)
                {
                    ndpImagesInRow.Minimum = 1;
                    ndpImagesInRow.Maximum = imageFiles.Count;
                    _layoutProp.ImagesInRow = (int)ndpImagesInRow.Value;
                    _layoutProp.ImagesInColumn = (int)ndpImagesInColumn.Value;
                }
                else
                {
                    ndpImagesInRow.Minimum = ndpImagesInColumn.Minimum = 0;
                    ndpImagesInRow.Value = ndpImagesInColumn.Value = 0;
                }

                Working = false;
                WorkingMessage = string.Format("Directory scan done [{0}]", _stopwatch.Elapsed);
                _stopwatch.Reset();

            }, sched);

            return true;
        }

        private static bool IsFileDrop(DragEventArgs e)
        {
            return e.Data.GetDataPresent(DataFormats.FileDrop);
        }

        private static string HandleDirectoryDrag(DragEventArgs e)
        {
            if (!IsFileDrop(e))
            {
                return null;
            }

            var fileDrop = e.Data.GetData(DataFormats.FileDrop) as IList<string>;

            if (fileDrop == null || fileDrop.Count() != 1)
            {
                return null;
            }

            return fileDrop[0];
        }
    }
}

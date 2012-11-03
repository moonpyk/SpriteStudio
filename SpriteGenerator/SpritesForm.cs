using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SpriteGenerator
{
    public partial class SpritesForm : Form
    {
        private readonly bool[] _buttonGenerateEnabled = new bool[3];
        private readonly LayoutProperties _layoutProp = new LayoutProperties();
        public bool Done = false;

        public SpritesForm()
        {
            InitializeComponent();
        }

        private void SpritesFormLoad(object sender, EventArgs e)
        {
            _layoutProp.Layout = SpriteLayoutUtil.FromString(rbAutomaticLayout.Text);

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

        // Generate button click event. Start generating output image and CSS file.
        private void ButtonGenerateClick(object sender, EventArgs e)
        {
            _layoutProp.OutputSpriteFilePath = tbOutputImageFilePath.Text;
            _layoutProp.OutputCssFilePath = tbOutputCSSFilePath.Text;
            _layoutProp.DistanceBetweenImages = (int)ndpDistanceBetweenImages.Value;
            _layoutProp.MarginWidth = (int)ndpMarginWidth.Value;

            progressWork.Visible = true;

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

                progressWork.Visible = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Browse input images folder.
        private void ButtonBrowseFolderClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            CheckImagesDirectory(false);
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
            ).ToArray();

            // If there is no file with the enabled formats in the choosen directory.
            if (_layoutProp.InputFilePaths.Length == 0)
            {
                if (!beSilent)
                {
                    MessageBox.Show("This directory does not contain image files.");
                }
                return false;
            }

            // If there are files with the enabled formats in the choosen directory.
            tbInputDirectoryPath.Text = folderBrowserDialog.SelectedPath;

            _buttonGenerateEnabled[0] = true;
            btGenerate.Enabled = _buttonGenerateEnabled.All(element => element);

            rbAutomaticLayout.Checked = true;

            int width, height;

            using (var firstImage = Image.FromFile(_layoutProp.InputFilePaths[0]))
            {
                width = firstImage.Width;
                height = firstImage.Height;
            }

            bool canVertical = false,
                 canHorizontal = false;

            var sched = TaskScheduler.FromCurrentSynchronizationContext();

            progressWork.Visible = true;

            // Maybe long operation, depends of how many images you have
            Task.Factory.StartNew(() =>
            {
#if PARA
                var allImages = _layoutProp.InputFilePaths.AsParallel()
                    .Select(Image.FromFile)
                    .ToList();
#else
                var allImages = _layoutProp.InputFilePaths.Select(Image.FromFile).ToList();
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
                    ndpImagesInRow.Maximum = _layoutProp.InputFilePaths.Length;
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

                progressWork.Visible = false;

            }, sched);

            return true;
        }

        // Select output image file path.
        private void ButtonSelectOutputImageFilePathClick(object sender, EventArgs e)
        {
            saveFileDialogOutputImage.ShowDialog();

            if (string.IsNullOrEmpty(saveFileDialogOutputImage.FileName))
            {
                return;
            }

            if (_buttonGenerateEnabled[2] && tbOutputCSSFilePath.Text[0] != saveFileDialogOutputImage.FileName[0])
            {
                MessageBox.Show("Output image and CSS file must be on the same drive.");
            }
            else
            {
                tbOutputImageFilePath.Text = saveFileDialogOutputImage.FileName;
                _buttonGenerateEnabled[1] = true;
                btGenerate.Enabled = _buttonGenerateEnabled.All(element => element);
            }
        }

        // Select output CSS file path.
        private void ButtonSelectOutputCssFilePathClick(object sender, EventArgs e)
        {
            saveFileDialogOutputCss.ShowDialog();

            if (string.IsNullOrEmpty(saveFileDialogOutputCss.FileName))
            {
                return;
            }

            if (_buttonGenerateEnabled[1] && tbOutputImageFilePath.Text[0] != saveFileDialogOutputCss.FileName[0])
            {
                MessageBox.Show("Output image and CSS file must be on the same drive.");
            }
            else
            {
                tbOutputCSSFilePath.Text = saveFileDialogOutputCss.FileName;
                _buttonGenerateEnabled[2] = true;
                btGenerate.Enabled = _buttonGenerateEnabled.All(_ => _);
            }
        }

        // Rectangular layout radiobutton checked change.
        private void RadioButtonRectangularLayoutCheckedChanged(object sender, EventArgs e)
        {
            RadioButtonLayoutCheckedChanged(sender, e);

            // Enabling numericupdowns to select layout dimension.
            if (rbRectangularLayout.Checked)
            {
                ndpImagesInRow.Enabled = true;
                ndpImagesInColumn.Enabled = true;
                labelX.Enabled = true;
                labelSprites.Enabled = true;
                ndpImagesInRow.Maximum = _layoutProp.InputFilePaths.Length;
            }
            else // Disabling numericupdowns
            {
                ndpImagesInRow.Enabled = false;
                ndpImagesInColumn.Enabled = false;
                labelX.Enabled = false;
                labelSprites.Enabled = false;
            }
        }

        // Checked change event for all layout radiobutton.
        private void RadioButtonLayoutCheckedChanged(object sender, EventArgs e)
        {
            var rd = sender as RadioButton;

            // Setting layout field value.
            if (rd != null && rd.Checked)
            {
                _layoutProp.Layout = SpriteLayoutUtil.FromString(rd.Text);
            }
        }

        // Sprites in row numericupdown value changed event
        private void NumericUpDownImagesInRowValueChanged(object sender, EventArgs e)
        {
            var numberOfFiles = _layoutProp.InputFilePaths.Length;

            //Setting sprites in column numericupdown value
            ndpImagesInColumn.Minimum = numberOfFiles / (int)ndpImagesInRow.Value;
            ndpImagesInColumn.Minimum += (numberOfFiles % (int)ndpImagesInRow.Value) > 0 ? 1 : 0;
            ndpImagesInColumn.Maximum = ndpImagesInColumn.Minimum;

            _layoutProp.ImagesInRow = (int)ndpImagesInRow.Value;
            _layoutProp.ImagesInColumn = (int)ndpImagesInColumn.Value;
        }
    }
}

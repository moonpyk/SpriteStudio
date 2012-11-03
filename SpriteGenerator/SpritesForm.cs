using System;
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
            _layoutProp.Layout = radioButtonAutomaticLayout.Text;
        }

        //Generate button click event. Start generating output image and CSS file.
        private void ButtonGenerateClick(object sender, EventArgs e)
        {
            _layoutProp.OutputSpriteFilePath = textBoxOutputImageFilePath.Text;
            _layoutProp.OutputCssFilePath = textBoxOutputCSSFilePath.Text;
            _layoutProp.DistanceBetweenImages = (int)numericUpDownDistanceBetweenImages.Value;
            _layoutProp.MarginWidth = (int)numericUpDownMarginWidth.Value;

            progressWork.Visible = true;
            Task.Factory.StartNew(() =>
            {
                var sprite = new Sprite(_layoutProp);
                sprite.Create();
            }).ContinueWith(o => Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Browse input images folder.
        private void ButtonBrowseFolderClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] filters = { ".png", ".jpg", ".jpeg", ".gif" };

            _layoutProp.InputFilePaths = (
                from filter in filters
                from file in Directory.GetFiles(folderBrowserDialog.SelectedPath)
                where file.EndsWith(filter)
                select file
            ).ToArray();

            //If there is no file with the enabled formats in the choosen directory.
            if (_layoutProp.InputFilePaths.Length == 0)
            {
                MessageBox.Show("This directory does not contain image files.");
            }

            //If there are files with the enabled formats in the choosen directory.
            else
            {
                textBoxInputDirectoryPath.Text = folderBrowserDialog.SelectedPath;

                _buttonGenerateEnabled[0] = true;
                buttonGenerate.Enabled = _buttonGenerateEnabled.All(element => element);

                radioButtonAutomaticLayout.Checked = true;

                var width = Image.FromFile(_layoutProp.InputFilePaths[0]).Width;
                var height = Image.FromFile(_layoutProp.InputFilePaths[0]).Height;

                var canVertical = false;
                var canHorizontal = false;

                var sched = TaskScheduler.FromCurrentSynchronizationContext();

                progressWork.Visible = true;

                // Maybe long operation, depends of how many images you have
                Task.Factory.StartNew(() =>
                {
                    // Horizontal layout radiobutton is enabled only when all image heights are the same.
                    canHorizontal = _layoutProp.InputFilePaths.All(file => Image.FromFile(file).Height == height);

                    // Vertical layout radiobutton is enabled only when all image widths are the same.
                    canVertical = _layoutProp.InputFilePaths.All(file => Image.FromFile(file).Width == width);
                })
                .ContinueWith(obj =>
                {
                    radioButtonHorizontalLayout.Enabled = canHorizontal;
                    radioButtonVerticalLayout.Enabled = canVertical;

                    // Rectangular layout radiobutton is enabled only when all image heights and all image widths are the same.
                    radioButtonRectangularLayout.Enabled = radioButtonHorizontalLayout.Enabled &&
                                                           radioButtonVerticalLayout.Enabled;

                    //Setting rectangular layout dimensions.
                    if (radioButtonRectangularLayout.Enabled)
                    {
                        numericUpDownImagesInRow.Minimum = 1;
                        numericUpDownImagesInRow.Maximum = _layoutProp.InputFilePaths.Length;
                        _layoutProp.ImagesInRow = (int)numericUpDownImagesInRow.Value;
                        _layoutProp.ImagesInColumn = (int)numericUpDownImagesInColumn.Value;
                    }
                    else
                    {
                        numericUpDownImagesInRow.Minimum = 0;
                        numericUpDownImagesInColumn.Minimum = 0;
                        numericUpDownImagesInRow.Value = 0;
                        numericUpDownImagesInColumn.Value = 0;
                    }

                    progressWork.Visible = false;
                }, sched);
            }
        }

        // Select output image file path.
        private void ButtonSelectOutputImageFilePathClick(object sender, EventArgs e)
        {
            saveFileDialogOutputImage.ShowDialog();
            if (saveFileDialogOutputImage.FileName == "")
            {
                return;
            }

            if (_buttonGenerateEnabled[2] && textBoxOutputCSSFilePath.Text[0] != saveFileDialogOutputImage.FileName[0])
            {
                MessageBox.Show("Output image and CSS file must be on the same drive.");
            }
            else
            {
                textBoxOutputImageFilePath.Text = saveFileDialogOutputImage.FileName;
                _buttonGenerateEnabled[1] = true;
                buttonGenerate.Enabled = _buttonGenerateEnabled.All(element => element);
            }
        }

        // Select output CSS file path.
        private void ButtonSelectOutputCssFilePathClick(object sender, EventArgs e)
        {
            saveFileDialogOutputCss.ShowDialog();
            if (saveFileDialogOutputCss.FileName == "")
            {
                return;
            }

            if (_buttonGenerateEnabled[1] &&
                textBoxOutputImageFilePath.Text[0] != saveFileDialogOutputCss.FileName[0])
            {
                MessageBox.Show("Output image and CSS file must be on the same drive.");
            }
            else
            {
                textBoxOutputCSSFilePath.Text = saveFileDialogOutputCss.FileName;
                _buttonGenerateEnabled[2] = true;
                buttonGenerate.Enabled = _buttonGenerateEnabled.All(_ => _);
            }
        }

        // Rectangular layout radiobutton checked change.
        private void RadioButtonRectangularLayoutCheckedChanged(object sender, EventArgs e)
        {
            RadioButtonLayoutCheckedChanged(sender, e);

            // Enabling numericupdowns to select layout dimension.
            if (radioButtonRectangularLayout.Checked)
            {
                numericUpDownImagesInRow.Enabled = true;
                numericUpDownImagesInColumn.Enabled = true;
                labelX.Enabled = true;
                labelSprites.Enabled = true;
                numericUpDownImagesInRow.Maximum = _layoutProp.InputFilePaths.Length;
            }
            else // Disabling numericupdowns
            {
                numericUpDownImagesInRow.Enabled = false;
                numericUpDownImagesInColumn.Enabled = false;
                labelX.Enabled = false;
                labelSprites.Enabled = false;
            }
        }

        // Checked change event for all layout radiobutton.
        private void RadioButtonLayoutCheckedChanged(object sender, EventArgs e)
        {
            var rd = sender as RadioButton;

            //Setting layout field value.
            if (rd != null && rd.Checked)
            {
                _layoutProp.Layout = rd.Text;
            }
        }

        // Sprites in row numericupdown value changed event
        private void NumericUpDownImagesInRowValueChanged(object sender, EventArgs e)
        {
            var numberOfFiles = _layoutProp.InputFilePaths.Length;

            //Setting sprites in column numericupdown value
            numericUpDownImagesInColumn.Minimum = numberOfFiles / (int)numericUpDownImagesInRow.Value;
            numericUpDownImagesInColumn.Minimum += (numberOfFiles % (int)numericUpDownImagesInRow.Value) > 0 ? 1 : 0;
            numericUpDownImagesInColumn.Maximum = numericUpDownImagesInColumn.Minimum;

            _layoutProp.ImagesInRow = (int)numericUpDownImagesInRow.Value;
            _layoutProp.ImagesInColumn = (int)numericUpDownImagesInColumn.Value;
        }
    }
}

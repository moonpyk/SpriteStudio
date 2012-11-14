using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;
using Newtonsoft.Json;
using Ookii.Dialogs.Wpf;
using SpriteGenerator;
using SpriteStudio.Properties;

namespace SpriteStudio
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand RefreshRoutedCommand = new RoutedCommand();
        public static RoutedCommand GenerateRoutedCommand = new RoutedCommand();
        public static RoutedCommand ExitRoutedCommand = new RoutedCommand();

        private readonly GenerationConditions _ready = new GenerationConditions();

        private readonly VistaSaveFileDialog _sfdOutputCss = new VistaSaveFileDialog
        {
            Filter = Properties.Resources.OutputCssSaveFilter
        };

        private readonly VistaSaveFileDialog _sfdOutputImage = new VistaSaveFileDialog
        {
            Filter = Properties.Resources.OutputImageSaveFilter
        };

        private readonly Stopwatch _stopwatch = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            Working = false;
            WorkingMessage = string.Empty;

            _layoutProperties.InputFilePaths = new List<string>();

            _ready.PropertyChanged += delegate
            {
                btRefresh.IsEnabled = mnRefresh.IsEnabled = _ready.ImagePathOK;
                btGenerate.IsEnabled = mnGenerate.IsEnabled = _ready.IsOK;
            };
        }

        protected bool Working
        {
            set
            {
                taskbar.ProgressState = value
                    ? TaskbarItemProgressState.Indeterminate
                    : TaskbarItemProgressState.None;

                progressWork.Visibility = value
                    ? Visibility.Visible
                    : Visibility.Hidden;

                progressWork.IsIndeterminate = value;
            }
        }

        protected string WorkingMessage
        {
            set
            {
                lbStatusMessage.Content = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
            // DebugDragEvent(e);

            var dir = HandleDirectoryDrag(e);

            if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
            {
                e.Effects = DragDropEffects.Copy;
                return;
            }

            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            // DebugDragEvent(e);

            var dir = HandleDirectoryDrag(e);

            if (string.IsNullOrEmpty(dir))
            {
                return;
            }

            tbInputDirectoryPath.Text = dir;
            ValidateImagesDirectory(false);
        }

        private void BtBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            var b = new VistaFolderBrowserDialog
            {
                SelectedPath = tbInputDirectoryPath.Text,
            };

            if (b.ShowDialog() != true)
            {
                return;
            }

            tbInputDirectoryPath.Text = b.SelectedPath;

            ValidateImagesDirectory(false);
        }

        private void BtBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            _sfdOutputImage.ShowDialog();

            if (string.IsNullOrEmpty(_sfdOutputImage.FileName))
            {
                return;
            }

            ValidateOutputImagePath(false);
        }

        private void BtBrowseCss_Click(object sender, RoutedEventArgs e)
        {
            _sfdOutputCss.ShowDialog();

            if (string.IsNullOrEmpty(_sfdOutputCss.FileName))
            {
                return;
            }

            ValidateOutputCssPath(false);
        }

        private void BtSquare_Click(object sender, RoutedEventArgs e)
        {
            ndpImagesInRow.Value = (int)Math.Round(
                Math.Sqrt(_layoutProperties.InputFilePaths.Count),
                0
            );
        }

        private void RbLayout_CheckedChanged(object sender, RoutedEventArgs e)
        {
            var rd = sender as RadioButton;

            // Setting layout field value.
            if (rd == null || rd.IsChecked == null || !rd.IsChecked.Value)
            {
                return;
            }

            _ready.IsLayoutOK = true;
            _layoutProperties.Layout = SpriteLayoutUtil.FromString(rd.Content as string);
        }

        private void RbLayoutRectangular_CheckedChanged(object sender, RoutedEventArgs e)
        {
            RbLayout_CheckedChanged(sender, e);

            var enableAutomaticProps = rbLayoutRectangular.IsChecked == true;

            ndpImagesInRow.IsEnabled =
                ndpImagesInColumn.IsEnabled =
                labelX.IsEnabled =
                lbSprites.IsEnabled =
                btSquare.IsEnabled = enableAutomaticProps;

            // Enabling numericupdowns to select layout dimension.
            if (rbLayoutRectangular.IsChecked == true)
            {
                ndpImagesInRow.Maximum =
                    _layoutProperties.InputFilePaths.Count;
            }
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            ValidateImagesDirectory(false);
        }

        private void OnGenerate(object sender, RoutedEventArgs e)
        {
            _layoutProperties.OutputSpriteFilePath = tbOutputImageFilePath.Text;
            _layoutProperties.OutputCssFilePath = tbOutputCSSFilePath.Text;
            _layoutProperties.Padding = (int)ndpPadding.Value;
            _layoutProperties.Margin = (int)ndpMargin.Value;

            Working = true;
            WorkingMessage = "Generating sprite...";

            Task.Factory.StartNew(() =>
            {
                _stopwatch.Start();
                using (var sprite = new Sprite(_layoutProperties))
                {
                    sprite.Generate();
                }
                _stopwatch.Stop();

            }).ContinueWith(o =>
            {
                var s = Settings.Default;

                s.Save();

                Working = false;
                WorkingMessage = string.Format("Spriting done [{0}]", _stopwatch.Elapsed);
                _stopwatch.Reset();

            }, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
            if (_ready.OutputCssPathOK && !ValidateSameDrive(tbOutputCSSFilePath.Text, _sfdOutputImage.FileName))
            {
                if (!beSilent)
                {
                    MessageBox.Show(Properties.Resources.ErrorMessageOutputImageAndOutCssNotSameDrive);
                }
                _ready.OutputImagePathOK = false;
                return false;
            }

            _ready.OutputImagePathOK = true;
            tbOutputImageFilePath.Text = _sfdOutputImage.FileName;

            return true;
        }

        private bool ValidateOutputCssPath(bool beSilent)
        {
            if (_ready.OutputImagePathOK && !ValidateSameDrive(tbOutputImageFilePath.Text, _sfdOutputCss.FileName))
            {
                if (!beSilent)
                {
                    MessageBox.Show(Properties.Resources.ErrorMessageOutputImageAndOutCssNotSameDrive);
                }
                _ready.OutputCssPathOK = false;
                return false;
            }

            tbOutputCSSFilePath.Text = _sfdOutputCss.FileName;
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
                _layoutProperties.InputFilePaths = (
                    from filter in filters
                    from file in Directory.GetFiles(tbInputDirectoryPath.Text).AsParallel()
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
            var imageFiles = _layoutProperties.InputFilePaths;

            if (imageFiles.Count == 0)
            {
                if (!beSilent)
                {
                    MessageBox.Show(Properties.Resources.ErrorDirectoryNoImageFile);
                }
                _ready.ImagePathOK = false;
                return false;
            }

            // If there are files with the enabled formats in the choosen directory.
            _ready.ImagePathOK = _ready.IsLayoutOK = true;

            rbLayoutAutomatic.IsEnabled = true;
            rbLayoutAutomatic.IsChecked = true;

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

                    _layoutProperties.ImagesHeight = scan.ImagesHeight;
                    _layoutProperties.ImagesWidth = scan.ImagesWidth;
                }

                _stopwatch.Stop();
            })
            .ContinueWith(obj =>
            {
                rbLayoutHorizonal.IsEnabled = canHorizontal;
                rbLayoutVertical.IsEnabled = canVertical;

                // Rectangular layout is enabled only when all image heights and all image widths are the same.
                rbLayoutRectangular.IsEnabled = canHorizontal && canVertical;

                // Setting rectangular layout dimensions.
                if (rbLayoutRectangular.IsEnabled)
                {
                    ndpImagesInRow.Minimum = 1;
                    ndpImagesInRow.Maximum = imageFiles.Count;
                    _layoutProperties.ImagesInRow = (int)ndpImagesInRow.Value;
                    _layoutProperties.ImagesInColumn = (int)ndpImagesInColumn.Value;

                    if (ndpImagesInRow.Value == 0)
                    {
                        ndpImagesInRow.Value = 1;
                    }
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
            var data = e.Data;

            return
                data.GetDataPresent(DataFormats.FileDrop) ||
                data.GetDataPresent(DataFormats.StringFormat);
        }

        private static string HandleDirectoryDrag(DragEventArgs e)
        {
            if (!IsFileDrop(e))
            {
                return null;
            }

            var data = e.Data;
            var fileDrop = data.GetData(DataFormats.FileDrop) as IList<string>;

            if (fileDrop != null && fileDrop.Count == 1)
            {
                return fileDrop[0];
            }

            var stringDrop = data.GetData(DataFormats.StringFormat) as string;

            if (stringDrop != null)
            {
                return stringDrop;
            }

            return null;
        }

        private void NdpImagesInRow_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (_layoutProperties.InputFilePaths == null)
            {
                return;
            }

            var numberOfFiles = _layoutProperties.InputFilePaths.Count;

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
            ndpImagesInColumn.Value = ndpImagesInColumn.Minimum;

            _layoutProperties.ImagesInRow = imagesInRow;
            _layoutProperties.ImagesInColumn = (int)ndpImagesInColumn.Value;
        }

        #region Debugging Tools

        [Conditional("DEBUG")]
        private static void DebugDragEvent(DragEventArgs e)
        {
            Debug.WriteLine("----------------");
            foreach (var f in e.Data.GetFormats())
            {
                var data = e.Data.GetData(f);

                try
                {
                    Debug.WriteLine(JsonConvert.SerializeObject(new
                    {
                        Format = f,
                        Data = data
                    }));
                }
                catch (Exception)
                {
                    Debug.WriteLine(JsonConvert.SerializeObject(new
                    {
                        Format = f,
                        Data = data.GetType(),
                    }));
                }
            }
            Debug.WriteLine("----------------");
        }

        #endregion
    }
}

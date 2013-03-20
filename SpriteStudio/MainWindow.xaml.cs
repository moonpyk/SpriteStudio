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
using SpriteStudio.Properties;

namespace SpriteStudio
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static RoutedCommand CommandRefresh = new RoutedCommand();
        public static RoutedCommand CommandGenerate = new RoutedCommand();
        public static RoutedCommand CommandExit = new RoutedCommand();
        public static RoutedCommand CommandTopMost = new RoutedCommand();

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

            LayoutProperties.InputFilePaths = new List<string>();

            _ready.PropertyChanged += delegate
            {
                BtRefresh.IsEnabled = MnRefresh.IsEnabled = _ready.ImagePathOK;
                BtGenerate.IsEnabled = MnGenerate.IsEnabled = _ready.IsOK;
            };
        }

        protected bool Working
        {
            set
            {
                Taskbar.ProgressState = value
                    ? TaskbarItemProgressState.Indeterminate
                    : TaskbarItemProgressState.None;

                ProgressWork.Visibility = value
                    ? Visibility.Visible
                    : Visibility.Hidden;

                ProgressWork.IsIndeterminate = value;

                BtGenerate.IsEnabled = !value && _ready.IsOK;
            }
        }

        protected string WorkingMessage
        {
            set
            {
                LbStatusMessage.Content = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLastSettings();
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

            TbInputDirectoryPath.Text = dir;
            ValidateImagesDirectory(false);
        }

        private void BtBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            var b = new VistaFolderBrowserDialog
            {
                SelectedPath = TbInputDirectoryPath.Text,
            };

            if (b.ShowDialog() != true)
            {
                return;
            }

            TbInputDirectoryPath.Text = b.SelectedPath;

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
            NdpImagesInRow.Value = (int)Math.Round(
                Math.Sqrt(LayoutProperties.InputFilePaths.Count),
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
            LayoutProperties.Layout = SpriteLayoutUtil.FromString(rd.Content as string);
        }

        private void RbLayoutRectangular_CheckedChanged(object sender, RoutedEventArgs e)
        {
            RbLayout_CheckedChanged(sender, e);

            var enableAutomaticProps = RbLayoutRectangular.IsChecked == true;

            NdpImagesInRow.IsEnabled = enableAutomaticProps;

            // Enabling numericupdowns to select layout dimension.
            if (RbLayoutRectangular.IsChecked == true)
            {
                NdpImagesInRow.Maximum =
                    LayoutProperties.InputFilePaths.Count;
            }
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            ValidateImagesDirectory(false);
        }

        private void OnGenerate(object sender, RoutedEventArgs e)
        {
            if (NdpPadding.Value == null || NdpMargin.Value == null)
            {
                return;
            }

            LayoutProperties.OutputSpriteFilePath = TbOutputImageFilePath.Text;
            LayoutProperties.OutputCssFilePath = TbOutputCssFilePath.Text;
            LayoutProperties.Padding = (int)NdpPadding.Value;
            LayoutProperties.Margin = (int)NdpMargin.Value;

            Working = true;
            WorkingMessage = "Generating sprite...";

            Task.Factory.StartNew(() =>
            {
                _stopwatch.Start();
                using (var sprite = new Sprite(LayoutProperties))
                {
                    sprite.Generate();
                }
                _stopwatch.Stop();

            }).ContinueWith(o =>
            {
                var s = Settings.Default;

                s.LastDirectory = TbInputDirectoryPath.Text;
                s.LastOutputCssFile = TbOutputCssFilePath.Text;
                s.LastOutputImageFile = TbOutputImageFilePath.Text;
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

        private void OnChangeTopMost(object sender, ExecutedRoutedEventArgs e)
        {
            MnTopMost.IsChecked = !MnTopMost.IsChecked;
        }

        private void OnTopMostChanged(object sender, RoutedEventArgs e)
        {
            var s = Settings.Default;
            s.WindowAlwaysOnTop = Topmost;

            s.Save();
        }

        private void LoadLastSettings()
        {
            var settings = Settings.Default;

            if (!string.IsNullOrEmpty(settings.LastDirectory))
            {
                TbInputDirectoryPath.Text = settings.LastDirectory;

                if (!ValidateImagesDirectory(true))
                {
                    TbInputDirectoryPath.Text = settings.LastDirectory = "";
                }
            }

            if (!string.IsNullOrEmpty(settings.LastOutputCssFile))
            {
                TbOutputCssFilePath.Text = _sfdOutputCss.FileName = settings.LastOutputCssFile;

                if (!ValidateOutputCssPath(true))
                {
                    TbOutputCssFilePath.Text = _sfdOutputCss.FileName = settings.LastOutputCssFile = "";
                }
            }

            if (!string.IsNullOrEmpty(settings.LastOutputImageFile))
            {
                TbOutputImageFilePath.Text = _sfdOutputImage.FileName = settings.LastOutputImageFile;

                if (!ValidateOutputImagePath(true))
                {
                    TbOutputImageFilePath.Text = _sfdOutputImage.FileName = settings.LastOutputImageFile = "";
                }
            }

            Topmost = settings.WindowAlwaysOnTop;
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
            if (_ready.OutputCssPathOK && !ValidateSameDrive(TbOutputCssFilePath.Text, _sfdOutputImage.FileName))
            {
                if (!beSilent)
                {
                    MessageBox.Show(Properties.Resources.ErrorMessageOutputImageAndOutCssNotSameDrive);
                }
                _ready.OutputImagePathOK = false;
                return false;
            }

            _ready.OutputImagePathOK = true;
            TbOutputImageFilePath.Text = _sfdOutputImage.FileName;

            return true;
        }

        private bool ValidateOutputCssPath(bool beSilent)
        {
            if (_ready.OutputImagePathOK && !ValidateSameDrive(TbOutputImageFilePath.Text, _sfdOutputCss.FileName))
            {
                if (!beSilent)
                {
                    MessageBox.Show(Properties.Resources.ErrorMessageOutputImageAndOutCssNotSameDrive);
                }
                _ready.OutputCssPathOK = false;
                return false;
            }

            TbOutputCssFilePath.Text = _sfdOutputCss.FileName;
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
                LayoutProperties.InputFilePaths = (
                    from filter in filters
                    from file in Directory.GetFiles(TbInputDirectoryPath.Text).AsParallel()
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
            var imageFiles = LayoutProperties.InputFilePaths;

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

            RbLayoutAutomatic.IsEnabled = true;
            RbLayoutAutomatic.IsChecked = true;

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

                    LayoutProperties.ImagesHeight = scan.ImagesHeight;
                    LayoutProperties.ImagesWidth = scan.ImagesWidth;
                }

                _stopwatch.Stop();
            })
            .ContinueWith(obj =>
            {
                RbLayoutHorizonal.IsEnabled = canHorizontal;
                RbLayoutVertical.IsEnabled = canVertical;

                // Rectangular layout is enabled only when all image heights and all image widths are the same.
                RbLayoutRectangular.IsEnabled = canHorizontal && canVertical;

                // Setting rectangular layout dimensions.
                if (RbLayoutRectangular.IsEnabled)
                {
                    NdpImagesInRow.Minimum = 1;
                    NdpImagesInRow.Maximum = imageFiles.Count;

                    if (NdpImagesInRow.Value != null && NdpImagesInColumn.Value != null)
                    {
                        LayoutProperties.ImagesInRow = (int)NdpImagesInRow.Value;
                        LayoutProperties.ImagesInColumn = (int)NdpImagesInColumn.Value;

                        if (NdpImagesInRow.Value == 0)
                        {
                            NdpImagesInRow.Value = 1;
                        }
                    }
                }
                else
                {
                    NdpImagesInRow.Minimum = NdpImagesInColumn.Minimum = 0;
                    NdpImagesInRow.Value = NdpImagesInColumn.Value = 0;
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

            return data.GetData(DataFormats.StringFormat) as string;
        }

        private void NdpImagesInRow_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (LayoutProperties.InputFilePaths == null || NdpImagesInRow.Value == null)
            {
                return;
            }

            var numberOfFiles = LayoutProperties.InputFilePaths.Count;

            // Setting sprites in column numericupdown value
            var imagesInRow = (int)NdpImagesInRow.Value;

            if (imagesInRow > 0)
            {
                // ReSharper disable PossibleLossOfFraction : Wanted behaviour
                NdpImagesInColumn.Minimum = numberOfFiles / imagesInRow;
                // ReSharper restore PossibleLossOfFraction
                NdpImagesInColumn.Minimum += (numberOfFiles % imagesInRow) > 0
                    ? 1
                    : 0;
            }
            else
            {
                NdpImagesInColumn.Minimum = 0;
            }

            NdpImagesInColumn.Maximum = NdpImagesInColumn.Minimum;
            NdpImagesInColumn.Value = NdpImagesInColumn.Minimum;

            LayoutProperties.ImagesInRow = imagesInRow;

            if (NdpImagesInColumn.Value != null)
            {
                LayoutProperties.ImagesInColumn = (int)NdpImagesInColumn.Value;
            }
        }

        #region Debugging Tools

        [Conditional("DEBUG")]
        // ReSharper disable UnusedMember.Local
        private static void DebugDragEvent(DragEventArgs e)
        // ReSharper restore UnusedMember.Local
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

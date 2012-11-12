using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Ookii.Dialogs.Wpf;
using SpriteGenerator;

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

        public MainWindow()
        {
            InitializeComponent();
            Working = false;
            WorkingMessage = string.Empty;
        }

        protected bool Working
        {
            set
            {
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
            // ValidateImagesDirectory(false);
        }

        private void BtBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            var b = new VistaFolderBrowserDialog
            {
                SelectedPath = tbInputDirectoryPath.Text,
            };
            b.ShowDialog();
            tbInputDirectoryPath.Text = b.SelectedPath;
        }

        private void BtBrowseImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtBrowseCss_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RbLayout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(LayoutProperties.ImagesWidth);
        }

        private void OnGenerate(object sender, RoutedEventArgs e)
        {

        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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

        private void TopMost_Checked(object sender, RoutedEventArgs e)
        {
            var me = sender as MenuItem;

            if (me != null)
            {
                Topmost = me.IsChecked;
            }
        }
    }
}

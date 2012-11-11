using System;
using System.Collections.Generic;
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

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Window_Drop(object sender, DragEventArgs e)
        {

        }

        private void BtBrowsePath_Click(object sender, RoutedEventArgs e) {
            var b = new Ookii.Dialogs.Wpf.VistaOpenFileDialog();
            b.ShowDialog();
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

        }

        private void OnGenerate(object sender, RoutedEventArgs e)
        {

        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

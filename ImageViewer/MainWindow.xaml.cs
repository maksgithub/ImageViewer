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
using ImageViewer.Providers;
using ImageViewer.ViewModels;

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainWindowViewModel = new MainWindowViewModel();
            DataContext = _mainWindowViewModel;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var images = ImagesProvider.GetImages();
            await _mainWindowViewModel.AddImagesAsync(images.ToArray());
        }


        private async void MainWindow_OnDrop(object sender, DragEventArgs e)
        {
            string[] droppedFiles = null;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                droppedFiles = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            }

            if ((null == droppedFiles) || (!droppedFiles.Any()))
            {
                return;
            }

            await _mainWindowViewModel.AddImagesAsync(droppedFiles);
        }
    }
}
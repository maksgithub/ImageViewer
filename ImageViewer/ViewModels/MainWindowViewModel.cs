using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ImageViewer.Common;
using ImageViewer.Providers;

namespace ImageViewer.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private ImagesListViewModel _currentView;

        public MainWindowViewModel()
        {
            CurrentView = new ImagesListViewModel();
        }

        public ImagesListViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private ICommand _buttonCommand;
        public ICommand ButtonCommand =>
            _buttonCommand ?? (_buttonCommand = new RelayCommand(async o => await OnButtonCommand(o)));

        private async Task OnButtonCommand(object obj)
        {
            if (obj is string buttonName)
            {
                switch (buttonName)
                {
                    case "EscapeButton":
                        CurrentView.CloseImageDetails();
                        break;
                    case "UpButton":
                        await CurrentView.ShowNextImage();
                        break;
                    case "DownButton":
                        await CurrentView.ShowPreviousImage();
                        break;
                    case "BackButton":
                        CurrentView.ClearImages();
                        break;
                }
            }
        }

        public async Task AddImagesAsync(string[] imagesPaths)
        {
            await CurrentView.AddImagesAsync(imagesPaths);
        }
    }
}
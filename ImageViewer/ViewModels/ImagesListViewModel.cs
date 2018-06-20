using System.Threading.Tasks;
using System.Windows.Input;
using ImageViewer.Common;
using static ImageViewer.Common.ImageValidator;

namespace ImageViewer.ViewModels
{
    internal class ImagesListViewModel : BaseViewModel
    {
        ICommand _imageDoubleClickCommand;
        ICommand _nextImageCommand;
        ICommand _previousImageCommand;
        ICommand _blureImageCommand;

        ImageDetailsViewModel _currentImage;

        public ObservableLinkedList<ImageDetailsViewModel> Images { get; } =
            new ObservableLinkedList<ImageDetailsViewModel>();

        public ImageDetailsViewModel ImageDetailsView
        {
            get => _currentImage;
            set
            {
                if (!Equals(_currentImage, value))
                {
                    _currentImage = value;
                    OnPropertyChanged();
                }
                OnPropertyChanged(nameof(ImageDetailsVisible));
            }
        }

        public bool ImageDetailsVisible => ImageDetailsView != null;

        public ICommand ImageDoubleClickCommand => _imageDoubleClickCommand ??
                                                   (_imageDoubleClickCommand =
                                                       new RelayCommand(async o => await OnImageDoubleClickCommand(o)));

        public ICommand NextImageCommand => _nextImageCommand ??
                                            (_nextImageCommand =
                                                new RelayCommand(async o => await ShowNextImage()));


        public ICommand PreviousImageCommand => _previousImageCommand ??
                                                (_previousImageCommand =
                                                    new RelayCommand(async o => await ShowPreviousImage()));

        public ICommand BlureImageCommand => _blureImageCommand ??
                                             (_blureImageCommand =
                                                 new RelayCommand(OnBlureImageCommand));


        private async Task OnImageDoubleClickCommand(object param)
        {
            if (param is ImageDetailsViewModel imageDetailsViewModel)
            {
                ImageDetailsView = imageDetailsViewModel;
                await imageDetailsViewModel.Initialize();
            }
        }

        private void OnBlureImageCommand(object o)
        {
            ImageDetailsView.IsBlurred = !ImageDetailsView.IsBlurred;
        }

        public void CloseImageDetails()
        {
            ImageDetailsView?.Close();
            ImageDetailsView = null;
        }

        public async Task ShowPreviousImage()
        {
            if (Images.Count > 1 && ImageDetailsView != null)
            {
                ImageDetailsView?.Close();
                var current = Images.Find(_currentImage);
                ImageDetailsView = current.Previous != null ? current.Previous.Value : Images.Last.Value;
                await ImageDetailsView.Initialize();
            }
        }

        public async Task ShowNextImage()
        {
            if (Images.Count > 1 && ImageDetailsView != null)
            {
                ImageDetailsView?.Close();
                var current = Images.Find(_currentImage);
                ImageDetailsView = current.Next != null ? current.Next.Value : Images.First.Value;
                await ImageDetailsView.Initialize();
            }
        }

        public Task AddImagesAsync(string[] imagesPaths)
        {
            return Task.Run(() =>
            {
                foreach (var imagesPath in imagesPaths)
                {
                    if (IsImage(imagesPath))
                    {
                        BeginInvoke(() => Images.AddLast(new ImageDetailsViewModel(imagesPath)));
                    }
                }
            });
        }

        public void ClearImages()
        {
            Images.Clear();
        }
    }
}
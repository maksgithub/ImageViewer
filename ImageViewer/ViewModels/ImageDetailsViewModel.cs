using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ImageViewer.Common;
using static ImageViewer.Providers.ImagesProvider;

namespace ImageViewer.ViewModels
{
    class ImageDetailsViewModel : BaseViewModel
    {
        private readonly string _path;
        private bool _isBlurred;
        private BitmapImage _imageSource;
        private CancellationTokenSource _cts;
        private bool _isInitializing;

        public ImageDetailsViewModel(string path)
        {
            _path = path;
            Thumbnail = CreateThumbnail(path);
        }

        public BitmapSource Thumbnail { get; }

        public BitmapImage ImageSource
        {
            get => _imageSource;
            private set
            {
                if (!Equals(_imageSource, value))
                {
                    _imageSource = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsInitializing
        {
            get => _isInitializing;
            private set
            {
                if (_isInitializing != value)
                {
                    _isInitializing = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task Initialize()
        {
            _cts = new CancellationTokenSource();
            IsInitializing = true;
            try
            {
                ImageSource = await CreateBitmapImageAsync(_cts.Token);
            }
            catch (TaskCanceledException)
            {
                ImageSource = null;
            }
            IsInitializing = false;
        }


        public bool IsBlurred
        {
            get => _isBlurred;
            set
            {
                _isBlurred = value;
                OnPropertyChanged();
            }
        }

        public void Close()
        {
            IsInitializing = false;
            ImageSource = null;
            _cts?.Cancel();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public Task<BitmapImage> CreateBitmapImageAsync(CancellationToken ctsToken)
        {
            return Task.Run(() => CreateBitmapImage(_path), ctsToken);
        }
    }
}
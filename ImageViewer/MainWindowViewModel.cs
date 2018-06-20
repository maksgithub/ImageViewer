using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageViewer.Common;
using ImageViewer.ViewModels;

namespace ImageViewer
{
    class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _currentView;

        public MainWindowViewModel()
        {
            CurrentView = new ImagesListViewModel();
        }

        public BaseViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
    }
}
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace ImageViewer.Common
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IDispatcher
    {
        static readonly Dispatcher Dispatcher = Application.Current.Dispatcher;

        void IDispatcher.Invoke(Action method, DispatcherPriority priority)
        {
            Dispatcher.Invoke(method, priority);
        }

        T IDispatcher.Invoke<T>(Func<T> method, DispatcherPriority priority)
        {
            return Dispatcher.Invoke(method, priority);
        }

        void IDispatcher.BeginInvoke(Action method, DispatcherPriority priority)
        {
            Dispatcher.BeginInvoke(method, priority);
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public static void BeginInvoke(Action method, DispatcherPriority priority = DispatcherPriority.Background)
        {
            Dispatcher.BeginInvoke(method, priority);
        }

        public static T Invoke<T>(Func<T> method, DispatcherPriority priority = DispatcherPriority.Background)
        {
            return Dispatcher.Invoke(method, priority);
        }

        public static void Invoke(Action method, DispatcherPriority priority = DispatcherPriority.Background)
        {
            Dispatcher.Invoke(method, priority);
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            VerifyPropertyName(propertyName);
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> getPropertyExpression)
        {
            if (getPropertyExpression?.Body is MemberExpression memberExpression)
            {
                var propertyName = memberExpression.Member.Name;
                OnPropertyChanged(propertyName);
            }
        }

        void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null &&
                !propertyName.Equals("Item[]", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid property name: " + propertyName);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ImageViewer.Common
{
    public interface IDispatcher
    {
        void Invoke(Action method, DispatcherPriority priority = DispatcherPriority.Background);
        T Invoke<T>(Func<T> method, DispatcherPriority priority = DispatcherPriority.Background);
        void BeginInvoke(Action method, DispatcherPriority priority = DispatcherPriority.Background);
    }
}

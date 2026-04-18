using AdcratRoot.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AdcratRoot.WPF
{
    public class ArdcratRootWindow
    {
        private DispatcherTimer updater;

        /// <summary>
        /// Функция обновления
        /// </summary>
        public Action Update;
        public Action OnLoaded;
        public Action OnActivated;
        public Action OnDeactivated;
        public Action OnStateChanged;
        public Action OnLocationChanged;
        public Action OnSizeChanged;
        public Action OnGotFocus;
        public Action OnLostFocus;

        public ArdcratRootWindow(ArdcratRoot ardcratRoot) 
        {
            updater = new DispatcherTimer();
            updater.Interval = TimeSpan.FromMilliseconds(2000);
            updater.Tick += FuncUpdate;
            updater.Start();
        }
        ~ArdcratRootWindow() { 
            updater.Stop();
            updater.Tick -= FuncUpdate;
            GC.SuppressFinalize(this);
        }
        
        void FuncUpdate(object sender, EventArgs e) { if (Update != null) Task.Run(() => { Update(); }); }
        void FuncLoaded(object sender, EventArgs e) { if (OnLoaded != null) Task.Run(() => OnLoaded()); }
        void FuncActivated(object sender, EventArgs e) { if (OnActivated != null) Task.Run(() => OnActivated()); }
        void FuncDeactivated(object sender, EventArgs e) { if (OnDeactivated != null) Task.Run(() => OnDeactivated()); }
        void FuncStateChanged(object sender, EventArgs e) { if (OnStateChanged != null) Task.Run(() => OnStateChanged()); }
        void FuncLocationChanged(object sender, EventArgs e) { if (OnLocationChanged != null) Task.Run(() => OnLocationChanged()); }
        void FuncSizeChanged(object sender, SizeChangedEventArgs e) { if (OnSizeChanged != null) Task.Run(() => OnSizeChanged()); }
        void FuncGotFocus(object sender, EventArgs e) { if (OnGotFocus != null) Task.Run(() => OnGotFocus()); }
        void FuncLostFocus(object sender, EventArgs e) { if (OnLostFocus != null) Task.Run(() => OnLostFocus()); }
    }
}

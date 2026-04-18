using AdcratRoot.System.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WpfArbotRoot;
using WpfArbotRoot.AdcratRoot.System;

namespace AdcratRoot.System
{
    public class ArdcratRoot
    {
        private DispatcherTimer _errorCheckTimer;
        public Configurashion configurashion;
        private Window _targetWindow; 
        public ArdcratRoot(string FileConfig,Window window)
        {
            if (!File.Exists(FileConfig)) {
                MessageBox.Show("ArdcratRoot.config -> не найден", "Файл конфигурации, не был обноружен!",MessageBoxButton.OKCancel,MessageBoxImage.Error);
            }
            else {
                configurashion = new Configurashion(FileConfig);
                configurashion.Show();
            }
        }

        public ArdcratRoot(string FileConfig, MainWindow window)
        {
            if (!File.Exists(FileConfig))
            {
                MessageBox.Show("ArdcratRoot.config -> не найден", "Файл конфигурации, не был обноружен!", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
            else
            {
                configurashion = new Configurashion(FileConfig);
                configurashion.Show();
            }
        }
        ~ArdcratRoot()
        {
            
        }
        /// <summary>
        /// Подстройка под конфигуративный файл AdcratRoo.conf
        /// </summary>
        /// <returns>MainWindow</returns>
        /// 
        public Window AdcratRootWinMain()
        {
            if (_targetWindow != null)
            {
                _targetWindow.Height = configurashion.height;
                _targetWindow.Width = configurashion.width;
                _targetWindow.Title = configurashion.title;
                return _targetWindow;
            }
            else
            {
                return new MainWindow
                {
                    Height = configurashion.height,
                    Width = configurashion.width,
                    Title = configurashion.title
                };
            }
        }

        /// <summary>
        /// Подстройка под конфигуративный файл AdcratRoo.conf
        /// </summary>
        /// <returns>Window</returns>
        public Window AdcratRootWin()
        {
            return new Window
            {
                Height = configurashion.height,
                Width = configurashion.width,
                Title = configurashion.title,
            };
        }


        public void Show()
        {

        }
    }


    public class ArdcratRootException : Exception
    {
        public string Mesage {get; private set;}
        public ArdcratRootException(string mesage)
        {
            throw new Exception(mesage);
        }
    }
}

namespace AdcratRoot.System.Element
{
    public enum AdcratRootObject
    {
       Null, NaN, Zero, Key, 
       Namber, 
       File, Window, Page
    }
}

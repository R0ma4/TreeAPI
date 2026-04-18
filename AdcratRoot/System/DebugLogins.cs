using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AdcratRoot.System
{
    public class DebugLogins
    {
        class DLStory
        {
            public Level Level;
            public string mesage, descript;
            string Data = DateTime.Now.ToString();

            public DLStory(Level level, string now, string info, string Description) { 
                Level = level;
                Data = now;
                mesage = info;
                descript = Description;
            }

        }
        
        List<DLStory> dLStories = new List<DLStory>();
        public enum Level
        {
            INFO, WAR, ERROR, FATAL
        }

        int id = 0;
        public string mesage,descript,Path = string.Empty;
        public DebugLogins(string path) 
        {
            try
            {
                Path = path.Trim();
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
            }
            catch (ArdcratRootException ex)
            {
                MessageBox.Show($"{ex.Message}\n", "Не вышло обработать событие",MessageBoxButton.OKCancel,MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n", "Не вышло обработать событие",MessageBoxButton.OKCancel,MessageBoxImage.Error);
            }
        }
        public string bo = "false";
        ~DebugLogins()
        {
            Drop();
        }
        public void Drop()
        {
            dLStories.Clear();
            if (bo == "true") { File.Delete(Path); }
        }

        public void Add(string info, string Description, Level level)
        {
            try
            {
                mesage = info;
                descript = Description;
                var now = DateTime.Now.ToString();
                string msg = $"[{id}][{now}][{level}][{info}][{Description}]";
                msg = msg + '\n' + File.ReadAllText(Path);
                File.WriteAllText(Path, msg);
                dLStories.Add(new DLStory(level, now.ToString(), info, Description));
                id++;
            }
            catch (IOException ex)
            {
                MessageBox.Show($"{ex.Message}\n", "Не вышло обработать событие", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n", "Не вышло обработать событие", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }

        public void Clear()
        {
            dLStories.Clear();
            if(bo == "true") File.WriteAllText(Path, null);
        }
    }
}

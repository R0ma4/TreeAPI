using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfArbotRoot.AdcratRoot.System
{
    public class Configurashion
    {
        string config;

        #region стандартные значения кофиграции 


        protected bool ignore = false;
        public string BlockName = string.Empty;
        public string BlockValueName = string.Empty;
        public string KeyName = string.Empty;
        public string ValuekName = string.Empty;
        public Window Win;

        public string path_log_testerong, min_level_log, max_level_log, levrl_log, clousedrop_log;

        // Блок program - как насройки внешнего вида программы
        public double height = 450, minheight = 100, maxheight = SystemParameters.PrimaryScreenHeight;
        public double width = 900, minwidth = 100, maxwidth = SystemParameters.PrimaryScreenWidth;
        public string title = "${Programm:Name} - ${Programm:Project}";
        public string size = "normal";
        // Кеш, Хронение, Пмять
        public double Memory;
        #endregion

        public Configurashion(string path_file_config) 
        {
            config = File.ReadAllText(path_file_config);
            Show();
        }

        private (string key, string value) ParseNameValue(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return (null, null);

            // Формат: [BlockName(ValueName)]
            var match1 = Regex.Match(input, @"^\[\s*([^\(\s]+)\s*\(\s*([^\)\s]+)\s*\)\s*\]$");
            if (match1.Success)
            {
                BlockName = match1.Groups[1].Value;
                BlockValueName = match1.Groups[2].Value;
                return (match1.Groups[1].Value, match1.Groups[2].Value);

            }

            // Формат: Key = Value
            var match2 = Regex.Match(input, @"^\s*([^=\s]+)\s*=\s*(.+)\s*$");
            if (match2.Success)
            {
                return (match2.Groups[1].Value, match2.Groups[2].Value);
            }

            return (null, null);
        }


        public void Show() 
        {
            try {
                var lines = config.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    var (key, value) = ParseNameValue(line.Trim());
                    if(key == "$ELEMENTIGNORE")
                    {
                        switch (value)
                        {
                            case "true": ignore = true; break;
                            case "false": ignore = false; break;
                        }
                    }
                    if (!ignore)
                    {
                        if (BlockName == "ArborRoot") { }
                        else if (BlockName == "program") { BlockProgramm(key, value); }
                        else if (BlockName == "Testing" && BlockValueName == "Log") { BlockTestingLog(key, value); }
                        else if (BlockName == "page") { }
                        else if (BlockName == "NetWork") { }
                        else if (BlockName == "Temp") { }
                        else { MessageBox.Show($"{key} из {BlockName} не обробатываеммое значение, в данной версии", "Ошибка в конфигурации", MessageBoxButton.OKCancel, MessageBoxImage.Error); }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"При чтени .conf файла, произашла ошибка\n{ex}","Ошибка при чтении .conf");
            }
        }



        private string Titel(string text)
        {
            var replacements = new Dictionary<string, string>
            {
                ["Programm"] = "Programm - как значение имеет множесво типов: { Name, Data, Memory, Type, Dir}",
                ["Window"] = "Window - как значение имеет множесво типов: { height (min, max), width (min, max)}",
                ["Programm:Name"] = "Имя программы",
                ["Window:height"] = height.ToString(),
                ["Window:maxheight"] = maxheight.ToString(),
                ["Window:minheight"] = minheight.ToString(),
                ["Window:width"] = width.ToString(),
                ["Window:maxwidth"] = maxwidth.ToString(),
                ["Window:minwidth"] = minwidth.ToString()
            };
            string new_titlle = text;
            foreach (var kvp in replacements) { new_titlle = new_titlle.Replace($"${{{kvp.Key}}}", kvp.Value); }
            return new_titlle.Replace("'",null);
        }

        public string Block(string key) { return null; }
        public string Key(string key) { return null; }

        public void Add(string Block, string Key, object value)
        {

        }

        public void Remove(string Block, string Key) { }

        // Обработка блоков
        void BlockProgramm(string key, string value)
        {
           
            switch (key)
            {
                case "height" when double.TryParse(value, out double heightValue): height = heightValue; break;
                case "minheight" when double.TryParse(value, out double heightValue): minheight = heightValue; break;
                case "maxheight" when double.TryParse(value, out double heightValue): maxheight = heightValue; break;
                case "width" when double.TryParse(value, out double widthValue): width = widthValue; break;
                case "minwidth" when double.TryParse(value, out double widthValue): minwidth = widthValue; break;
                case "maxwidth" when double.TryParse(value, out double widthValue): maxwidth = widthValue; break;
                case "title": title = Titel(value); break;
                case "size": size = value.Replace("\'", null); break;
            }
        }
        // Обработка блоков
        void BlockTestingLog(string key, string value)
        {
            // path_log_testerong, min_level_log, max_level_log, levrl_log, clousedrop_log
            switch (key)
            {
                case "path": path_log_testerong = value; break;
                case "minlevel":  min_level_log = value; break;
                case "maxlevel": max_level_log = value; break;
                case "level": levrl_log = value; break;
                case "clousedrop": clousedrop_log = value; break;
            }
        }

    }
}

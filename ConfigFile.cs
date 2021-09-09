using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEXamlTool
{
    internal class ConfigFile
    {
        static internal string XamlFilePath
        {
            get;
            set;
        }

        static internal string AssemblyPath
        {
            get;
            set;
        }

        static internal void Load()
        {
            string currentExePath = Process.GetCurrentProcess().MainModule.FileName;
            int lastIndex = currentExePath.LastIndexOf('\\');
            currentExePath = currentExePath.Substring(0, lastIndex);

            if (!File.Exists(currentExePath + @"\config.ini"))
            {
                var stream = File.Create(currentExePath + @"\config.ini");
                stream.Close();
            }

            var contents = File.ReadAllText(currentExePath + @"\config.ini").Split('\n');

            if (2 <= contents.Length)
            {
                AssemblyPath = contents[0];
                XamlFilePath = contents[1];
            }
        }

        static internal void Save()
        {
            string currentExePath = Process.GetCurrentProcess().MainModule.FileName;
            int lastIndex = currentExePath.LastIndexOf('\\');
            currentExePath = currentExePath.Substring(0, lastIndex);

            string content = String.Format("{0}\n{1}", AssemblyPath, XamlFilePath);
            File.WriteAllText(currentExePath + @"\config.ini", content);
        }
    }
}

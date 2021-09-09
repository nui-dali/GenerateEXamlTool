using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEXamlTool
{
    public class HitPackage
    {
        public void AddDllPath(string version, string path)
        {
            List<string> dllPaths = null;

            if (Versions.ContainsKey(version))
            {
                dllPaths = Versions[version];
            }
            else
            {
                dllPaths = new List<string>();
                Versions.Add(version, dllPaths);
            }

            dllPaths.Add(path);
        }

        public Dictionary<string, List<string>> Versions
        {
            get;
        } = new Dictionary<string, List<string>>();
    }
}

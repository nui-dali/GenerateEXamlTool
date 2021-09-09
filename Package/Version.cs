using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEXamlTool
{
    public class Version
    {
        public Version(string path)
        {
            VersionName = path.Substring(path.LastIndexOf('\\') + 1);
            var dllFiles = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

            foreach (var file in dllFiles)
            {
                dlls.Add(new Dll(file));
            }
        }

        public string FindAssemblyPath(AssemblyNameReference assemblyName)
        {
            foreach (var dll in dlls)
            {
                if (dll.IsMatchAssembly(assemblyName))
                {
                    return dll.Path;
                }
            }

            return null;
        }

        public string VersionName
        {
            get;
        }

        private List<Dll> dlls = new List<Dll>();
    }
}

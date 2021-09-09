using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEXamlTool
{
    public class Package
    {
        public Package(string path)
        {
            PackageName = path.Substring(path.LastIndexOf('\\') + 1);
            var subFolders = Directory.GetDirectories(path);

            foreach (var folder in subFolders)
            {
                versions.Add(new Version(folder));
            }
        }

        public List<(string, string)> FindAssemblyPath(AssemblyNameReference assemblyName)
        {
            List<(string, string)> ret = null;

            foreach (var version in versions)
            {
                var dllPath = version.FindAssemblyPath(assemblyName);

                if (null != dllPath)
                {
                    if (null == ret)
                    {
                        ret = new List<(string, string)>();
                    }

                    ret.Add((version.VersionName, dllPath));
                }
            }

            return ret;
        }

        public string PackageName
        {
            get;
        }

        private List<Version> versions = new List<Version>();
    }
}

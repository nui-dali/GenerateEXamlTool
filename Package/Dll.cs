using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEXamlTool
{
    public class Dll
    {
        private static int dllCount = 0;

        public Dll(string path)
        {
            dllCount++;
            try
            {
                Path = path;

                var dllName = path.Substring(path.LastIndexOf('\\') + 1);
                this.dllName = dllName.Substring(0, dllName.LastIndexOf('.'));
                //var assDef = AssemblyDefinition.ReadAssembly(path);
                //assemblyName = assDef.FullName;
            }
            catch
            {

            }
        }

        public bool IsMatchAssembly(AssemblyNameReference assemblyName)
        {
            if (assemblyName.Name == dllName)
            {
                if (null == assDef)
                {
                    assDef = AssemblyDefinition.ReadAssembly(Path);
                }

                if (assDef.FullName == assemblyName.FullName)
                {
                    assDef.Dispose();
                    assDef = null;
                    return true;
                }
                else
                {
                    assDef.Dispose();
                    assDef = null;
                }
            }

            return false;
        }

        public string Path
        {
            get;
        }

        private AssemblyDefinition assDef;
        private string dllName;
    }
}

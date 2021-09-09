using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEXamlTool
{
    public class AssembliesInParallelFolder
    {
        public AssembliesInParallelFolder(string path)
        {
            int index = path.LastIndexOf('\\');
            this.path = path.Substring(0, index);
            var mainAssemlbyDef = AssemblyDefinition.ReadAssembly(path);

            assemblyDefinitions = new List<AssemblyDefinition>();

            var dllFiles = Directory.GetFiles(this.path, "*.dll", SearchOption.AllDirectories);

            foreach (var file in dllFiles)
            {
                try
                {
                    var assDef = AssemblyDefinition.ReadAssembly(file);
                    if (assDef.FullName != mainAssemlbyDef.FullName)
                    {
                        assemblyDefinitions.Add(assDef);
                    }
                    else
                    {
                        assDef.Dispose();
                    }
                }
                catch
                {

                }
            }

            mainAssemlbyDef.Dispose();
        }

        public string GetAssembly(AssemblyNameReference assemblyNameReference)
        {
            foreach (var ass in assemblyDefinitions)
            {
                if (ass.FullName == assemblyNameReference.FullName)
                {
                    return ass.MainModule.FileName;
                }
            }

            return null;
        }

        private string path;

        public List<AssemblyDefinition> assemblyDefinitions;
    }
}

using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEXamlTool
{
    public class GatherMainAssemblyInfo
    {
        public GatherMainAssemblyInfo(string assPath)
        {
            var assDef = AssemblyDefinition.ReadAssembly(assPath);
        }
    }
}

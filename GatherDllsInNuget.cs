using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenerateEXamlTool
{
    public class NugetPackages
    {
        public NugetPackages(string rootPath = null)
        {
            if (null == rootPath)
            {
                string curUser = GetCurrentUser();
                this.rootPath = $@"C:\Users\{curUser}\.nuget\packages";
            }
            else
            {
                this.rootPath = rootPath;
            }

            var subFolders = Directory.GetDirectories(this.rootPath);

            foreach (var folder in subFolders)
            {
                if (folder.EndsWith("tizen.net")
                    ||
                    folder.EndsWith("tizen.net.api6")
                    ||
                    folder.EndsWith("tizen.net.api7")
                    ||
                    folder.EndsWith("tizen.net.api8")
                    ||
                    folder.EndsWith("tizen.nui.xamlbuild"))
                {
                    continue;
                }

                packages.Add(new Package(folder));
            }
        }

        public bool GatherAssemblyPath(AssemblyNameReference assemblyName)
        {
            bool ret = false;

            foreach (var package in packages)
            {
                var hitPackages = package.FindAssemblyPath(assemblyName);

                if (null != hitPackages)
                {
                    HitPackage hitPackage = null;

                    if (this.hitPackages.ContainsKey(package.PackageName))
                    {
                        hitPackage = this.hitPackages[package.PackageName];
                    }
                    else
                    {
                        hitPackage = new HitPackage();
                        this.hitPackages.Add(package.PackageName, hitPackage);
                    }

                    foreach (var value in hitPackages)
                    {
                        hitPackage.AddDllPath(value.Item1, value.Item2);
                    }

                    ret = true;

                    break;
                }
            }

            return ret;
        }

        public Dictionary<string, HitPackage> hitPackages
        {
            get;
        } = new Dictionary<string, HitPackage>();

        private string rootPath;

        private List<Package> packages = new List<Package>();

        private enum WTSInfoClass
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingBytes,
            WTSOutgoingBytes,
            WTSIncomingFrames,
            WTSOutgoingFrames,
            WTSClientInfo,
            WTSSessionInfo
        }

        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WTSInfoClass wtsInfoClass, out IntPtr ppBuffer, out uint pBytesReturned);

        [DllImport("Wtsapi32.dll")]
        private static extern void WTSFreeMemory(IntPtr pointer);

        private static string GetCurrentUser()
        {
            IntPtr buffer;
            uint strLen;
            int cur_session = -1;
            var username = "SYSTEM"; // assume SYSTEM as this will return "\0" below
            if (WTSQuerySessionInformation(IntPtr.Zero, cur_session, WTSInfoClass.WTSUserName, out buffer, out strLen) && strLen > 1)
            {
                username = Marshal.PtrToStringAnsi(buffer); // don't need length as these are null terminated strings
                WTSFreeMemory(buffer);
                //if (WTSQuerySessionInformation(IntPtr.Zero, cur_session, WTSInfoClass.WTSDomainName, out buffer, out strLen) && strLen > 1)
                //{
                //    username = Marshal.PtrToStringAnsi(buffer) + "\\" + username; // prepend domain name
                //    WTSFreeMemory(buffer);
                //}
            }
            return username;
        }
    }
}

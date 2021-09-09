using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tizen.NUI.EXaml;
using Tizen.NUI.Xaml.Build.Tasks;

namespace GenerateEXamlTool
{
    public partial class GenerateEXamlTool : Form
    {
        public GenerateEXamlTool()
        {
            InitializeComponent();

            listViewCombox = new ListViewCombox(listViewInfoOfPackages, comboBox1, 1);
            listViewInfoOfPackages.MouseUp += listViewInfoOfPackages_MouseUp;

            nugetPackages = new NugetPackages();

            ConfigFile.Load();

            textBoxAssemblyPath.Text = ConfigFile.AssemblyPath;
            textBoxXamlFilePath.Text = ConfigFile.XamlFilePath;

            if (0 < textBoxAssemblyPath.Text.Length)
            {
                assembliesInParallelFolder = new AssembliesInParallelFolder(textBoxAssemblyPath.Text);
                GatherReferAssemblies(textBoxAssemblyPath.Text);
            }
        }

        private void listViewInfoOfPackages_MouseUp(object sender, MouseEventArgs e)
        {
            var selectPackageName = listViewCombox.Location(e.X, e.Y);

            if (null != selectPackageName)
            {
                comboBox1.Items.Clear();

                var selectPackage = nugetPackages.hitPackages[selectPackageName];

                foreach (var versionInfo in selectPackage.Versions)
                {
                    comboBox1.Items.Add(versionInfo.Key);
                }
            }
        }

        private List<string> refAssInParallelFolder = new List<string>();

        private AssembliesInParallelFolder assembliesInParallelFolder;
        private NugetPackages nugetPackages;
        private ListViewCombox listViewCombox;

        private void GatherReferAssemblies(string path)
        {
            nugetPackages.hitPackages.Clear();

            var assDef = AssemblyDefinition.ReadAssembly(path);

            foreach (var referAss in assDef.MainModule.AssemblyReferences)
            {
                if (null == assembliesInParallelFolder.GetAssembly(referAss))
                {
                    if (false == nugetPackages.GatherAssemblyPath(referAss))
                    {
                        if ("Tizen.NUI.XamlBuild" != referAss.Name)
                        {
                            MessageBox.Show($"Can't find assembly {referAss.Name} in nuget packages.");
                        }
                    }
                }
            }

            listViewInfoOfPackages.Items.Clear();
            listViewInfoOfPackages.Columns.Add("Package", 350, HorizontalAlignment.Left);
            listViewInfoOfPackages.Columns.Add("Version", 200, HorizontalAlignment.Left);

            foreach (var hitPackage in nugetPackages.hitPackages)
            {
                ListViewItem lvi = new ListViewItem(hitPackage.Key);
                lvi.SubItems.Add(hitPackage.Value.Versions.Last().Key);
                listViewInfoOfPackages.Items.Add(lvi);
            }

            assDef.Dispose();
        }

        private void buttonChooseAssembly_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (null != ConfigFile.AssemblyPath)
            {
                int lastIndex = ConfigFile.AssemblyPath.LastIndexOf('\\');

                if (0 < lastIndex)
                {
                    openFileDialog.InitialDirectory = ConfigFile.AssemblyPath.Substring(0, lastIndex);
                }

                if (ConfigFile.AssemblyPath.EndsWith(".exe"))
                {
                    openFileDialog.Filter = "exe|*.exe|dll|*.dll";
                }
                else
                {
                    openFileDialog.Filter = "dll|*.dll|exe|*.exe";
                }
            }
            else
            {
                openFileDialog.Filter = "dll|*.dll|exe|*.exe";
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxAssemblyPath.Text = openFileDialog.FileName;
                assembliesInParallelFolder = new AssembliesInParallelFolder(textBoxAssemblyPath.Text);
                GatherReferAssemblies(textBoxAssemblyPath.Text);

                ConfigFile.AssemblyPath = openFileDialog.FileName;
                ConfigFile.Save();
            }
        }

        private void buttonGenerateEXaml_Click(object sender, EventArgs e)
        {
            if (0 == textBoxAssemblyPath.Text.Length)
            {
                return;
            }

            string referAssemblies = "";

            foreach (var referAss in assembliesInParallelFolder.assemblyDefinitions)
            {
                referAssemblies += referAss.MainModule.FileName + ";";
            }

            foreach (var item in listViewInfoOfPackages.Items)
            {
                if (item is ListViewItem listViewItem)
                {
                    var package = nugetPackages.hitPackages[listViewItem.Text];
                    var dllFiles = package.Versions[listViewItem.SubItems[1].Text];

                    foreach (var file in dllFiles)
                    {
                        referAssemblies += file + ";";
                    }
                }
            }

            if (referAssemblies.EndsWith(";"))
            {
                referAssemblies = referAssemblies.Substring(0, referAssemblies.Length - 1);
            }

            var assemblyPath = textBoxAssemblyPath.Text;
            var outputPath = assemblyPath.Substring(0, assemblyPath.LastIndexOf('\\') + 1);
            var xamlPath = textBoxXamlFilePath.Text;

            try
            {
                var examlFileDir = GenerateEXaml(assemblyPath, xamlPath, referAssemblies, outputPath);
                if (null != examlFileDir)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.InitialDirectory = outputPath + @"res\examl";
                    openFileDialog.Filter = "EXaml|*.examl";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GenerateEXaml(string assemblyPath, string xamlFilePath, string ReferencePath, string outputRootPath)
        {
            var examl = new XamlCTask
            {
                Assembly = assemblyPath,
                ReferencePath = ReferencePath,
                DebugSymbols = false,
                DebugType = "portable",
                XamlFilePath = xamlFilePath,
                outputRootPath = outputRootPath,
                UseInjection = false,
                BuildEngine = new DummyBuildEngine(),
                KeepXamlResources = true
            };

            IList<Exception> exceptions;

            if (examl.Execute(out exceptions) || exceptions == null || !exceptions.Any())
            {
                return outputRootPath + @"/res/examl/";
            }

            if (exceptions.Count > 0)
            {
                MessageBox.Show(exceptions[0].Message);
            }

            return null;
        }

        private void buttonChooseXamlFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Xaml File|*.xaml";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (null != ConfigFile.XamlFilePath)
            {
                int lastIndex = ConfigFile.XamlFilePath.LastIndexOf('\\');

                if (0 < lastIndex)
                {
                    openFileDialog.InitialDirectory = ConfigFile.XamlFilePath.Substring(0, lastIndex);
                }
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxXamlFilePath.Text = openFileDialog.FileName;
                ConfigFile.XamlFilePath = openFileDialog.FileName;
                ConfigFile.Save();
            }
        }
    }

    public class ListViewCombox
    {
        ListView _listView;
        ComboBox _combox;
        int _showColumn = 0;
        ListViewItem.ListViewSubItem _selectedSubItem;

        /// <summary>
        /// 列表combox
        /// </summary>
        /// <param name="listView">listView控件</param>
        /// <param name="combox">要呈现的combox控件</param>
        /// <param name="showColumn">要在哪一列显示combox(从0开始)</param>
        public ListViewCombox(ListView listView, ComboBox combox, int showColumn)
        {
            _listView = listView;
            _combox = combox;
            _showColumn = showColumn;
            BindComboxEvent();
        }

        /// <summary>
        /// 定位combox
        /// </summary>
        /// <param name="x">点击的x坐标</param>
        /// <param name="y">点击的y坐标</param>
        public string Location(int x, int y)
        {
            string ret = null;
            ListViewItem item = _listView.GetItemAt(5, y);
            if (item != null)
            {
                _selectedSubItem = item.GetSubItemAt(x, y);
                if (_selectedSubItem != null)
                {
                    int clickColumn = item.SubItems.IndexOf(_selectedSubItem);
                    if (clickColumn == 0)
                    {
                        _combox.Visible = false;
                    }
                    else if (clickColumn == _showColumn)
                    {
                        int padding = 2;
                        Rectangle rect = _selectedSubItem.Bounds;
                        rect.X += _listView.Left + padding;
                        rect.Y += _listView.Top + padding;
                        rect.Width = _listView.Columns[clickColumn].Width + padding;
                        if (_combox != null)
                        {
                            _combox.Bounds = rect;
                            _combox.Text = _selectedSubItem.Text;
                            _combox.Visible = true;
                            _combox.BringToFront();
                            _combox.Focus();
                            ret = item.Text;
                        }
                    }
                }
            }

            return ret;
        }

        private void BindComboxEvent()
        {
            if (_combox != null)
            {
                _combox.SelectedIndexChanged += combox_SelectedIndexChanged;
                _combox.Leave += combox_Leave;
            }
        }

        private void combox_Leave(object sender, EventArgs e)
        {
            if (_selectedSubItem != null)
            {
                _selectedSubItem.Text = _combox.Text;
                _combox.Visible = false;
            }
        }

        private void combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedSubItem != null)
            {
                _selectedSubItem.Text = _combox.Text;
                _combox.Visible = false;
            }
        }
    }
}

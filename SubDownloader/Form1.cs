using CookComputing.XmlRpc;
using SubDownloader.OpenSubtitlesAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            try
            {
                var directoryNode = new TreeNode(directoryInfo.Name);
                foreach (var directory in directoryInfo.GetDirectories())
                    if (!directory.Name.StartsWith("$"))
                        directoryNode.Nodes.Add(CreateDirectoryNode(directory));
                foreach (var file in directoryInfo.GetFiles())
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
                return directoryNode;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
                throw;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = @"D:\MediaPlayer";
            
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = folderBrowserDialog1.SelectedPath;
                foreach (var item in VideoFiles.GetFiles(path))
                {
                    dataGridView1.Rows.Add(item.Name, item.Path, item.HasSubtitle, item.Hash);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IOpenSubtitles openSubs = XmlRpcProxyGen.Create<IOpenSubtitles>();
            openSubs.AttachLogger(new XmlRpcDebugLogger());
            var test = openSubs.ServerInfo();

        }
    }
}

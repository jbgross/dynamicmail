using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    /// <summary>
    /// Class to display and select folders to
    /// index or search from
    /// </summary>
    public partial class SelectFolders : Form
    {
        private List<Outlook.MAPIFolder> selectedFolders = new List<Outlook.MAPIFolder>();
        private Finishable finish;
        private Dictionary<string, Outlook.MAPIFolder> nameFolder;
        private TreeNode userInvoke = null;
        private FolderTree sourceTree;

        /// <summary>
        /// Get an array of the selected folders
        /// </summary>
        public Outlook.MAPIFolder[] SelectedFolders
        {
            get 
            {
                this.GetSelectedFolders();
                return (Outlook.MAPIFolder []) this.selectedFolders.ToArray(); 
            }
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="roots"></param>
        /// <param name="source"></param>
        public SelectFolders(FolderTree sourceTree, Finishable finish)
        {
            InitializeComponent();
            this.sourceTree = sourceTree;
            this.nameFolder = sourceTree.NameFolder;
            try
            {
                foreach (TreeNode parent in this.sourceTree.Nodes)
                {
                    this.folderTree.Nodes.Add(parent);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            this.folderTree.Refresh();
            this.folderTree.AfterCheck += new TreeViewEventHandler(treeView_AfterCheck);
            this.nameFolder = sourceTree.NameFolder;
            this.finish = finish;
            this.Show();
            this.Focus();
        }


        /// <summary>
        /// Get an array of all the folders selected
        /// </summary>
        /// <returns></returns>
        public void GetSelectedFolders()
        {
            TreeNodeCollection tops = this.folderTree.Nodes;
            foreach(TreeNode top in tops)
            {
                if (top.Checked && this.nameFolder.ContainsKey(top.Text))
                {
                    this.selectedFolders.Add((Outlook.MAPIFolder)this.nameFolder[top.Text]);
                }
                this.GetSelectedSubFolders(this.selectedFolders, top);
            }

        }

        /// <summary>
        /// Recurse down list adding select folders
        /// </summary>
        /// <param name="?"></param>
        /// <param name="parent"></param>
        private void GetSelectedSubFolders(List<Outlook.MAPIFolder> selectedFolders, TreeNode parent)
        {
            TreeNode child = parent.FirstNode;
            while(true) {
                if (child == null)
                    return;
                if (child.Checked && this.nameFolder.ContainsKey(child.Name)) 
                {
                    selectedFolders.Add((Outlook.MAPIFolder) this.nameFolder[child.Name]);
                    GetSelectedSubFolders(selectedFolders, child);
                }
                child = child.NextNode;
            } 
        }

        /// <summary>
        /// Code taken from http://www.dotnet247.com/247reference/msgs/26/133631.aspx
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
                userInvoke = e.Node;

            if (IsDescendent(userInvoke, e.Node))
            {
                foreach (TreeNode tn in e.Node.Nodes)
                    tn.Checked = e.Node.Checked;
            }
        }

        private bool IsDescendent(TreeNode parent, TreeNode desc)
        {
            return desc.FullPath.IndexOf(parent.FullPath) == 0;
        }

        private void Done_Click(object sender, EventArgs e)
        {
            this.finish.Finish();
            ReleaseTree(sender, e);
            this.Close();
        }

        private void ReleaseTree(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.folderTree.Nodes)
            {
                this.folderTree.Nodes.Remove(node);
            }
            this.sourceTree.ClearCheckBoxes();
        }

    }
}
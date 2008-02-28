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
        private Finishable source;
        private TreeNode tNode;
        private Hashtable nameFolder = new Hashtable();
        private TreeNode userInvoke = null;

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
        public SelectFolders(Outlook.Folders roots, Finishable source)
        {
            InitializeComponent();
            this.folderTree.AfterCheck += new TreeViewEventHandler(treeView_AfterCheck);
            this.source = source;
            foreach (Outlook.MAPIFolder folder in roots)
            {
                // Add parent node to treeView1 control
                tNode = this.folderTree.Nodes.Add(folder.FullFolderPath);
                this.nameFolder[folder.FullFolderPath] = folder;
                BuildTree(tNode, folder);
            }
            this.Visible = true;
        }

        /// <summary>
        /// Get an array of all the folders selected
        /// </summary>
        /// <returns></returns>
        public void GetSelectedFolders()
        {
            TreeNode top = this.folderTree.TopNode;
            while (true)
            {
                if (top == null)
                    return;
                if (top.Checked && this.nameFolder.ContainsKey(top.Text))
                {
                    this.selectedFolders.Add((Outlook.MAPIFolder)this.nameFolder[top.Text]);
                }
                this.GetSelectedSubFolders(this.selectedFolders, top);
                top = top.NextNode;
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
                if (child.Checked && this.nameFolder.ContainsKey(child.Text)) 
                {
                    selectedFolders.Add((Outlook.MAPIFolder) this.nameFolder[child.Text]);
                    GetSelectedSubFolders(selectedFolders, child);
                }
                child = child.NextNode;
            } 
        }

        private void BuildTree(TreeNode tree, Outlook.MAPIFolder parent)
        {
            Outlook.Folders children = parent.Folders;
            if (children.Count == 0)
            {
                return;
            }

            foreach (Outlook.MAPIFolder child in children)
            {
                TreeNode parentNode = tree.Nodes.Add(child.FullFolderPath);
                this.nameFolder[child.FullFolderPath] = child;
                BuildTree(parentNode, child);
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
            this.source.Finish();
        }

    }
}
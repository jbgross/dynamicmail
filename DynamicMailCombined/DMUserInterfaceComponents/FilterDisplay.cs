using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    public partial class FilterDisplay : Form, Finishable
    {
        private List<Outlook.MAPIFolder> selectedFolders = new List<Outlook.MAPIFolder>();
        private Finishable source;
        private TreeNode tNode;
        private List<TreeNode> roots = new List<TreeNode>();
        private Dictionary<String, Outlook.MAPIFolder> nameFolder = new Dictionary<String, Outlook.MAPIFolder>();
        private TreeNode userInvoke = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roots">the root folders</param>
        public FilterDisplay(Outlook.Folders roots, Outlook.MAPIFolder current)
        {
            InitializeComponent();
            this.BuildRoot(roots, this);
            this.SelectFolderAndDisplay(current);
            this.Show();
            this.Focus();
        }

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
        /// Build out folder hierarchy
        /// </summary>
        /// <param name="roots"></param>
        /// <param name="source"></param>
        public void BuildRoot(Outlook.Folders roots, Finishable source)
        {
            this.folderTree.AfterCheck += new TreeViewEventHandler(treeView_AfterCheck);
            this.source = source;
            foreach (Outlook.MAPIFolder folder in roots)
            {
                // Add parent node to folderTree control
                tNode = this.folderTree.Nodes.Add(folder.FullFolderPath);
                this.roots.Add(tNode);
                this.nameFolder[folder.FullFolderPath] = folder;
                BuildTree(tNode, folder);
            }
        }

        /// <summary>
        /// Add a folder to the Selected list and display that level
        /// </summary>
        public void SelectFolderAndDisplay(Outlook.MAPIFolder folder)
        {
            this.selectedFolders.Add(folder);
            foreach (TreeNode root in this.roots)
            {
                this.ExpandChecked(root);
            }
        }


        /// <summary>
        /// recurse through and expand checked nodes
        /// </summary>
        /// <param name="parent"></param>
        private void ExpandChecked(TreeNode parent)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                // first, get the folder out of the list
                Outlook.MAPIFolder childFolder = this.nameFolder[child.Name];
                if (this.selectedFolders.Contains(childFolder))
                {
                    child.Checked = true;
                }

                if (child.Checked)
                {
                    parent.Expand();
                }
                ExpandChecked(child);
            }
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

        public void Finish()
        {
            this.Refresh();
        }

        public void Cancel()
        {
            // do nothing
        }
    }
}
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
	public class FolderTree: TreeNode
	{
        private List<Outlook.MAPIFolder> selectedFolders = new List<Outlook.MAPIFolder>();
        private Dictionary<string, Outlook.MAPIFolder> nameFolder = new Dictionary<string, Outlook.MAPIFolder>();
        private TreeNode userInvoke = null;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="roots"></param>
        /// <param name="source"></param>
        private FolderTree(Outlook.Folders roots)
        {
            this.TreeView.CheckBoxes = true;
            foreach (Outlook.MAPIFolder folder in roots)
            {
                // Add parent node to treeView1 control
                TreeNode root = new TreeNode(folder.FullFolderPath);
                this.Nodes.Add(root);
                this.nameFolder[folder.FullFolderPath] = folder;
                BuildTree(root, folder);
            }
        }

        /// <summary>
        /// Get an array of all the folders selected
        /// </summary>
        /// <returns></returns>
        public void GetSelectedFolders()
        {
            TreeNode top = this.TreeView.TopNode;
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

        private void BuildTree(TreeNode parentNode, Outlook.MAPIFolder parentFolder)
        {
            Outlook.Folders children = parentFolder.Folders;
            if (children.Count == 0)
            {
                return;
            }

            foreach (Outlook.MAPIFolder child in children)
            {
                TreeNode childNode = new TreeNode(child.FullFolderPath);
                parentNode.Nodes.Add(childNode);
                this.nameFolder[child.FullFolderPath] = child;
                BuildTree(parentNode, child);
            }
        }

        private bool IsDescendent(TreeNode parent, TreeNode desc)
        {
            return desc.FullPath.IndexOf(parent.FullPath) == 0;
        }


	}
}

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
    public partial class FilterDisplay : Form
    {
        private String initialSelectedFolder;
        private Finishable source;
        private static TreeNode tNode;
        private Dictionary<String, Outlook.MAPIFolder> nameFolder;
        private TreeNode userInvoke = null;
        private FilterMail filter;
        private FolderTree sourceTree;

        // this works much better than a list, trust me - JBG
        private Dictionary<int, Outlook.MailItem> messageList = new Dictionary<int, Outlook.MailItem>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sourceTree">The FolderTree object, which will be released later</param>
        /// <param name="currentFolderName">The name of the current folder selected</param>
        /// <param name="filter">The actual filter</param>
        public FilterDisplay(FolderTree sourceTree, String currentFolderName, FilterMail filter)
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

            this.filter = filter;
            this.SelectFolderAndDisplay(currentFolderName);
            this.ShowMessages();
            this.Show();
            this.Focus();
        }

        /// <summary>
        /// Get the messages out of the filter, and create
        /// a list element for each
        /// </summary>
        private void ShowMessages()
        {
            this.mailGrid.Rows.Clear();
            this.Refresh();
            this.messageList = new Dictionary<int, Outlook.MailItem>();
            foreach (Outlook.MailItem msg in this.filter.Messages)
            {
                String sender = msg.SenderName != null ? msg.SenderName : msg.SenderEmailAddress;
                String recip = msg.To != null ? msg.To : "no recipient";
                String subject = msg.Subject != null ? msg.Subject : "<no subject>";
                String date = msg.SentOn.ToString();
                String [] data = { sender, recip, subject, date };
                int pos = this.mailGrid.Rows.Add(data);
                this.messageList[pos] = msg;
            }
        }

 
        /// <summary>
        /// Add a folder to the Selected list and display that level
        /// </summary>
        public void SelectFolderAndDisplay(String folderName)
        {
            this.initialSelectedFolder = folderName;
            foreach (TreeNode root in this.folderTree.Nodes)
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
                String path = child.Name;
                // check the folder
                if (this.initialSelectedFolder.Equals(path))
                {
                    child.Checked = true;
                }

                // expand the folder and its parent
                if (child.Checked)
                {
                    parent.Expand();
                    child.Expand();
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
            foreach (TreeNode root in this.folderTree.Nodes)
            {
                this.GetSelectedSubFolders(root);
            }

        }

        /// <summary>
        /// Recurse down list adding select folders
        /// </summary>
        /// <param name="?"></param>
        /// <param name="parent"></param>
        private void GetSelectedSubFolders(TreeNode parent)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                if(this.nameFolder.ContainsKey(child.Name)) 
                {
                    Outlook.MAPIFolder folder = this.nameFolder[child.Name];
                    if (child.Checked)
                    {
                        this.filter.AddFolder(folder);
                    }
                    else
                    {
                        this.filter.RemoveFolder(folder);
                    }
                    // recurse 
                    GetSelectedSubFolders(child);
                }
            }
        } 

        private void folderTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //  need to avoid open recurse 
            if (e.Node.Name.Equals(this.initialSelectedFolder))
            {
                return;
            }

            e.Node.Expand();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            this.GetSelectedFolders(); 
            this.ShowMessages();
        }

        /// <summary>
        /// If cells are selected instead of rows, select those rows
        /// </summary>
        private void SelectRowsFromCells()
        {
            if (this.mailGrid.SelectedRows.Count == 0 && this.mailGrid.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in this.mailGrid.SelectedCells)
                {
                    this.mailGrid.Rows[cell.RowIndex].Selected = true;
                }
            }
        }

        private void mailGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.SelectRowsFromCells();
            int index = e.RowIndex;
            if (this.messageList.ContainsKey(index))
            {
                Outlook.MailItem msg = this.messageList[index];
                try
                {
                    msg.Display(false);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }

        }

        private void FilterDisplay_Close (object sender, EventArgs e)
        {
            foreach (TreeNode node in this.folderTree.Nodes)
            {
                this.folderTree.Nodes.Remove(node);
            }
            this.sourceTree.ClearCheckBoxes();
        }

    }
}
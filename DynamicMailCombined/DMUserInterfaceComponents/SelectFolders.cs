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
    /// <summary>
    /// Class to display and select folders to
    /// index or search from
    /// </summary>
    public partial class SelectFolders : Form
    {
        private List<Outlook.MAPIFolder> folders = new List<Outlook.MAPIFolder>();
        Finishable source;

        public Outlook.MAPIFolder[] SelectedFolders
        {
            get { return (Outlook.MAPIFolder []) this.folders.ToArray(); }
        }

        public SelectFolders(Outlook.Folders roots, Finishable source)
        {
            InitializeComponent();
            this.source = source;
            foreach (Outlook.MAPIFolder folder in roots)
            {
                TreeNode tNode;
                // Add parent node to treeView1 control
                tNode = this.folderTree.Nodes.Add(folder.Name);
            }
            this.Visible = true;
        }

        private void RecurseDown()
        {
        }

        private void Done_Click(object sender, EventArgs e)
        {
            this.source.Finished();
        }

    }
}
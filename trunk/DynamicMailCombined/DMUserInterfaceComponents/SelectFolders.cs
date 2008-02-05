using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    public partial class SelectFolders : Form
    {
        public SelectFolders()
        {
            InitializeComponent();
            TreeNode tNode;
            // Add parent node to treeView1 control
            tNode = this.folderTree.Nodes.Add("A");
            // Add child node: two overloads available
            tNode.Nodes.Add(new TreeNode("C"));
            tNode.Nodes.Add("D");
            // Insert node after C
            tNode.Nodes.Insert(1,new TreeNode("E"));
            // Add parent node to treeView1 control 
            tNode = this.folderTree.Nodes.Add("B");
        }

    }
}
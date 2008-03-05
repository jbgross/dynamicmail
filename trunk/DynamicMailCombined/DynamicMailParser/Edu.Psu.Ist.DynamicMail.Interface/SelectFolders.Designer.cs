using System.Windows.Forms;
namespace Edu.Psu.Ist.DynamicMail.Interface
{
    partial class SelectFolders
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Done = new System.Windows.Forms.Button();
            this.folderTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // Done
            // 
            this.Done.Location = new System.Drawing.Point(180, 290);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(75, 23);
            this.Done.TabIndex = 2;
            this.Done.Text = "Done";
            this.Done.UseVisualStyleBackColor = true;
            this.Done.Click += new System.EventHandler(this.Done_Click);
            this.FormClosing += new FormClosingEventHandler(this.Done_Click);

            // 
            // folderTree
            // 
            this.folderTree.CheckBoxes = true;
            this.folderTree.Location = new System.Drawing.Point(12, 12);
            this.folderTree.Name = "folderTree";
            this.folderTree.Size = new System.Drawing.Size(268, 249);
            this.folderTree.TabIndex = 1;
            // 
            // SelectFolders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 338);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.folderTree);
            this.Name = "SelectFolders";
            this.Text = "SelectFolders";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Done;
        private System.Windows.Forms.TreeView folderTree;


    }
}
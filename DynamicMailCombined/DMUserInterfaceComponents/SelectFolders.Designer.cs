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
            this.folderList = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // folderList
            // 
            this.folderList.AcceptsReturn = true;
            this.folderList.AllowDrop = true;
            this.folderList.Location = new System.Drawing.Point(31, 24);
            this.folderList.Multiline = true;
            this.folderList.Name = "folderList";
            this.folderList.Size = new System.Drawing.Size(100, 20);
            this.folderList.TabIndex = 0;
            // 
            // SelectFolders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.folderList);
            this.Name = "SelectFolders";
            this.Text = "SelectFolders";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderList;

    }
}
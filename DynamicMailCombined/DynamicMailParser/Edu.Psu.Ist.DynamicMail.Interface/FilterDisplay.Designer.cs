namespace Edu.Psu.Ist.DynamicMail.Interface
{
    partial class FilterDisplay
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
            this.mailGrid = new System.Windows.Forms.DataGridView();
            this.SenderCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecipientCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubjectCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.folderTree = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.mailGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mailGrid
            // 
            this.mailGrid.AllowUserToAddRows = false;
            this.mailGrid.AllowUserToDeleteRows = false;
            this.mailGrid.AllowUserToOrderColumns = true;
            this.mailGrid.AllowUserToResizeRows = false;
            this.mailGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mailGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SenderCol,
            this.RecipientCol,
            this.SubjectCol,
            this.DateCol});
            this.mailGrid.Location = new System.Drawing.Point(243, 12);
            this.mailGrid.MultiSelect = false;
            this.mailGrid.Name = "mailGrid";
            this.mailGrid.ReadOnly = true;
            this.mailGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.mailGrid.Size = new System.Drawing.Size(628, 657);
            this.mailGrid.TabIndex = 2;
            this.mailGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mailGrid_CellContentDoubleClick);
            // 
            // SenderCol
            // 
            this.SenderCol.HeaderText = "Sender";
            this.SenderCol.Name = "SenderCol";
            this.SenderCol.ReadOnly = true;
            // 
            // RecipientCol
            // 
            this.RecipientCol.HeaderText = "Recipient";
            this.RecipientCol.Name = "RecipientCol";
            this.RecipientCol.ReadOnly = true;
            // 
            // SubjectCol
            // 
            this.SubjectCol.HeaderText = "Subject";
            this.SubjectCol.Name = "SubjectCol";
            this.SubjectCol.ReadOnly = true;
            this.SubjectCol.Width = 250;
            // 
            // DateCol
            // 
            this.DateCol.HeaderText = "Date";
            this.DateCol.Name = "DateCol";
            this.DateCol.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select Folders:";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(127, 646);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(88, 23);
            this.RefreshButton.TabIndex = 5;
            this.RefreshButton.Text = "Refresh View";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // folderTree
            // 
            this.folderTree.CheckBoxes = true;
            this.folderTree.Location = new System.Drawing.Point(15, 37);
            this.folderTree.Name = "folderTree";
            this.folderTree.Size = new System.Drawing.Size(210, 592);
            this.folderTree.TabIndex = 6;
            this.folderTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.folderTree_AfterCheck);
            // 
            // FilterDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 694);
            this.Controls.Add(this.folderTree);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mailGrid);
            this.Name = "FilterDisplay";
            this.Text = "FilterDisplay";
            ((System.ComponentModel.ISupportInitialize)(this.mailGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView mailGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.TreeView folderTree;
        private System.Windows.Forms.DataGridViewTextBoxColumn SenderCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecipientCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubjectCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCol;
    }
}
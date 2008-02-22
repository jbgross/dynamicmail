namespace Edu.Psu.Ist.DynamicMail.Interface
{
    partial class NetworkManager
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
            this.nameBox = new System.Windows.Forms.TextBox();
            this.addAccountButton = new System.Windows.Forms.Button();
            this.removeName = new System.Windows.Forms.Button();
            this.nameL = new System.Windows.Forms.Label();
            this.addContact = new System.Windows.Forms.Button();
            this.saveGroup = new System.Windows.Forms.Button();
            this.ignoreGroup = new System.Windows.Forms.Button();
            this.groupList = new System.Windows.Forms.DataGridView();
            this.emailCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.groupList)).BeginInit();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(333, 280);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(107, 20);
            this.nameBox.TabIndex = 3;
            // 
            // addAccountButton
            // 
            this.addAccountButton.Location = new System.Drawing.Point(333, 96);
            this.addAccountButton.Name = "addAccountButton";
            this.addAccountButton.Size = new System.Drawing.Size(107, 20);
            this.addAccountButton.TabIndex = 4;
            this.addAccountButton.Text = "Add Account";
            this.addAccountButton.UseVisualStyleBackColor = true;
            this.addAccountButton.Click += new System.EventHandler(this.addAddress_Click);
            // 
            // removeName
            // 
            this.removeName.Location = new System.Drawing.Point(333, 52);
            this.removeName.Name = "removeName";
            this.removeName.Size = new System.Drawing.Size(107, 20);
            this.removeName.TabIndex = 5;
            this.removeName.Text = "Remove Account";
            this.removeName.UseVisualStyleBackColor = true;
            this.removeName.Click += new System.EventHandler(this.removeName_Click);
            // 
            // nameL
            // 
            this.nameL.AutoSize = true;
            this.nameL.Location = new System.Drawing.Point(347, 252);
            this.nameL.Name = "nameL";
            this.nameL.Size = new System.Drawing.Size(78, 13);
            this.nameL.TabIndex = 6;
            this.nameL.Text = "Network Name";
            // 
            // addContact
            // 
            this.addContact.Location = new System.Drawing.Point(333, 122);
            this.addContact.Name = "addContact";
            this.addContact.Size = new System.Drawing.Size(107, 20);
            this.addContact.TabIndex = 7;
            this.addContact.Text = "Add Contact";
            this.addContact.UseVisualStyleBackColor = true;
            // 
            // saveGroup
            // 
            this.saveGroup.Location = new System.Drawing.Point(318, 319);
            this.saveGroup.Name = "saveGroup";
            this.saveGroup.Size = new System.Drawing.Size(70, 25);
            this.saveGroup.TabIndex = 11;
            this.saveGroup.Text = "Save";
            this.saveGroup.UseVisualStyleBackColor = true;
            this.saveGroup.Click += new System.EventHandler(this.saveGroup_Click);
            // 
            // ignoreGroup
            // 
            this.ignoreGroup.Location = new System.Drawing.Point(394, 319);
            this.ignoreGroup.Name = "ignoreGroup";
            this.ignoreGroup.Size = new System.Drawing.Size(70, 25);
            this.ignoreGroup.TabIndex = 12;
            this.ignoreGroup.Text = "Ignore";
            this.ignoreGroup.UseVisualStyleBackColor = true;
            this.ignoreGroup.Click += new System.EventHandler(this.ignoreGroup_Click);
            // 
            // groupList
            // 
            this.groupList.AllowUserToAddRows = false;
            this.groupList.AllowUserToDeleteRows = false;
            this.groupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.emailCol,
            this.nameCol});
            this.groupList.Location = new System.Drawing.Point(12, 12);
            this.groupList.Name = "groupList";
            this.groupList.ReadOnly = true;
            this.groupList.Size = new System.Drawing.Size(293, 356);
            this.groupList.TabIndex = 13;
            // 
            // emailCol
            // 
            this.emailCol.HeaderText = "Email";
            this.emailCol.Name = "emailCol";
            this.emailCol.ReadOnly = true;
            this.emailCol.Width = 120;
            // 
            // nameCol
            // 
            this.nameCol.HeaderText = "Name";
            this.nameCol.Name = "nameCol";
            this.nameCol.ReadOnly = true;
            this.nameCol.Width = 120;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(333, 23);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(107, 23);
            this.editButton.TabIndex = 14;
            this.editButton.Text = "Edit Account";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // NetworkManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 380);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.groupList);
            this.Controls.Add(this.ignoreGroup);
            this.Controls.Add(this.saveGroup);
            this.Controls.Add(this.addContact);
            this.Controls.Add(this.nameL);
            this.Controls.Add(this.removeName);
            this.Controls.Add(this.addAccountButton);
            this.Controls.Add(this.nameBox);
            this.Name = "NetworkManager";
            this.Text = "Network Manager";
            ((System.ComponentModel.ISupportInitialize)(this.groupList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button addAccountButton;
        private System.Windows.Forms.Button removeName;
        private System.Windows.Forms.Label nameL;
        private System.Windows.Forms.Button addContact;
        private System.Windows.Forms.Button saveGroup;
        private System.Windows.Forms.Button ignoreGroup;
        private System.Windows.Forms.DataGridView groupList;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameCol;
    }
}
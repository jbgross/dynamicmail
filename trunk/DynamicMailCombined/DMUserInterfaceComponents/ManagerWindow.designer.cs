namespace Edu.Psu.Ist.DynamicMail.Interface
{
    partial class ManagerWindow
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.addAddress = new System.Windows.Forms.Button();
            this.removeName = new System.Windows.Forms.Button();
            this.nameL = new System.Windows.Forms.Label();
            this.addContact = new System.Windows.Forms.Button();
            this.iconButton = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.saveGroup = new System.Windows.Forms.Button();
            this.ignoreGroup = new System.Windows.Forms.Button();
            this.groupList = new System.Windows.Forms.DataGridView();
            this.emailCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupList)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(467, 90);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 107);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(162, 260);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(228, 281);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(118, 20);
            this.nameBox.TabIndex = 3;
            // 
            // addAddress
            // 
            this.addAddress.Location = new System.Drawing.Point(190, 307);
            this.addAddress.Name = "addAddress";
            this.addAddress.Size = new System.Drawing.Size(75, 20);
            this.addAddress.TabIndex = 4;
            this.addAddress.Text = "Add Address";
            this.addAddress.UseVisualStyleBackColor = true;
            // 
            // removeName
            // 
            this.removeName.Location = new System.Drawing.Point(277, 307);
            this.removeName.Name = "removeName";
            this.removeName.Size = new System.Drawing.Size(69, 20);
            this.removeName.TabIndex = 5;
            this.removeName.Text = "Remove";
            this.removeName.UseVisualStyleBackColor = true;
            // 
            // nameL
            // 
            this.nameL.AutoSize = true;
            this.nameL.Location = new System.Drawing.Point(187, 284);
            this.nameL.Name = "nameL";
            this.nameL.Size = new System.Drawing.Size(35, 13);
            this.nameL.TabIndex = 6;
            this.nameL.Text = "Name";
            // 
            // addContact
            // 
            this.addContact.Location = new System.Drawing.Point(191, 333);
            this.addContact.Name = "addContact";
            this.addContact.Size = new System.Drawing.Size(74, 20);
            this.addContact.TabIndex = 7;
            this.addContact.Text = "Add Contact";
            this.addContact.UseVisualStyleBackColor = true;
            // 
            // iconButton
            // 
            this.iconButton.Location = new System.Drawing.Point(362, 281);
            this.iconButton.Name = "iconButton";
            this.iconButton.Size = new System.Drawing.Size(63, 20);
            this.iconButton.TabIndex = 9;
            this.iconButton.Text = "Icon";
            this.iconButton.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(441, 280);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 40);
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // saveGroup
            // 
            this.saveGroup.Location = new System.Drawing.Point(334, 342);
            this.saveGroup.Name = "saveGroup";
            this.saveGroup.Size = new System.Drawing.Size(70, 25);
            this.saveGroup.TabIndex = 11;
            this.saveGroup.Text = "Save";
            this.saveGroup.UseVisualStyleBackColor = true;
            // 
            // ignoreGroup
            // 
            this.ignoreGroup.Location = new System.Drawing.Point(410, 342);
            this.ignoreGroup.Name = "ignoreGroup";
            this.ignoreGroup.Size = new System.Drawing.Size(70, 25);
            this.ignoreGroup.TabIndex = 12;
            this.ignoreGroup.Text = "Ignore";
            this.ignoreGroup.UseVisualStyleBackColor = true;
            // 
            // groupList
            // 
            this.groupList.AllowUserToAddRows = false;
            this.groupList.AllowUserToDeleteRows = false;
            this.groupList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.emailCol,
            this.nameCol});
            this.groupList.Location = new System.Drawing.Point(188, 112);
            this.groupList.Name = "groupList";
            this.groupList.ReadOnly = true;
            this.groupList.Size = new System.Drawing.Size(290, 145);
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
            // ManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 383);
            this.Controls.Add(this.groupList);
            this.Controls.Add(this.ignoreGroup);
            this.Controls.Add(this.saveGroup);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.iconButton);
            this.Controls.Add(this.addContact);
            this.Controls.Add(this.nameL);
            this.Controls.Add(this.removeName);
            this.Controls.Add(this.addAddress);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ManagerWindow";
            this.Text = "Manager";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button addAddress;
        private System.Windows.Forms.Button removeName;
        private System.Windows.Forms.Label nameL;
        private System.Windows.Forms.Button addContact;
        private System.Windows.Forms.Button iconButton;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button saveGroup;
        private System.Windows.Forms.Button ignoreGroup;
        private System.Windows.Forms.DataGridView groupList;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameCol;
    }
}
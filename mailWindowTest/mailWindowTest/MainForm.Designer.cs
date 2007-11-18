namespace mailWindowTest
{
    partial class MainForm
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
            this.closeButton = new System.Windows.Forms.Button();
            this.myMail = new System.Windows.Forms.DataGridView();
            this.populateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.myMail)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(382, 411);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // myMail
            // 
            this.myMail.AllowUserToAddRows = false;
            this.myMail.AllowUserToDeleteRows = false;
            this.myMail.AllowUserToResizeRows = false;
            this.myMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myMail.Location = new System.Drawing.Point(31, 33);
            this.myMail.Name = "myMail";
            this.myMail.Size = new System.Drawing.Size(355, 302);
            this.myMail.TabIndex = 1;

            // 
            // populateButton
            // 
            this.populateButton.Location = new System.Drawing.Point(52, 399);
            this.populateButton.Name = "populateButton";
            this.populateButton.Size = new System.Drawing.Size(75, 23);
            this.populateButton.TabIndex = 2;
            this.populateButton.Text = "Populate";
            this.populateButton.UseVisualStyleBackColor = true;
            this.populateButton.Click += new System.EventHandler(this.populateButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 456);
            this.Controls.Add(this.populateButton);
            this.Controls.Add(this.myMail);
            this.Controls.Add(this.closeButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.myMail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.DataGridView myMail;
        private System.Windows.Forms.Button populateButton;
    }
}
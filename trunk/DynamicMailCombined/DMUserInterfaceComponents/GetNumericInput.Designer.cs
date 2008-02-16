namespace Edu.Psu.Ist.DynamicMail.Interface
{
    partial class GetNumericInput
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
            this.OpenLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NumberBox = new System.Windows.Forms.NumericUpDown();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelProcessButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenLabel
            // 
            this.OpenLabel.AutoSize = true;
            this.OpenLabel.Location = new System.Drawing.Point(12, 14);
            this.OpenLabel.Name = "OpenLabel";
            this.OpenLabel.Size = new System.Drawing.Size(141, 13);
            this.OpenLabel.TabIndex = 0;
            this.OpenLabel.Text = "this will be set to some value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "(if unknown, leave at default)";
            // 
            // NumberBox
            // 
            this.NumberBox.Location = new System.Drawing.Point(230, 14);
            this.NumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumberBox.Name = "NumberBox";
            this.NumberBox.Size = new System.Drawing.Size(72, 20);
            this.NumberBox.TabIndex = 2;
            this.NumberBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(149, 47);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // CancelProcessButton
            // 
            this.CancelProcessButton.Location = new System.Drawing.Point(230, 47);
            this.CancelProcessButton.Name = "CancelProcessButton";
            this.CancelProcessButton.Size = new System.Drawing.Size(75, 23);
            this.CancelProcessButton.TabIndex = 4;
            this.CancelProcessButton.Text = "Cancel";
            this.CancelProcessButton.UseVisualStyleBackColor = true;
            this.CancelProcessButton.Click += new System.EventHandler(this.CancelProcessButton_Click);
            // 
            // GetNumericInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 82);
            this.Controls.Add(this.CancelProcessButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.NumberBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OpenLabel);
            this.Name = "GetNumericInput";
            this.Text = "Get Numeric Input";
            ((System.ComponentModel.ISupportInitialize)(this.NumberBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label OpenLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NumberBox;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelProcessButton;
    }
}
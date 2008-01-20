namespace Edu.Psu.Ist.DynamicMail.Interface
{
    partial class ProgressInfoBox
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
            this.progress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TimeRemaining = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ItemsLeft = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(41, 41);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(295, 22);
            this.progress.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Operation Progress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Estimated Time to Completion: ";
            // 
            // TimeRemaining
            // 
            this.TimeRemaining.AutoSize = true;
            this.TimeRemaining.Location = new System.Drawing.Point(196, 112);
            this.TimeRemaining.Name = "TimeRemaining";
            this.TimeRemaining.Size = new System.Drawing.Size(13, 13);
            this.TimeRemaining.TabIndex = 3;
            this.TimeRemaining.Text = "?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Items Left to Index: ";
            // 
            // ItemsLeft
            // 
            this.ItemsLeft.AutoSize = true;
            this.ItemsLeft.Location = new System.Drawing.Point(196, 89);
            this.ItemsLeft.Name = "ItemsLeft";
            this.ItemsLeft.Size = new System.Drawing.Size(13, 13);
            this.ItemsLeft.TabIndex = 5;
            this.ItemsLeft.Text = "?";
            // 
            // ProgressInfoBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 156);
            this.Controls.Add(this.ItemsLeft);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TimeRemaining);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progress);
            this.Name = "ProgressInfoBox";
            this.Text = "Progress Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TimeRemaining;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ItemsLeft;
    }
}
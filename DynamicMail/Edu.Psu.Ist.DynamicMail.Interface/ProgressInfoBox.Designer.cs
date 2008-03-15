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
            this.ItemsRemaining = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TotalTime = new System.Windows.Forms.Label();
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
            this.label2.Location = new System.Drawing.Point(8, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Estimated Time to Completion: ";
            // 
            // TimeRemaining
            // 
            this.TimeRemaining.AutoSize = true;
            this.TimeRemaining.Location = new System.Drawing.Point(179, 112);
            this.TimeRemaining.Name = "TimeRemaining";
            this.TimeRemaining.Size = new System.Drawing.Size(13, 13);
            this.TimeRemaining.TabIndex = 3;
            this.TimeRemaining.Text = "?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Items Left to Index: ";
            // 
            // ItemsRemaining
            // 
            this.ItemsRemaining.AutoSize = true;
            this.ItemsRemaining.Location = new System.Drawing.Point(179, 89);
            this.ItemsRemaining.Name = "ItemsRemaining";
            this.ItemsRemaining.Size = new System.Drawing.Size(13, 13);
            this.ItemsRemaining.TabIndex = 5;
            this.ItemsRemaining.Text = "?";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(278, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "Stop Indexing and Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Total Duration So Far:";
            // 
            // TotalTime
            // 
            this.TotalTime.AutoSize = true;
            this.TotalTime.Location = new System.Drawing.Point(179, 134);
            this.TotalTime.Name = "TotalTime";
            this.TotalTime.Size = new System.Drawing.Size(13, 13);
            this.TotalTime.TabIndex = 8;
            this.TotalTime.Text = "?";
            // 
            // ProgressInfoBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 171);
            this.Controls.Add(this.TotalTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ItemsRemaining);
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
        private System.Windows.Forms.Label ItemsRemaining;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label TotalTime;
    }
}
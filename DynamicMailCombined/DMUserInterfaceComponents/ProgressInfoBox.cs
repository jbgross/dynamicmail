using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Edu.Psu.Ist.DynamicMail;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    public partial class ProgressInfoBox : Form
    {
        int incrementQuantity;
        DateTime startTime;
        int totalCount;
        private Stoppable stop;

        /// <summary>
        /// Create a new progress info box with a ref back
        /// to the calling process, so we can stop it
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="stop"></param>
        public ProgressInfoBox(int totalCount, Stoppable stop)
        {
            this.totalCount = totalCount;
            this.stop = stop;
            this.incrementQuantity = totalCount / 100;
            InitializeComponent();
            this.startTime = DateTime.Now;
            CheckForIllegalCrossThreadCalls = false;
            this.Visible = true;
            this.Refresh();
        }

        /// <summary>
        /// Increment the progress bar by specifying the units
        /// left, and recalculate the time left
        /// </summary>
        /// <param name="itemsLeft"></param>
        public void Increment(int itemsLeft)
        {
            this.progress.Value++;
            this.ItemsLeft.Text = itemsLeft.ToString();
            this.Refresh();
            int unitsLeft = (100 - this.progress.Value);
            DateTime currentTime = DateTime.Now;
            TimeSpan totalDuration = currentTime - this.startTime;
            double unitTimeSoFar = (totalDuration.TotalSeconds / this.progress.Value);
            double timeLeft = unitsLeft * unitTimeSoFar;
            this.TimeRemaining.Text = this.ConvertTime(timeLeft);
        }

        private String ConvertTime(double millisec)
        {
            int sec = (int) millisec / 1000;
            if (sec <= 60)
            {
                return "00:" + this.PrependZero(sec);
            }
            else
            {
                int min = sec / 60;
                sec %= 60;
                return this.PrependZero(min) + ":" + this.PrependZero(sec);
            }

        }

        private String PrependZero(int num)
        {
            if (num < 10)
            {
                return "0" + num;
            }
            else
            {
                return num.ToString();
            }
        }

        protected void finalize()
        {
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.stop.Stop();
        }

    }
}
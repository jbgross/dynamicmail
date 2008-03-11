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
        private int incrementQuantity;
        private DateTime startTime;
        private DateTime lastTime;
        private int totalCount;
        private Stoppable stop;

        /// <summary>
        /// Create a new progress info box with a ref back
        /// to the calling process, so we can stop it
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="stop"></param>
        public ProgressInfoBox(int totalCount, Stoppable stop)
        {
            InitializeComponent();
            this.totalCount = totalCount;
            this.ItemsRemaining.Text = totalCount.ToString();
            this.stop = stop;
            this.incrementQuantity = totalCount / 100;
            this.startTime = DateTime.Now;
            this.lastTime = DateTime.Now;
            CheckForIllegalCrossThreadCalls = false;
            this.Show();
            this.Focus();
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
            this.ItemsRemaining.Text = itemsLeft.ToString();
            this.Refresh();
            int unitsLeft = (100 - this.progress.Value);
            DateTime currentTime = DateTime.Now;

            // get the average time to completion
            TimeSpan totalDurationSoFar = currentTime - this.startTime;
            double unitTimeSoFar = totalDurationSoFar.TotalSeconds / this.progress.Value;
            double meanTimeLeft = unitsLeft * unitTimeSoFar;

            // get the time to completion based on the last iteration
            //TimeSpan localDuration = currentTime - this.lastTime;
            //double localUnitTime = localDuration.TotalSeconds / this.progress.Value;
            //double localTimeLeft = unitsLeft * localUnitTime;

            // use whichever is higher
            //double higherTime = meanTimeLeft > localTimeLeft ? meanTimeLeft : localTimeLeft;

            //this.TimeRemaining.Text = this.ConvertTime((int) higherTime);
            this.TimeRemaining.Text = this.ConvertTime((int)meanTimeLeft);
            TimeSpan totalDuration = currentTime - this.startTime;
            this.TotalTime.Text = this.ConvertTime((int)totalDuration.TotalSeconds);
            this.lastTime = currentTime;
            this.Focus();
        }

        /// <summary>
        /// Change the text of the "Items Remaining" field
        /// </summary>
        /// <param name="text"></param>
        public void ChangeText(String text)
        {
            this.ItemsRemaining.Text = text;
            this.TimeRemaining.Text = "?";
        }

        private String ConvertTime(int sec)
        {
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
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
        int startTime;
        int totalCount;

        public ProgressInfoBox(int totalCount)
        {
            this.totalCount = totalCount;
            this.incrementQuantity = totalCount / 100;
            InitializeComponent();
            this.startTime = Environment.TickCount;
            CheckForIllegalCrossThreadCalls = false;
            this.Visible = true;
            this.Refresh();
        }


        public void Increment(int itemsLeft)
        {
            this.progress.Value++;
            this.ItemsLeft.Text = itemsLeft.ToString();
            this.Refresh();
            int unitsLeft = (100 - this.progress.Value);
            int currentTime = Environment.TickCount;
            int timeSoFar = currentTime - this.startTime;
            int unitTimeSoFar = timeSoFar / this.progress.Value;
//            this.TimeRemaining.Text = unitTimeSoFar.ToString();
            int timeLeft = unitsLeft * unitTimeSoFar;
            this.TimeRemaining.Text = this.ConvertTime(timeLeft);
        }

        public String ConvertTime(int milliseconds)
        {
            int sec = milliseconds / 1000;
            if (sec <= 100)
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

    }
}
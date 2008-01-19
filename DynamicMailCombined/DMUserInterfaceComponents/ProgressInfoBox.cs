using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    public partial class ProgressInfoBox : Form
    {
        int totalCount;
        int increments;
        int incrementQuantity;

        public int IncrementQuantity
        {
            get { return incrementQuantity; }
            private set { incrementQuantity = value; }
        }

        int startTime;

        public ProgressInfoBox(int totalCount)
        {
            this.totalCount = totalCount;
            IncrementQuantity = totalCount / 100;
            InitializeComponent();
            this.startTime = Environment.TickCount;
            this.Refresh();
        }

        public void Increment()
        {
            if (this.progress.Value < 100)
            {
                this.progress.Value += 1;
                int unitsLeft = (100 - this.progress.Value);
                int currentTime = Environment.TickCount;
                int timeSoFar = currentTime - startTime;
                int unitTimeSoFar = timeSoFar / this.progress.Value;
                if (this.progress.Value > 0 && unitTimeSoFar > 0)
                {
                    int timeLeft = unitsLeft / unitTimeSoFar;
                    this.TimeRemaining.Text = timeLeft.ToString();
                }
            }
            this.Refresh();
            Thread.Sleep(100);
        }

    }
}
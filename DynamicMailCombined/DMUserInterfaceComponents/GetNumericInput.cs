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
    public partial class GetNumericInput : Form
    {
        private Finishable finishable;

        /// <summary>
        /// Get the count
        /// </summary>
        public int Count
        {
            get { return (int) this.NumberBox.Value; }
            private set { this.NumberBox.Value = value; }
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public GetNumericInput(String message, int initialValue, Finishable finish)
        {
            this.finishable = finish;
            InitializeComponent();
            Count = initialValue;
            this.OpenLabel.Text = message;
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Count = (int) this.NumberBox.Value;
            this.Close();
            finishable.Finish();
        }

        private void CancelProcessButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.finishable.Cancel();
        }

    }
}
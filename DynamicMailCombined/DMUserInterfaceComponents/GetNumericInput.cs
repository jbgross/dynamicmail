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
        bool done = false;

        public bool Done
        {
            get { return done; }
            private set { done = value; }
        }

        int count = 0;

        /// <summary>
        /// Get the count
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public GetNumericInput()
        {
            InitializeComponent();
            Visible = true;

            //lock this object to block main thread
            Monitor.Enter(this);
        }

        /// <summary>
        /// Placeholder method to wait for data
        /// </summary>
        [STAThread]
        public void Run()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.count = (int) this.countSN.Value;
            Done = true;
            Visible = false;
            Monitor.Exit(this);
        }

    }
}
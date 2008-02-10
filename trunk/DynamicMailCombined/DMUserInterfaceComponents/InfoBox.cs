using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    /// <summary>
    /// Just print out data to a textbox
    /// </summary>
    public partial class InfoBox : Form
    {


        /// <summary>
        /// Get the textbox for this object
        /// </summary>
        public void AddText(String text) {
            this.textBox1.AppendText(text);
            this.textBox1.AppendText(System.Environment.NewLine);
            this.Refresh();
        }

        /// <summary>
        /// Public constructor
        /// </summary>
        public InfoBox()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.Visible = true;
            this.Refresh();
        }
    }
}
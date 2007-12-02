using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mailWindowTest
{
    public partial class MainForm : Form
    {
        DataTable myMailData = new DataTable();
        

        public MainForm()
        {
            InitializeComponent();
        }



        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void populateButton_Click(object sender, EventArgs e)
        {

            myMailData = ThisApplication.generateDataTable();

            myMail.DataSource = myMailData;
           // myMail.Columns["EntryID:"].Visible = false;
        }

    }
}
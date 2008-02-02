using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    public partial class ManagerWindow : Form
    {
        public ManagerWindow()
        {
            InitializeComponent();
            populateList();
        }

        public void populateList()
        {
            string[] row0 = { "Serena Mei", "scm227@psu.edu" };
            string[] row1 = { "Joshua Gross", "jgross@ist.psu.edu" };
            string[] row2 = { "Dave Reber", "dreber@gmail.com" };
            string[] row3 = { "Bob Smith", "knowitall@gmail.com" };
            string[] row4 = { "Ryan Foster", "rrf5000@psu.edu" };
            string[] row5 = { "Jeff Vernon", "jmv5001@psu.edu" };
            string[] row6 = { "Anthony Orenga", "ajo132@psu.edu" };
            string[] row7 = { "Michael Zalewski", "mez5001@psu.edu" };
            string[] row8 = { "Michael Kozeniaskus", "mrk211@psu.edu" };
            string[] row9 = { "Sally Haynes", "s.haynes@gmail.com" };

            groupList.Rows.Add(row0);
            groupList.Rows.Add(row1);
            groupList.Rows.Add(row2);
            groupList.Rows.Add(row3);
            groupList.Rows.Add(row4);
            groupList.Rows.Add(row5);
            groupList.Rows.Add(row6);
            groupList.Rows.Add(row7);
            groupList.Rows.Add(row8);
            groupList.Rows.Add(row9);

        }

        private void groupList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
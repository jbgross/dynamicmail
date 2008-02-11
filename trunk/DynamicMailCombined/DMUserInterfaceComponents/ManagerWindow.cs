using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Threading;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Ist.Psu.DynamicMail;

namespace Edu.Psu.Ist.DynamicMail
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

        private void addAddress_Click(object sender, EventArgs e)
        {
            AddAddress addWindow = new AddAddress();

            addWindow.Show();
            
        }

        private void saveGroup_Click(object sender, EventArgs e)
        {
            //create an XMP writer object
            ObjectXmlWriter WriteXML = new ObjectXmlWriter();

            //create Index List of the dataGridView's columns
            List<Object> groupIList = new List<Object>();
            groupIList.Add(groupList.Columns.IndexOf(nameCol));
            groupIList.Add(groupList.Columns.IndexOf(emailCol));

            //send list to XML writer to write to the specified file
            WriteXML.WriteObjectXml(groupIList, "c:\\groupList.xml");
        }

        private void ignoreGroup_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void removeName_Click(object sender, EventArgs e)
        {
            if (this.groupList.SelectedRows.Count > 0 &&
                this.groupList.SelectedRows[0].Index !=
                this.groupList.Rows.Count - 1)
            {
                groupList.Rows.RemoveAt(groupList.SelectedRows[0].Index);
            }
        }

        private void addContact_Click(object sender, EventArgs e)
        {

        }

        
    }
}
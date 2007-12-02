using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Data;

namespace mailWindowTest
{
    public partial class ThisApplication
    {
        private void ThisApplication_Startup(object sender, System.EventArgs e)
        {

            Outlook.MAPIFolder inbox = this.ActiveExplorer().Session.
                    GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            Outlook.Items items = inbox.Items;

            generateDataTable(items);

            MainForm formMain = new MainForm();
            formMain.ShowDialog();

        }
  


        public void createTestMailItems()
        {

            Outlook.MailItem mail1 = (Outlook.MailItem)this.CreateItem(Outlook.OlItemType.olMailItem);
            mail1.Subject = "Testing";
            mail1.To = "dave@google.com";
            mail1.Body = "I am testing an email";

            Outlook.MailItem mail2 = (Outlook.MailItem)this.CreateItem(Outlook.OlItemType.olMailItem);
            mail1.Subject = "ForScore";
            mail1.To = "abe@gettysburg.com";
            mail1.Body = "Four Score and Seven years ago, our forefathers...";

            Outlook.MailItem mail3 = (Outlook.MailItem)this.CreateItem(Outlook.OlItemType.olMailItem);
            mail1.Subject = "aDream";
            mail1.To = "mluther1@cia.gov";
            mail1.Body = "I have a dream.  I'm falling and I wake up";


        }

        public static int /*DataTable*/ generateDataTable(Outlook.Items mailItems)
        {
            //create data table with colmns to hold mail info
            DataTable table = new DataTable();
            //table.TableName = "myMail";
            //table.Columns.Add("EntryID:", typeof(string));
            //table.Columns.Add("To:", typeof(string));
            //table.Columns.Add("Subject:", typeof(string));
            //table.Columns.Add("Message:", typeof(string));

            int count = 0;
            foreach( Outlook.MailItem email in mailItems)
            {
                count++;
            }

            MessageBox.Show("Count = " + count);
 
            


            //table.Rows.Add(new object[] { mail1.EntryID.ToString(), mail1.To.ToString(), mail1.Subject.ToString(), mail1.Body.ToString() });
            //table.Rows.Add(new object[] { mail2.EntryID.ToString(), mail2.To.ToString(), mail2.Subject.ToString(), mail2.Body.ToString() });
            //table.Rows.Add(new object[] { mail3.EntryID.ToString(), mail3.To.ToString(), mail3.Subject.ToString(), mail3.Body.ToString() });
            //table.Rows.Add(new object[] {"test1", "test2", "test3", "test4" });
            
            //table.AcceptChanges();

            //return table;
            return 0;

        }


       

        private void ThisApplication_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisApplication_Startup);
            this.Shutdown += new System.EventHandler(ThisApplication_Shutdown);
        }

        #endregion
    }
}

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
            MainForm formMain = new MainForm();
            formMain.ShowDialog();

            

          
        }

        public DataTable generateDataTable()
        {
            //create data table with colmns to hold mail info
            DataTable table = new DataTable();
            table.TableName = "myMail";
            table.Columns.Add("EntryID:", typeof(string));
            table.Columns.Add("To:", typeof(string));
            table.Columns.Add("Subject:", typeof(string));
            table.Columns.Add("Message:", typeof(string));

            //get some mail
            //for testing, we will create some mail
            Outlook.MailItem mail1 = createMailItem("Testing", "dave@google.com", "I am testing an email");
            Outlook.MailItem mail2 = createMailItem("ForScore", "abe@gettysburg.com", "Four Score and Seven years ago, our forefathers...");
            Outlook.MailItem mail3 = createMailItem("aDream", "mluther1@cia.gov", "I have a dream.  I'm falling and I wake up");
            

            table.Rows.Add(new object[] { mail1.EntryID, mail1.To, mail1.Subject, mail1.Body });
            table.Rows.Add(new object[] { mail2.EntryID, mail2.To, mail2.Subject, mail2.Body });
            table.Rows.Add(new object[] { mail3.EntryID, mail3.To, mail3.Subject, mail3.Body });
            
            
            table.AcceptChanges();

            return table;

        }

        public Outlook.MailItem createMailItem(string subject, string to, string body)
        {
            Outlook.MailItem mailItem = (Outlook.MailItem)this.CreateItem(Outlook.OlItemType.olMailItem);
            mailItem.Subject = subject;
            mailItem.To = to;
            mailItem.Body = body;
            mailItem.Display(false);

            return mailItem;
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

using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Edu.Psu.Ist.DynamicMail;

namespace Edu.Psu.Ist.DynamicMail
{
    public partial class ThisApplication
    {
        Office.CommandBar newToolBar;
        Office.CommandBarButton firstButton;
        Office.CommandBarButton secondButton;
        Office.CommandBarButton thirdButton;
        Outlook.Explorers selectExplorers;

        private void ThisApplication_Startup(object sender, System.EventArgs e)
        {

            selectExplorers = this.Explorers;
            selectExplorers.NewExplorer += new Outlook
                .ExplorersEvents_NewExplorerEventHandler(newExplorer_Event);
            AddToolbar();
        }

        private void newExplorer_Event(Outlook.Explorer new_Explorer)
        {
            ((Outlook._Explorer)new_Explorer).Activate();
            newToolBar = null;
            AddToolbar();
        }

        private void AddToolbar()
        {

            if (newToolBar == null)
            {
                Office.CommandBars cmdBars = this.ActiveExplorer().CommandBars;
                newToolBar = cmdBars.Add("Dynamic Mail Toolbar",
                    Office.MsoBarPosition.msoBarTop, false, true);
            }
            try
            {
                Office.CommandBarButton button_1 =
                    (Office.CommandBarButton)newToolBar.Controls
                    .Add(1, missing, missing, missing, missing);
                button_1.Style = Office
                    .MsoButtonStyle.msoButtonCaption;
                button_1.Caption = "Index Inbox";
                button_1.Tag = "InboxIndexer";
                if (this.firstButton == null)
                {
                    this.firstButton = button_1;
                    firstButton.Click += new Office.
                        _CommandBarButtonEvents_ClickEventHandler
                        (ButtonClick);
                }

                Office.CommandBarButton button_2 = (Office
                    .CommandBarButton)newToolBar.Controls.Add
                    (1, missing, missing, missing, missing);
                button_2.Style = Office
                    .MsoButtonStyle.msoButtonCaption;
                button_2.Caption = "Index Sent Mail";
                button_2.Tag = "SentMailIndexer";
                newToolBar.Visible = true;
                if (this.secondButton == null)
                {
                    this.secondButton = button_2;
                    secondButton.Click += new Office.
                        _CommandBarButtonEvents_ClickEventHandler
                        (ButtonClick);
                }

                Office.CommandBarButton button_3 = (Office
                    .CommandBarButton)newToolBar.Controls.Add
                    (1, missing, missing, missing, missing);
                button_3.Style = Office
                    .MsoButtonStyle.msoButtonCaption;
                button_3.Caption = "Index Contacts";
                button_3.Tag = "ContactsIndexer";
                newToolBar.Visible = true;
                if (this.thirdButton == null)
                {
                    this.thirdButton = button_3;
                    thirdButton.Click += new Office.
                        _CommandBarButtonEvents_ClickEventHandler
                        (ButtonClick);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonClick(Office.CommandBarButton ctrl,
                ref bool cancel)
        {
            //get the inbox
            Outlook.MAPIFolder inbox = this.ActiveExplorer().Session.
                GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
            Outlook.Items inboxItems = inbox.Items;

            //get the contacts folder
            Outlook.MAPIFolder contacts = this.ActiveExplorer().Session.
                GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts);
            Outlook.Items contactItems = contacts.Items;

            //get the sent mail folder
            Outlook.MAPIFolder sentMail = this.ActiveExplorer().Session.
                GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
            Outlook.Items sentItems = sentMail.Items;

            //create a new parser
            //Edu.Psu.Ist.DynamicMail.DynamicMailParser newParser;

            //display what button was clicked
            MessageBox.Show("You clicked: " + ctrl.Caption);

            ////if
            //if (ctrl.Caption == "InboxIndexer")
            //{

            //    newParser.InboxIndexer(inboxItems, contacts);
            //}
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

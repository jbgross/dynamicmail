using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Threading;
using Edu.Psu.Ist.DynamicMail.Interface;
using Edu.Ist.Psu.DynamicMail;


namespace Edu.Psu.Ist.DynamicMail
{
    public partial class ThisApplication
    {
        Office.CommandBar newToolBar;
        Office.CommandBarButton seeFolders;
        Office.CommandBarButton indexMailboxes;
        Office.CommandBarButton indexSentButton;
        Office.CommandBarButton clusterContactsButton;
        Office.CommandBarButton manageGroupButton;
        Outlook.Explorers selectExplorers;

        DynamicMailParser parser = new DynamicMailParser();

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
                Office.CommandBars cmdBars =
                    this.ActiveExplorer().CommandBars;
                newToolBar = cmdBars.Add("Email Groups",
                    Office.MsoBarPosition.msoBarTop, false, true);
            }
            try
            {
                Office.CommandBarButton button_0 =
                    (Office.CommandBarButton)newToolBar.Controls
                    .Add(1, missing, missing, missing, missing);
                button_0.Style = Office
                    .MsoButtonStyle.msoButtonCaption;
                button_0.Caption = "Index Mailboxes";
                button_0.Tag = "mailboxes";
                if (this.indexMailboxes == null)
                {
                    this.indexMailboxes = button_0;
                    indexMailboxes.Click += new Office.
                        _CommandBarButtonEvents_ClickEventHandler
                        (ButtonClick);
                }

                Office.CommandBarButton button_2 = (Office
                    .CommandBarButton)newToolBar.Controls.Add
                    (1, missing, missing, missing, missing);
                button_2.Style = Office
                    .MsoButtonStyle.msoButtonCaption;
                button_2.Caption = "Index Sent Mail";
                button_2.Tag = "sent";
                newToolBar.Visible = true;
                if (this.indexSentButton == null)
                {
                    this.indexSentButton = button_2;
                    indexSentButton.Click += new Office.
                        _CommandBarButtonEvents_ClickEventHandler
                        (ButtonClick);
                }
                
                Office.CommandBarButton clusterButton = (Office
                                    .CommandBarButton)newToolBar.Controls.Add
                                    (1, missing, missing, missing, missing);
                clusterButton.Style = Office
                    .MsoButtonStyle.msoButtonCaption;
                clusterButton.Caption = "Cluster Contacts";
                clusterButton.Tag = "cluster";
                newToolBar.Visible = true;
                if (this.clusterContactsButton == null)
                {
                    this.clusterContactsButton = clusterButton;
                    clusterContactsButton.Click += new Office.
                        _CommandBarButtonEvents_ClickEventHandler
                        (ButtonClick);
                }
                Office.CommandBarButton managerButton = (Office
                                    .CommandBarButton)newToolBar.Controls.Add
                                    (1, missing, missing, missing, missing);
                managerButton.Style = Office
                    .MsoButtonStyle.msoButtonCaption;
                managerButton.Caption = "Manage Group";
                managerButton.Tag = "manage";
                newToolBar.Visible = true;
                if (this.manageGroupButton == null)
                {
                    this.manageGroupButton = managerButton;
                    manageGroupButton.Click += new Office.
                        _CommandBarButtonEvents_ClickEventHandler
                        (ButtonClick1);
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
            try
            {
                Outlook.MAPIFolder inbox = this.ActiveExplorer().Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
                Outlook.MAPIFolder sentBox = this.ActiveExplorer().Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
                Outlook.MAPIFolder contacts = this.ActiveExplorer().Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts);
                Outlook.Folders allFolders = this.ActiveExplorer().Session.Folders;

                if (ctrl.Tag == "mailboxes")
                {
                    new IndexMailboxes(allFolders, Indexes.Instance);
                }
                if (ctrl.Tag == "sent")
                {
                    parser.sentBoxIndexer(sentBox, contacts);
                    Indexes.Instance.WriteIndexToXML();

                }
                if (ctrl.Tag == "contacts")
                {
                    parser.contactsIndexer(contacts);
                    Indexes.Instance.WriteIndexToXML();
                }
                if (ctrl.Tag.Equals("cluster"))
                {
                    PrepareClusterData pcd = new PrepareClusterData();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            //MessageBox.Show("You clicked: " + ctrl.Caption);
        }

        private void ButtonClick1(Office.CommandBarButton ctrl,
                ref bool cancel)
        {
            //Application.Run(new ManagerWindow());
            ManagerWindow mWindow = new ManagerWindow();
            mWindow.Show();
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

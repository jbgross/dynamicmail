using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Edu.Psu.Ist.DynamicMail.Interface;
using System.Collections.Generic;
using Edu.Psu.Ist.DynamicMail.Parse;
using Edu.Psu.Ist.DynamicMail;

namespace DynamicMail
{
    public partial class ThisAddIn : Finishable
    {
        private Office.CommandBar newToolBar;
        private Office.CommandBarButton indexMailboxes;
        private Office.CommandBarButton clusterContactsButton;
        private Office.CommandBarButton manageGroupButton;
        private Outlook.Explorers selectExplorers;

        private Office.CommandBar filterBar;
        private List<Office.CommandBarButton> filterButtons;
        private Dictionary<String, SocialNetwork> filterNetwork;

        private PrepareClusterData pcd = null;
        private SocialNetworkManager networkManager = null;
        private Outlook.Folders rootFolders;
        private SocialNetwork currentSocialNetwork;
        private FolderTree folderTree;


        public FolderTree FolderTree
        {
            get
            {
                if (folderTree == null)
                {
                    this.folderTree = new FolderTree(this.RootFolders);
                }
                return this.folderTree;
            }
        }

        public Outlook.Folders RootFolders
        {
            get { return rootFolders; }
            set { rootFolders = value; }
        }



        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            selectExplorers = this.Application.Explorers;
            selectExplorers.NewExplorer += new Outlook
                .ExplorersEvents_NewExplorerEventHandler(newExplorer_Event);
            this.rootFolders = this.Application.ActiveExplorer().Session.Folders;
            string myAddress = this.Application.ActiveExplorer().Session.CurrentUser.Address;
            Indexes.LocalAccountAddress = myAddress;
            this.AddToolbar();
            this.AddFilterBar();

        }
        private void newExplorer_Event(Outlook.Explorer new_Explorer)
        {
            ((Outlook._Explorer)new_Explorer).Activate();
            newToolBar = null;
            this.AddToolbar();
        }

        private void AddToolbar()
        {

            if (newToolBar == null)
            {
                Office.CommandBars cmdBars =
                    this.Application.ActiveExplorer().CommandBars;
                newToolBar = cmdBars.Add("Cluster Tools",
                    Office.MsoBarPosition.msoBarTop, false, true);
            }
            try
            {
                // the Index Mailboxes
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

                // the Cluster Contacts button
                Office.CommandBarButton clusterButton = (Office
                                    .CommandBarButton)newToolBar.Controls.Add
                                    (1, missing, missing, missing, missing);
                clusterButton.Style = Office
                    .MsoButtonStyle.msoButtonCaption;
                clusterButton.Caption = "Cluster Contacts";
                clusterButton.Tag = "cluster";
                if (this.clusterContactsButton == null)
                {
                    this.clusterContactsButton = clusterButton;
                    clusterContactsButton.Click += new Office.
                        _CommandBarButtonEvents_ClickEventHandler
                        (ButtonClick);
                }

                // the Manage Group button
                Office.CommandBarButton managerButton = (Office
                                    .CommandBarButton)newToolBar.Controls.Add
                                    (1, missing, missing, missing, missing);
                managerButton.Style = Office
                    .MsoButtonStyle.msoButtonCaption;
                managerButton.Caption = "Manage Group";
                managerButton.Tag = "manage";
                if (this.manageGroupButton == null)
                {
                    this.manageGroupButton = managerButton;
                    manageGroupButton.Click += new Office.
                        _CommandBarButtonEvents_ClickEventHandler
                        (ButtonClick);
                }

                newToolBar.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddFilterBar()
        {
            this.filterButtons = new List<Office.CommandBarButton>();
            this.filterNetwork = new Dictionary<String, SocialNetwork>();
            this.networkManager = new SocialNetworkManager(FolderTree);
            if (this.networkManager.Count == 0)
            {
                return;
            }
            else if (this.filterBar == null)
            {
                Office.CommandBars cmdBars =
                    this.Application.ActiveExplorer().CommandBars;
                this.filterBar = cmdBars.Add("Social Network Filters",
                    Office.MsoBarPosition.msoBarTop, false, true);
                this.filterNetwork = new Dictionary<String, SocialNetwork>();
                foreach (SocialNetwork sn in this.networkManager.SocialNetworks)
                {
                    Office.CommandBarButton filterButton = (Office.CommandBarButton)newToolBar.Controls.Add
                                    (1, missing, missing, missing, missing);
                    sn.RootFolders = this.RootFolders;
                    filterButton.Style = Office.MsoButtonStyle.msoButtonCaption;
                    filterButton.Caption = sn.Name;
                    filterButton.Tag = sn.Name;
                    filterButton.Click += new Office._CommandBarButtonEvents_ClickEventHandler(Filter);
                    this.filterButtons.Add(filterButton);
                    this.filterNetwork.Add(filterButton.Caption, sn);
                }
            }
        }

        /// <summary>
        /// Filter mail based on the social networks
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="cancel"></param>
        private void Filter(Office.CommandBarButton ctrl, ref bool cancel)
        {
            this.currentSocialNetwork = this.filterNetwork[ctrl.Caption];
            this.currentSocialNetwork.FilterFolder(this.Application.ActiveExplorer().CurrentFolder);
        }

        private void ButtonClick(Office.CommandBarButton ctrl,
                ref bool cancel)
        {
            try
            {
                Outlook.Explorer activeExplorer = this.Application.ActiveExplorer();
                Outlook.MAPIFolder inbox = activeExplorer.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
                Outlook.MAPIFolder sentBox = activeExplorer.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
                Outlook.MAPIFolder contacts = activeExplorer.Session.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts);

                if (ctrl.Tag == "mailboxes")
                {
                    if (this.folderTree == null)
                    {
                        this.folderTree = new FolderTree(this.rootFolders);
                    }
                    new IndexMailboxes(this.folderTree, Indexes.Instance, activeExplorer);
                }
                else if (ctrl.Tag.Equals("cluster"))
                {
                    this.pcd = new PrepareClusterData(this);
                }
                else if (ctrl.Tag.Equals("manage"))
                {
                    this.networkManager.ManageNetworks();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Finish()
        {
            this.networkManager = new SocialNetworkManager(pcd.Networks, FolderTree);
            this.pcd = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Cancel()
        {
        }


        /// <summary>
        /// Refresh the list of social networks
        /// </summary>
        private void RefreshNetworks()
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}

using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace OutlookAddin1
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
                Office.CommandBars cmdBars =
                    this.ActiveExplorer().CommandBars;
                newToolBar = cmdBars.Add("Email Groups",
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
                button_1.Tag = "inbox";
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
                button_2.Caption = "Nokia N800 Project";
                button_2.Tag = "IST412";
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
                button_3.Caption = "Disney Project";
                button_3.Tag = "IST443";
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
            if (ctrl.Tag == "inbox")
            {
                
            }
            MessageBox.Show("You clicked: " + ctrl.Caption);
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

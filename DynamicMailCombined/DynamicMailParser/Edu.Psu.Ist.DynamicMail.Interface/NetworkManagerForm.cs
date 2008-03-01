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
    /// Window to manage list of all networks
    /// </summary>
	public partial class NetworkManagerForm: Form, Finishable
	{
        private Dictionary<int, SocialNetwork> networkList = new Dictionary<int, SocialNetwork>();
        private NetworkEditorForm editor;
        private SocialNetwork network;
        private SocialNetworkManager networkManager;
        
        /// <summary>
        /// Constructor to edit existing networks
        /// </summary>
        /// <param name="networks"></param>
		public NetworkManagerForm(SocialNetworkManager snmanager)
		{
			InitializeComponent();
            this.networkManager = snmanager;
            foreach (SocialNetwork sn in networkManager.SocialNetworks)
            {
                String [] rowData = {sn.Name, sn.Accounts.Count.ToString() };
                int pos = this.NetworkGrid.Rows.Add(rowData);
                this.networkList[pos] = sn;
            }
            this.Show();
            this.Focus();
		}

        /// <summary>
        /// If the done button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoneButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.networkManager.Save();
        }

        /// <summary>
        /// If the edit button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditButton_Click(object sender, EventArgs e)
        {
            if (NetworkGrid.SelectedCells.Count == 1)
            {
                int pos = NetworkGrid.SelectedCells[0].RowIndex;
                if (this.networkList.ContainsKey(pos))
                {
                    this.network = this.networkList[pos];
                    this.editor = new NetworkEditorForm(this.network, this.networkManager);
                }
                else
                {
                    MessageBox.Show("An error occurred - no identifiable Network selected. Try again.");
                }
            } 
            else 
            {
                MessageBox.Show("Please select one and only one Network.");
            }
        }

        public void Finish()
        {
            // this isn't a new network if we are here
            this.networkManager.AddNetwork(this.network);
        }

        public void Cancel()
        {

        }
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Threading;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    /// <summary>
    /// Form to manage a SocialNetwork
    /// </summary>
    public partial class NetworkEditorForm : Form, Finishable
    {
        private SocialNetwork network;
        private SocialNetworkManager manager;
        private NetworkManagerForm managerForm;

        /// <summary>
        /// Get the Name column
        /// </summary>
        public DataGridViewTextBoxColumn NameColumn 
        {
            get { return this.nameCol; }
        }

        /// <summary>
        /// Get the Email Address column
        /// </summary>
        public DataGridViewTextBoxColumn AddressColumn
        {
            get { return this.emailCol; }
        }

        /// <summary>
        /// for adding an account
        /// </summary>
        private EditAccountForm addAccount = null;

        /// <summary>
        /// for editing an account
        /// </summary>
        private EditAccountForm editAccountForm = null;
        private DataGridViewRow editRow = null;
        private Account editAccount = null;

        /// <summary>
        /// Get the DataGridView to pull all data - necessary right now, 
        /// but eventually we'll move to just getting the accounts.
        /// </summary>
        public DataGridView NetworkData
        {
            get { return this.accountGrid; }
        }

        /// <summary>
        /// Edit an existing network
        /// </summary>
        /// <param name="finish"></param>
        /// <param name="network"></param>
        public NetworkEditorForm(NetworkManagerForm managerForm, SocialNetwork network, SocialNetworkManager manager)
        {
            this.network = network;
            this.manager = manager;
            this.managerForm = managerForm;
            InitializeComponent();
            this.PopulateAccounts(network.Accounts);
            if (network.Name != null && !network.Name.Equals(""))
            {
                this.nameBox.Text = network.Name;
            }
            this.Show();
            this.Focus();
        }

        /// <summary>
        /// populate the accounts from the network
        /// </summary>
        /// <param name="accounts"></param>
        private void PopulateAccounts(List<Account> accounts)
        {
            foreach(Account acct in this.network.Accounts)
            {
                String[] row = { acct.Name, acct.Address };
                this.accountGrid.Rows.Add(row);
            }
        }

        /// <summary>
        /// Add a new Address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addAddress_Click(object sender, EventArgs e)
        {
            this.addAccount = new EditAccountForm(this);
        }

        /// <summary>
        /// Save the SocialNetwork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveGroup_Click(object sender, EventArgs e)
        {
            string networkName = this.nameBox.Text;
            // have to have a name
            if (networkName == null || networkName.Equals(""))
            {
                MessageBox.Show("You must set the Network Name");
                return;
            }

            try
            {
                this.network.Name = networkName;
                if (this.network.IsNew)
                {
                    this.manager.AddNetwork(this.network);
                    this.manager.Save();
                }
                
                // this is a hack; it would be better to use the NetworkManagerForm
                // immediately after clustering, but...
                if (this.managerForm != null)
                {
                    this.managerForm.Refresh();
                }

                this.Close();
                this.manager.Finish();
            }
            catch (SocialNetworkException sne)
            {
                // the only exception that should come here is a
                // SocialNetworkException
                MessageBox.Show(sne.Message);
                return;
            }
        }

        /// <summary>
        /// Cancel adding a new person to list
        /// </summary>
        public void Cancel()
        {
            // don't do anything
        }

        /// <summary>
        /// Finish the AccountAdd by adding the new Account object
        /// </summary>
        public void Finish()
        {
            if (this.addAccount != null)
            {
                String name = this.addAccount.AccountName;
                String address = this.addAccount.AccountAddress;
                String [] arr = {name, address};
                this.accountGrid.Rows.Add(arr);
                this.network.Accounts.Add(new Account(name, address));
                this.addAccount = null;
                this.Refresh();
            }
            else if (this.editAccountForm != null)
            {
                String name = this.editAccountForm.AccountName;
                String address = this.editAccountForm.AccountAddress;
                // set the Account object variables
                this.editAccount.Name = name;
                this.editAccount.Address = address;

                // set row data
                this.editRow.Cells[0].Value = name;
                this.editRow.Cells[1].Value = address;
                
                // cleanup
                this.editAccountForm = null;
                this.editRow = null;
            }
            this.Refresh();
        }

        /// <summary>
        /// Close the window and call cancel on the caller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ignoreGroup_Click(object sender, EventArgs e)
        {
            this.Close();
            this.manager.Cancel();
        }

        /// <summary>
        /// if the remove button is selected, remove the first
        /// account in the remove list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeName_Click(object sender, EventArgs e)
        {
            this.SelectRowsFromCells();
            foreach (DataGridViewRow row in this.accountGrid.SelectedRows)
            {
                // remove from the list of accounts
                this.network.Accounts.RemoveAt(row.Index);
                // remove from rows
                accountGrid.Rows.RemoveAt(row.Index);
            }
            this.Refresh();
        }

        /// <summary>
        /// If the edit button is selected, allow the user 
        /// to edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, EventArgs e)
        {
            this.SelectRowsFromCells();
            foreach (DataGridViewRow row in this.accountGrid.SelectedRows)
            {
                int pos = row.Index;
                this.editAccount = this.network.Accounts[pos];
                this.editRow = row;
                this.editAccountForm = new EditAccountForm(this, this.editAccount.Name, this.editAccount.Address);
            }
        }


        /// <summary>
        /// If cells are selected instead of rows, select those rows
        /// </summary>
        private void SelectRowsFromCells()
        {
            if (this.accountGrid.SelectedRows.Count == 0 && this.accountGrid.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in this.accountGrid.SelectedCells)
                {
                    this.accountGrid.Rows[cell.RowIndex].Selected = true;
                }
            }
        }
    }
}
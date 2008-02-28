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
    public partial class NetworkManager : Form, Finishable
    {
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

        private String networkName;

        /// <summary>
        /// The name of the social network
        /// </summary>
        public String NetworkName
        {
            get { return networkName; }
            private set { networkName = value; }
        }

        private Finishable finish;
        private List<Account> accounts = new List<Account>();
        private EditAccount addAccount = null;

        // for editing an account
        private EditAccount editAccount = null;
        private DataGridViewRow editRow = null;
        private Account editingAccount = null;

        /// <summary>&
        /// Get the accounts as an array of Account objects
        /// </summary>
        public Account[] Accounts
        {
            get { return (Account[])this.accounts.ToArray(); }
        }

        /// <summary>
        /// Get the DataGridView to pull all data - necessary right now, 
        /// but eventually we'll move to just getting the accounts.
        /// </summary>
        public DataGridView NetworkData
        {
            get { return this.groupList; }
        }

        /// <summary>
        /// Public constructor, needs to be created by a Finishable object
        /// </summary>
        /// <param name="finish"></param>
        /// <param name="accounts"></param>
        public NetworkManager(Finishable finish, Account [] accounts)
        {
            this.finish = finish;
            
            // if we get an empty, move to next
            if (accounts.Length == 0)
            {
                this.finish.Cancel();
                return;
            }
            InitializeComponent();
            for (int i = 0; i < accounts.Length; i++)
            {
                Account account = accounts[i];
                String[] row = { account.Name, account.Address };
                this.groupList.Rows.Add(row);
                this.accounts.Add(account);
            }
            this.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addAddress_Click(object sender, EventArgs e)
        {
            this.addAccount = new EditAccount(this);
        }

        /// <summary>
        /// Save the SocialNetwork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveGroup_Click(object sender, EventArgs e)
        {
            // have to have a name
            NetworkName = this.nameBox.Text;
            if (NetworkName == null || NetworkName.Equals(""))
            {
                MessageBox.Show("You must set the Network Name");
                return;
            }
            try
            {
                this.finish.Finish();
            }
            catch (Exception sne)
            {
                // the only exception that should come here is a
                // SocialNetworkException
                MessageBox.Show(sne.Message);
                return;
            }
            this.Close();
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
                this.groupList.Rows.Add(arr);
                this.accounts.Add(new Account(name, address));
                this.addAccount = null;
                this.Refresh();
            }
            else if (this.editAccount != null)
            {
                String name = this.editAccount.AccountName;
                String address = this.editAccount.AccountAddress;
                // set the Account object variables
                this.editingAccount.Name = name;
                this.editingAccount.Address = address;

                // set row data
                this.editRow.Cells[0].Value = name;
                this.editRow.Cells[1].Value = address;
                
                // cleanup
                this.editingAccount = null;
                this.editAccount = null;
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
            this.finish.Cancel();
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
            foreach (DataGridViewRow row in this.groupList.SelectedRows)
            {
                // remove from the list of accounts
                Account acct = this.GetAccountFromRow(row);
                this.accounts.Remove(acct);
                // remove from rows
                groupList.Rows.RemoveAt(row.Index);
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
            foreach (DataGridViewRow row in this.groupList.SelectedRows)
            {
                Account acct = this.GetAccountFromRow(row);
                this.editingAccount = this.accounts[this.accounts.IndexOf(acct)];
                this.editRow = row;
                this.editAccount = new EditAccount(this, this.editingAccount.Name, this.editingAccount.Address);
            }
        }

        /// <summary>
        /// Given a row, returns an account object for that row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Account GetAccountFromRow(DataGridViewRow row)
        {
            String name = "";
            String address = "";
            if (row.Cells[0].Value != null)
                name = row.Cells[0].Value.ToString();
            if (row.Cells[1].Value != null)
                address = row.Cells[1].Value.ToString();
            return new Account(name, address);
        }

        /// <summary>
        /// If cells are selected instead of rows, select those rows
        /// </summary>
        private void SelectRowsFromCells()
        {
            if (this.groupList.SelectedRows.Count == 0 && this.groupList.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in this.groupList.SelectedCells)
                {
                    this.groupList.Rows[cell.RowIndex].Selected = true;
                }
            }
        }
    }
}
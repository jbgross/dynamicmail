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
using Edu.Psu.Ist.DynamicMail.Interface;

namespace Edu.Psu.Ist.DynamicMail
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

        private Finishable finish;
        private List<Account> accounts = new List<Account>();
        private AddAccount addAccount = null;

        /// <summary>
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
            InitializeComponent();
            foreach (Account account in accounts)
            {
                String[] row = { account.Name, account.Address };
                this.groupList.Rows.Add(row);
                this.accounts.Add(account);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addAddress_Click(object sender, EventArgs e)
        {
            AddAccount addWindow = new AddAccount(this);
            addWindow.Show();
        }


        private void saveGroup_Click(object sender, EventArgs e)
        {
            this.Close();
            this.finish.Finish();
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
                this.Refresh();
            }
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
            if (this.groupList.SelectedRows.Count > 0 &&
                this.groupList.SelectedRows[0].Index !=
                this.groupList.Rows.Count - 1)
            {
                String name = groupList.SelectedRows[0].Cells[0].ToString();
                if(name == null || name.Equals("")) 
                {
                    name = "foobar";
                }

                String address = groupList.SelectedRows[0].Cells[1].ToString();
                if(address == null || address.Equals(""))
                {
                    address = "barfoo";
                }

                foreach (Account acct in this.accounts)
                {
                    if (acct.Name.Equals(name) || acct.Name.Equals(address))
                    {
                        this.accounts.Remove(acct);
                        break;
                    }
                }

                groupList.Rows.RemoveAt(groupList.SelectedRows[0].Index);

            }
        }
    }
}
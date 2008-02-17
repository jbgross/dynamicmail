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
    /// Class to add an individual account to a network
    /// </summary>
    public partial class EditAccount : Form
    {
        Finishable finishable;
        
        private String accountName = "";

        /// <summary>
        /// The name for the Account
        /// </summary>
        public String AccountName
        {
            get 
            {
                if (this.accountName != null)
                {
                    this.accountName = this.NameBox.Text;
                }
                return this.accountName; 
            }
            private set 
            {
                this.accountName = value; 
                this.NameBox.Text = value; 
            }
        }

        private String accountAddress = "";

        /// <summary>
        /// The address for the account
        /// </summary>
        public String AccountAddress
        {
            get
            {
                if (this.accountAddress != null)
                {
                    this.accountAddress = this.AddressBox.Text;
                }
                return this.accountAddress;
            }
            private set
            {
                this.accountAddress = value;
                this.AddressBox.Text = value;
            }
        }

        /// <summary>
        /// Create a new instance and make visible
        /// </summary>
        /// <param name="finish"></param>
        public EditAccount(Finishable finish)
        {
            InitializeComponent();
            this.finishable = finish;
            this.Show();
        }

        /// <summary>
        /// Create a new instance and make visible
        /// </summary>
        /// <param name="finish"></param>
        public EditAccount(Finishable finish, String name, String address)
        {
            InitializeComponent();
            this.finishable = finish;
            AccountName = name;
            AccountAddress = address;
            this.Show();
        }

        /// <summary>
        /// Ignore this entire social network
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.finishable.Cancel();
        }

        /// <summary>
        /// Add a new Account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, EventArgs e)
        {
            AccountName = NameBox.Text;
            AccountAddress = AddressBox.Text;
            this.Close();
            this.finishable.Finish();
        }

    }
}
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
    public partial class AddAccount : Form
    {
        Finishable finishable;
        String accountName;

        /// <summary>
        /// The name for the Account
        /// </summary>
        public String AccountName
        {
            get { return accountName; }
            private set { accountName = value; }
        }

        String accountAddress;

        /// <summary>
        /// The address for the account
        /// </summary>
        public String AccountAddress
        {
            get { return accountAddress; }
            private set { accountAddress = value; }
        }

        /// <summary>
        /// Create a new instance and make visible
        /// </summary>
        /// <param name="finish"></param>
        public AddAccount(Finishable finish)
        {
            this.finishable = finish;
            InitializeComponent();
            Visible = true;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.finishable.Cancel();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            AccountName = NameBox.Text;
            AccountAddress = AddressBox.Text;
            Visible = false;
            this.finishable.Finish();
        }

    }
}
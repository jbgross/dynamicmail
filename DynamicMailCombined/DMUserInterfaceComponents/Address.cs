using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail.Interface
{
    /// <summary>
    /// A container to hold an address and a name
    /// </summary>
    public class Account
    {
        private String address;

        /// <summary>
        /// The address for the account
        /// </summary>
        public String Address
        {
            get { return address; }
            private set { address = value; }
        }
        private String name;

        /// <summary>
        /// The name for the account
        /// </summary>
        public String Name
        {
            get { return name; }
            private set { name = value; }
        }

        /// <summary>
        /// Constructor, takes two arguments - either can be null
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        public Account(String name, String address)
        {
            Address = address;
            Name = name;
        }

    }
}

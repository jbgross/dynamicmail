using System;
using System.Collections.Generic;
using System.Text;

namespace Edu.Psu.Ist.DynamicMail
{
    /// <summary>
    /// Exception for SocialNetwork classes
    /// </summary>
	public class SocialNetworkException : Exception
	{
        /// <summary>
        /// Empty constructor
        /// </summary>
        public SocialNetworkException()
        {
        }

        /// <summary>
        /// Constructor that takes a message
        /// </summary>
        /// <param name="msg"></param>
        public SocialNetworkException(String msg)
            : base(msg)
        {
        }


	}
}

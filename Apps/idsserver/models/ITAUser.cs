using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace idsserver.models
{
    
    public class ITAUser
    {
    
        /// <summary>
        /// Gets or sets the firstname
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets the fullname
        /// </summary>
        public string Fullname
        {
            get
            {
                return string.Format("{0} {1}", this.Firstname, this.Lastname);
            }
        }

        /// <summary>
        /// Gets or sets the lastname
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }

    
    }
}

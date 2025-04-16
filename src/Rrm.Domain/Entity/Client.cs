using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rrm.Domain.Entity
{
    /// <summary>
    /// Represents a client in the system.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the unique identifier for the client.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the email address of the client.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Gets or sets the phone number of the client.
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}

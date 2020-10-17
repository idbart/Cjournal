using System;
using System.Collections.Generic;
using System.Text;

namespace Cjournal_Desktop.Models
{
    // corresponds to an entry in the "users" table 
    public class UserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime birthday { get; set; }
    }
}

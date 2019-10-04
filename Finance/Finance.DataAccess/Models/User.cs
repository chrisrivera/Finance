using System;
using System.Collections.Generic;

namespace Finance.DataAccess.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
    }
}

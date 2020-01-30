using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WatchAndEnjoy.Models
{
    public class Admins
    {
        public string Name { get; set; }
        public EmailService Email { get; set; }
        public string Role { get; set; }
    }
}
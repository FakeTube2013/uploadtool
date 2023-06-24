using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YT2013.Backend.Schemas {
    public class LoginSchema {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool FromApp { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YT2013.Backend.Schemas.AccountData {
    public class Root {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string ProfilePictureURI { get; set; }
        public string Email { get; set; }
        public List<object> Videos { get; set; }
    }
}
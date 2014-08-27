using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicPoint.Controllers
{
    public class UserLoggedIn
    {
        public UserLoggedIn(bool logged, string user)
        {
            username = user;
            isLoggedIn = logged;
        }

        public string username;
        public bool isLoggedIn;
    }
}

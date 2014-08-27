using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace PicPoint.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public bool Get(string username, string password)
        {
            var obj = DBEntities.Proxy.Users.Where(x => x.username == username);
            if (obj.Count() > 0 && obj.First().password == password)
            {
                HttpCookie myCookie = new HttpCookie("CurrentUser");
                myCookie["Username"] = username;
                myCookie["Password"] = password;
                myCookie.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(myCookie);
                return true;
            }
            return false;
        }

    }
}

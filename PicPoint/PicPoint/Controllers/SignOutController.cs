using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace PicPoint.Controllers
{
    public class SignOutController : Controller
    {
        //
        // GET: /SignOut/

        public void Get()
        {
            if (Request.Cookies["CurrentUser"] != null)
            {
                HttpCookie myCookie = new HttpCookie("CurrentUser");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
        }

    }
}

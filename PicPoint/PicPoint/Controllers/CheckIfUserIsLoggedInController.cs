using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicPoint.Models;
using System.Web.Helpers;

namespace PicPoint.Controllers
{
    public class CheckIfUserIsLoggedInController : Controller
    {
        //
        // GET: /CheckIfUserIsLoggedInfdsfsd/

        public ActionResult Get()
        {
            HttpCookie cookie = Request.Cookies["CurrentUser"];
            if (cookie != null)
            {
                string user = cookie["Username"];
                List<Users> list = DBEntities.Proxy.Users.Where(x => x.username == user).ToList();
                if (list != null && list.Count == 1)
                {
                    if (list[0].password == cookie["Password"])
                        return Json(new UserLoggedIn(true, user), JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new UserLoggedIn(false, ""), JsonRequestBehavior.AllowGet);
        }

    }
}

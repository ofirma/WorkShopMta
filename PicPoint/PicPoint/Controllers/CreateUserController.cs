using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using PicPoint.Models;
using System.Net.Http;

namespace PicPoint.Controllers
{
    public class CreateUserController : Controller
    {
        //
        // GET: /CreateUser/

        public ActionResult Get(string username, string password, string email)
        {
            
            var obj = DBEntities.Proxy.Users.Where(x => x.username == username);
            if (obj.Count() == 0)
            {
                Users newUser = Users.CreateUsers(username, password);
                newUser.email = email;
                DBEntities.Proxy.Users.AddObject(newUser);
                DBEntities.Proxy.SaveChanges();
                HttpCookie myCookie = new HttpCookie("CurrentUser");
                myCookie["Username"] = username;
                myCookie["Password"] = password;
                myCookie.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(myCookie);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}

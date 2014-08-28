using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using PicPoint.Models;

namespace PicPoint.Controllers
{
    public class OfirController : Controller
    {

        public ActionResult Get()
        {
            int obj = 0;
            try
            {
                obj = DBEntities.Proxy.Users.ToList().Count;
            }
            catch (Exception ex)
            {
                
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
            
            return Json("Work", JsonRequestBehavior.AllowGet);
        }

    }
}

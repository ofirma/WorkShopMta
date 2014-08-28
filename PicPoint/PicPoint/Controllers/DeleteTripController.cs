using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using PicPoint.Models;
using System.Net;

namespace PicPoint.Controllers
{
    public class DeleteVideoController : Controller
    {
        public ActionResult DeleteVideo(string id)
        {
            List<Trips> list = DBEntities.Proxy.Trips.Where(x => x.trip_id == id).ToList();
           
            if (list != null && list.Count == 1)
            {
                DBEntities.Proxy.Trips.DeleteObject(list[0]);
                DBEntities.Proxy.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}

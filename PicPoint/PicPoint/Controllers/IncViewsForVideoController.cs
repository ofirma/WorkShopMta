using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicPoint.Models;

namespace PicPoint.Controllers
{
    public class IncViewsForVideoController : Controller
    {
        //
        // GET: /IncViewsForVideo/

        public ActionResult IncViews(string videoId)
        {
            List<Trips> list = DBEntities.Proxy.Trips.Where(x => x.trip_id == videoId).ToList();
            if (list != null && list.Count == 1)
            {
                list[0].views++;
                DBEntities.Proxy.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}

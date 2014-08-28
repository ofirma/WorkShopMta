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
    public class GetVideosOfUserController : Controller
    {
        public ActionResult GetVideosOfUser()
        {
            string username = Request.Cookies["CurrentUser"]["Username"];
            List<Trips> list = DBEntities.Proxy.Trips.Where(x => x.username == username).ToList();
            List<VideoMetadata> videos = new List<VideoMetadata>();

            foreach (var obj in list)
            {
                VideoMetadata video = new VideoMetadata(obj);
                videos.Add(video);
            }

            return Json(videos, JsonRequestBehavior.AllowGet);
        }

    }
}

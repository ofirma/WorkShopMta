using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using PicPoint.Models;
using System.Net;
using System.Configuration;
using System.Web.Http;

namespace PicPoint.Controllers
{
    public class GetVideosByFilterController : Controller
    {
        public ActionResult GetVideosOfUser(string filter)
        {
            List<Trips> list = DBEntities.Proxy.Trips.Where(x => x.trip_name.Contains(filter)).ToList();
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

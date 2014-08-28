using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicPoint.Models;

namespace PicPoint.Controllers
{
    public class UpdateMusicForVideoController : Controller
    {
        //
        // GET: /UpdateMusicForVideo/

        public bool Get(string videoId, int soundId)
        {
            List<Trips> list = DBEntities.Proxy.Trips.Where(x => x.trip_id == videoId).ToList();
            if (list != null && list.Count == 1 && list[0] != null)
            {
                list[0].sound_id = soundId;
                DBEntities.Proxy.SaveChanges();
                return true;
            }
            return false;
        }

    }
}

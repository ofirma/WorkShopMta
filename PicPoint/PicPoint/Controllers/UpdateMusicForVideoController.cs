using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicPoint.Models;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace PicPoint.Controllers
{
    public class UpdateMusicForVideoController : ApiController
    {
        //
        // GET: /UpdateMusicForVideo/

        [System.Web.Mvc.HttpPost]
        public bool Post(object json)
        {
            JObject obj = json as JObject;
            string tripId = obj["id"].ToString();
            int soundId = int.Parse(obj["soundId"].ToString());
            List<Trips> list = DBEntities.Proxy.Trips.Where(x => x.trip_id == tripId).ToList();
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

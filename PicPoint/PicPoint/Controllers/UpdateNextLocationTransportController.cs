using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicPoint.Models;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace PicPoint.Controllers
{
    public class UpdateNextLocationTransportController : ApiController
    {
        //
        // GET: /UpdateNextLocationTransport/

        [System.Web.Mvc.HttpPost]
        public bool Post(object json)
        {
            JObject obj = json as JObject;
            string id = obj["locationId"].ToString();
            string transportType = obj["transportType"].ToString();
            List<Locations> list = DBEntities.Proxy.Locations.Where(x => x.location_id == id).ToList();
            if (list != null && list.Count == 1 && list[0] != null)
            {
                list[0].travelModeToNextLocation = transportType;
                DBEntities.Proxy.SaveChanges();
                return true;
            }
            return false;
        }

    }
}

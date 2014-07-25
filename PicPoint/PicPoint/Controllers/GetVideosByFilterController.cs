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
    public class GetVideosByFilterController : ApiController
    {
        public HttpResponseMessage GetVideosOfUser(string filter)
        {
            List<Trips> trips = new List<Trips>();

            foreach (var obj in DBEntities.Proxy.Trips)
            {
                if (obj.trip_name.Contains(filter))
                {
                    trips.Add(obj);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, trips, Configuration.Formatters.JsonFormatter);
        }

    }
}

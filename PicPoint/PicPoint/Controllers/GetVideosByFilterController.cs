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
            Database1Entities1 de = new Database1Entities1();
            List<Trips> trips = new List<Trips>();

            foreach (var obj in de.Trips)
            {
                if (obj.trip_id.Contains(filter))
                {
                    trips.Add(obj);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, trips, Configuration.Formatters.JsonFormatter);
        }

    }
}

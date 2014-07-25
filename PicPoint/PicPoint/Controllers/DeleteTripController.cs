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
    public class DeleteTripController : ApiController
    {
        public HttpResponseMessage DeletePhoto(string id)
        {
            Trips trip = null;
            foreach (var obj in DBEntities.Proxy.Trips)
            {
                if (obj.trip_id == id)
                {
                    trip = obj;
                }
            }

            if (trip != null)
            {
                DBEntities.Proxy.Trips.DeleteObject(trip);
            }
            return Request.CreateResponse(HttpStatusCode.OK, Configuration.Formatters.JsonFormatter);
        }

    }
}

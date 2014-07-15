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
    public class GetVideosOfUserController : ApiController
    {
        public HttpResponseMessage GetVideosOfUser(string user)
    {
        Database1Entities1 de = new Database1Entities1();
        List<Trips> trips = new List<Trips>();

        foreach (var obj in de.Trips)
        {
            if (obj.username == user)
            {
                trips.Add(obj);
            }
        }

        return Request.CreateResponse(HttpStatusCode.OK, trips, Configuration.Formatters.JsonFormatter);
    }

    }
}

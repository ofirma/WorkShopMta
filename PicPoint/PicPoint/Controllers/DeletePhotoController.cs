using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using PicPoint.Models;

namespace PicPoint.Controllers
{
    public class DeletePhotoController : ApiController
    {
        

        public HttpResponseMessage DeletePhoto(string id)
        {
            Photos photo = null;
            foreach (var obj in DBEntities.Proxy.Photos)
            {
                if (obj.photo_id == id)
                {
                    photo = obj; 
                }
            }

            if (photo != null)
            {
                DBEntities.Proxy.Photos.DeleteObject(photo);
            }
            return Request.CreateResponse(HttpStatusCode.OK, Configuration.Formatters.JsonFormatter);
        }

    }
}

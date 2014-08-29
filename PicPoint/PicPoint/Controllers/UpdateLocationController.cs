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
    public class UpdateLocationController : ApiController
    {
        //
        // GET: /UpdateLocation/

        [System.Web.Mvc.HttpPost]
        public bool Post(LocationWrapper json)
        {

            List<Locations> list = DBEntities.Proxy.Locations.Where(x => x.location_id == json.id).ToList();
            if (list != null && list.Count == 1 && list[0] != null)
            {
                list[0].name = json.name;
                list[0].showStreetView = json.showStreetView ? 1 : 0;
                foreach (PhotoWrapper currObj in json.pics)
                {
                    List<Photos> photos = DBEntities.Proxy.Photos.Where(x => x.photo_id == currObj.id).ToList();
                    if (photos != null && photos.Count == 1 && photos[0] != null)
                    {
                        photos[0].caption = currObj.caption;
                    }
                }
                DBEntities.Proxy.SaveChanges();
                return true;
            }
            return false;
        }

    }
}

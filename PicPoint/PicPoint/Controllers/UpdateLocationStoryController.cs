using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicPoint.Models;
using System.Web.Http;

namespace PicPoint.Controllers
{
    public class UpdateLocationStoryController : ApiController
    {
        //
        // GET: /UpdateLocation/

        [System.Web.Mvc.HttpPost]
        public bool Post(LocationWrapper json)
        {
            List<Locations> list = DBEntities.Proxy.Locations.Where(x => x.location_id == json.id).ToList();
            if (list != null && list.Count == 1 && list[0] != null)
            {
                list[0].story = json.story;
                DBEntities.Proxy.SaveChanges();
                return true;
            }
            return false;
        }

    }
}

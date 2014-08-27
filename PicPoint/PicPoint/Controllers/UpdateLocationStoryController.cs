using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicPoint.Models;

namespace PicPoint.Controllers
{
    public class UpdateLocationStoryController : Controller
    {
        //
        // GET: /UpdateLocation/

        public bool Get(string locId, string story)
        {
            List<Locations> list = DBEntities.Proxy.Locations.Where(x => x.location_id == locId).ToList();
            if (list != null && list.Count == 1 && list[0] != null)
            {
                list[0].story = story;
                DBEntities.Proxy.SaveChanges();
                return true;
            }
            return false;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Web.Http;
using PicPoint.Models;

namespace PicPoint.Controllers
{

    public class LocationWrapper
    {
        public Locations locationData;
        public List<Photos> pics;
    }
    public class DayWrapper
    {
        public Days dayData;
        public List<LocationWrapper> locations;
    }

    public class LocationsController : ApiController
    {
        //
        // GET: /Locations/

        public List<DayWrapper> GetDays()
        {
            List<DayWrapper> days = new List<DayWrapper>();

            foreach (var obj in DBEntities.Proxy.Days)
            {
                List<Locations> loc = GetLocations(obj.day_id);
                List<LocationWrapper> locsWrapper = new List<LocationWrapper>();
                foreach (Locations locObj in loc)
                {
                    LocationWrapper locWrapper = new LocationWrapper();
                    locWrapper.locationData = locObj;
                    locWrapper.pics = GetPhotos(locObj.location_id);
                    locsWrapper.Add(locWrapper);
                }

                DayWrapper dayWrapper = new DayWrapper();
                dayWrapper.dayData = obj;
                dayWrapper.locations = locsWrapper;
                days.Add(dayWrapper);
            }
                return days;
        }

        private List<Locations> GetLocations(string dayId)
        {

            var locs = DBEntities.Proxy.Locations.Where(x => x.day_id == dayId);
            return locs.ToList();
        }

        private List<Photos> GetPhotos(string locId)
        {

            var locs = DBEntities.Proxy.Photos.Where(x => x.location_id == locId);
            return locs.ToList();
        }

    }
}

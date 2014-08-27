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

    //public class LocationWrapper
    //{
    //    public string id;
    //    public double latitude;
    //    public double longitude;
    //    public string travelModeToNextLocation;
    //    public List<Photos> pics;
    //}
    //public class DayWrapper
    //{
    //    public int day;
    //    public string daySummary;
    //    public List<LocationWrapper> locations;
    //}

    //public class PhotoWrapper
    //{
    //    public string caption;
    //    public string name;
    //}

    public class VideoController : ApiController
    {
        //
        // GET: /Locations/

        public List<DayWrapper> GetDays(string id)
        {
            TripWrapper trip = new TripWrapper(id);
                return trip.Days;
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

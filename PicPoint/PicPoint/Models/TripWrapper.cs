using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicPoint.Models
{
    public class LocationWrapper
        {
            public LocationWrapper(Locations locData)
            {
                id = locData.day_id;
                latitude = locData.latitude;
                longitude = locData.longitude;
                travelModeToNextLocation = locData.travelModeToNextLocation;
                pics = new List<PhotoWrapper>();
                foreach (Photos currPic in GetPhotos(locData.location_id))
                {
                    pics.Add(new PhotoWrapper(currPic));
                }
            }

            private List<Photos> GetPhotos(string locId)
            {
                var locs = DBEntities.Proxy.Photos.Where(x => x.location_id == locId);
                return locs.ToList();
            }
            public string id;
            public double? latitude;
            public double? longitude;
            public string travelModeToNextLocation;
            public List<PhotoWrapper> pics;
        }
    public class DayWrapper
        {
            public DayWrapper(Days dayData)
            {
                day = dayData.day;
                daySummary = dayData.daySummary;
                locations = new List<LocationWrapper>();
                foreach (Locations currLoc in GetLocations(dayData.day_id))
	            {
                    locations.Add(new LocationWrapper(currLoc));
	            }
            }

            private List<Locations> GetLocations(string dayId)
            {

                var locs = DBEntities.Proxy.Locations.Where(x => x.day_id == dayId);
                return locs.ToList();
            }

            public int day;
            public string daySummary;
            public List<LocationWrapper> locations;
        }
    public class PhotoWrapper
        {
            public string caption;
            public string name;

            public PhotoWrapper(Photos currPic)
            {
                caption = currPic.caption;
                name = currPic.name;
            }
        }
    public class TripWrapper
    {

        public TripWrapper(string tripId)
        {
            List<Days> days = DBEntities.Proxy.Days.Where(x => x.trip_id == tripId).ToList();
            Days = new List<DayWrapper>();
            foreach (Days currDay in days)
            {
                Days.Add(new DayWrapper(currDay));
            }
        }

        public List<DayWrapper> Days;
    }
}
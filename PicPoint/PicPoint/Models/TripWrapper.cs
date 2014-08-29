using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PicPoint.Models
{
    public class LocationWrapper
        {
        public LocationWrapper()
        {
        }
            public LocationWrapper(Locations locData)
            {
                id = locData.location_id;
                latitude = locData.latitude;
                longitude = locData.longitude;
                travelModeToNextLocation = locData.travelModeToNextLocation;
                name = locData.name;
                story = locData.story;
                showStreetView = locData.showStreetView == 1;
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
            public string name;
            public string story;
            public bool showStreetView;
        }
    public class DayWrapper
        {
            public DayWrapper(Days dayData)
            {
                picsNum = 0;
                string date = string.Format("{0}.{1}.{2}", dayData.date.Value.Day.ToString(), dayData.date.Value.Month.ToString(), dayData.date.Value.Year.ToString());
                daySummary = date + " - Tel Aviv <br/>";
                day = date;

                locations = new List<LocationWrapper>();
                foreach (Locations currLoc in GetLocations(dayData.day_id))
	            {
                    daySummary += "<br/>" + currLoc.name;
                    LocationWrapper loc = new LocationWrapper(currLoc);
                    locations.Add(loc);
                    picsNum += loc.pics.Count;
	            }
            }

            private List<Locations> GetLocations(string dayId)
            {

                var locs = DBEntities.Proxy.Locations.Where(x => x.day_id == dayId);
                return locs.ToList();
            }

            public string day;
            public string daySummary;
            public int picsNum;
            public List<LocationWrapper> locations;
        }
    public class PhotoWrapper
        {
            public string caption;
            public string name;
            public string id;

            public PhotoWrapper()
            {

            }

            public PhotoWrapper(Photos currPic)
            {
                    caption = currPic.caption;
                    name = currPic.name;
                    id = currPic.photo_id;
            }
        }
    public class TripWrapper
    {

        public TripWrapper(string tripId)
        {
            List<Days> days = DBEntities.Proxy.Days.Where(x => x.trip_id == tripId).ToList();
            allVideo = new VideoWrapper(tripId);
            allVideo.days = new List<DayWrapper>();
            foreach (Days currDay in days)
            {
                allVideo.days.Add(new DayWrapper(currDay));
            }
        }

        public VideoWrapper allVideo;

    }

    public class VideoWrapper
    {
        public VideoWrapper(string tripId)
        {
            List<Trips> trips = DBEntities.Proxy.Trips.Where(x => x.trip_id == tripId).ToList();
            musicOptions = new List<Sound>();
            musicOptions.Add(new Sound(){id = 0, name = "Sunny Beach", path= "resources/sounds/1.mp3"});
            musicOptions.Add(new Sound(){id = 1, name = "Happiness", path= "resources/sounds/happiness.mp3"});
            musicOptions.Add(new Sound(){id = 2, name = "Acoustic Air", path= "resources/sounds/acoustic.mp3"});
            musicOptions.Add(new Sound(){id = 3, name = "Funky", path= "resources/sounds/funky.mp3"});
            musicOptions.Add(new Sound(){id = 4, name = "Clear Day", path= "resources/sounds/clearDay.mp3"});
            Sound noneSound = new Sound() { id = -1, name = "None", path = "" };
            musicOptions.Add(noneSound);
            if (trips != null && trips.Count == 1)
            {
                List<Sound> sounds = musicOptions.Where(x => x.id == trips[0].sound_id).ToList();
                backgroundMusic = sounds[0];
            }
            musicOptions.Remove(noneSound);
            
        }
        public List<DayWrapper> days;
        public Sound backgroundMusic;
        public List<Sound> musicOptions;
    }

    public class Sound
    {
        public int id;
        public string name;
        public string path;
    }
}
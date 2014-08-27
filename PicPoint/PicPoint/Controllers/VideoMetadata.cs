using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PicPoint.Models;

namespace PicPoint.Controllers
{
    public class VideoMetadata
    {
        public VideoMetadata(Trips trip)
        {
            id = trip.trip_id;
            img = GetPicFromTrip(trip);
            name = trip.trip_name;
            uploader = trip.username;
            views = trip.views.Value;
        }

        private string GetPicFromTrip(Trips trip)
        {
            List<Photos> list = DBEntities.Proxy.Photos.Where(x => x.trip_id == trip.trip_id).ToList();
            if (list != null && list.Count > 0)
            {
                return list[0].name;
            }
            return null;

        }

        public string id;
        public string img;
        public string name;
        public string uploader;
        public int views;
    }
}

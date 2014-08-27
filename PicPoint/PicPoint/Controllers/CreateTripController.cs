using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using PicPoint.Models;
using System.Device.Location;

namespace PicPoint.Controllers
{
    public class CreateTripController : ApiController
    {
        public const double DEFAULT_DISTANCE = 100;
        public class PhotosComparer : IEqualityComparer<Photos>
        {
            public bool Equals(Photos x, Photos y)
            {
                var p1 = new GeoCoordinate(x.longitude3.Value, x.longitude3.Value);
                var p2 = new GeoCoordinate(y.longitude3.Value, y.longitude3.Value);
                if (p1.GetDistanceTo(p2) < DEFAULT_DISTANCE)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(Photos obj)
            {
                return obj.GetHashCode();
            }
        }


        public class Date
        {
            public Date(int day, int month, int year, DateTime date)
            {
                Day = day;
                Month = month;
                Year = year;
                Datetime = date;
            }

            public override bool Equals(object obj)
            {
                Date date = obj as Date;
                if (date.Day == Day && date.Month == Month && date.Year == Year)
                    return true;
                return false;
            }
            public int Day;
            public int Year;
            public int Month;
            public DateTime Datetime;
        }

        public void Get(string id)
        {
            List<Photos> photos = DBEntities.Proxy.Photos.Where(x => x.username == id).ToList();
            string newTripId = Guid.NewGuid().ToString();
            Trips trip = Trips.CreateTrips(newTripId);
            trip.username = id;
            DBEntities.Proxy.AddToTrips(trip);
            var sortedPhotos = photos.OrderBy(x => x.date).ToList();
            Date date = new Date(sortedPhotos[0].date.Value.Day,
                sortedPhotos[0].date.Value.Month,
                sortedPhotos[0].date.Value.Year,
                sortedPhotos[0].date.Value);
            int dayCount = 0;
            Days day = Days.CreateDays(++dayCount, Guid.NewGuid().ToString());
            day.trip_id = newTripId;
            day.date = date.Datetime;
            DBEntities.Proxy.AddToDays(day);
            foreach (Photos obj in sortedPhotos)
            {
                Date currDate = new Date(obj.date.Value.Day,
                    obj.date.Value.Month,
                    obj.date.Value.Year,
                    obj.date.Value);

                if (!currDate.Equals(date))
                {
                    day = Days.CreateDays(++dayCount, Guid.NewGuid().ToString());
                    day.trip_id = newTripId;
                    day.date = currDate.Datetime;
                    date = currDate;
                    DBEntities.Proxy.AddToDays(day);
                }
                obj.day_id = day.day_id;
                obj.trip_id = newTripId;
            }

            DBEntities.Proxy.SaveChanges();


            List<Days> days = DBEntities.Proxy.Days.Where(x => x.trip_id == newTripId).ToList();
            foreach (Days currDay in days)
            {
                List<Photos> photosOfDay = DBEntities.Proxy.Photos.Where(x => x.day_id == currDay.day_id).ToList();
                PhotosComparer pc = new PhotosComparer();
                var listOfPhotos = photosOfDay.GroupBy(x => x, pc).Select(grp => grp.ToList()).ToList();
                foreach (List<Photos> currList in listOfPhotos)
                {
                    Locations loc = Locations.CreateLocations(Guid.NewGuid().ToString());
                    loc.day_id = currList[0].day_id;
                    DBEntities.Proxy.AddToLocations(loc);
                }
            }

            trip.trip_name = GetRandomCity() + " - " + GetNumOfDays(trip) + " Days";



            DBEntities.Proxy.SaveChanges();
        }

        private string GetRandomCity()
        {
            Random ran = new Random();
            int value = ran.Next(2);
            if (value == 0)
            {
                return "Amsterdam";
            }
            else
            {
                return "London";
            }
        }

        private string GetNumOfDays(Trips trip)
        {
            List<Days> list = DBEntities.Proxy.Days.Where(x => x.trip_id == trip.trip_id).ToList();
            return list.Count.ToString();
        }



    }
}

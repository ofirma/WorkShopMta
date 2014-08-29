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
    public class CreateTripController : Controller
    {
        public const double DEFAULT_DISTANCE = 500000;
        public class PhotosComparer : IEqualityComparer<PicWrapper>
        {
            public bool Equals(PicWrapper x, PicWrapper y)
            {
                return x.id == y.id;
            }

            public int GetHashCode(PicWrapper obj)
            {
                return obj.id.GetHashCode();
            }
        }

        public class PicWrapper
        {
            public PicWrapper(Photos photo)
            {
                photoData = photo;
                id = Guid.NewGuid().ToString();
            }

            public Photos photoData;
            public string id;
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

        public ActionResult Get()
        {
            List<Photos> photos = DBEntities.Proxy.Photos.Where(x => x.trip_id == null).ToList();

            if (photos.Count == 0)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }


            string newTripId = Guid.NewGuid().ToString();
            Trips trip = Trips.CreateTrips(newTripId);
            string username = Request.Cookies["CurrentUser"]["Username"];
            trip.username = username;
            trip.sound_id = -1;
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
                List<PicWrapper> list = DividePhotos(photosOfDay);
                PhotosComparer pc = new PhotosComparer();
                var listOfPhotos = list.GroupBy(x => x, pc).Select(grp => grp.ToList()).ToList();
                foreach (List<PicWrapper> currList in listOfPhotos)
                {
                    Locations loc = Locations.CreateLocations(Guid.NewGuid().ToString());
                    loc.day_id = currList[0].photoData.day_id;
                    loc.name = GetRandomCity();
                    DBEntities.Proxy.AddToLocations(loc);
                    foreach (PicWrapper currObj in currList)
                    {
                        currObj.photoData.location_id = loc.location_id;
                    }
                }
            }

            trip.trip_name = GetRandomCity() + " - " + GetNumOfDays(trip) + " Days";



            DBEntities.Proxy.SaveChanges();
            return Json(newTripId, JsonRequestBehavior.AllowGet);
        }

        private List<PicWrapper> DividePhotos(List<Photos> photosOfDay)
        {
            List<PicWrapper> list = new List<PicWrapper>();
            foreach (var currObj in photosOfDay)
            {
                list.Add(new PicWrapper(currObj));
            }
            foreach (var x in list)
            {
                foreach (var y in list)
                {
                    Compare(x, y);
                }
            }
            return list;
        }

        private void Compare(PicWrapper x, PicWrapper y)
        {
            if (x.photoData.photo_id == y.photoData.photo_id)
            {
                return;
            }
            var p1 = new GeoCoordinate(x.photoData.longitude3.Value, x.photoData.longitude3.Value);
            var p2 = new GeoCoordinate(y.photoData.longitude3.Value, y.photoData.longitude3.Value);
            if (p1.GetDistanceTo(p2) < DEFAULT_DISTANCE)
            {
                string guid = null;
                if (x.id != y.id)
                {
                     y.id = x.id;
                }

            }

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

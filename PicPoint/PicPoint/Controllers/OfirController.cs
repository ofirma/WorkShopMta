using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using PicPoint.Models;

namespace PicPoint.Controllers
{
    public class OfirController : Controller
    {

        public ActionResult Get(string a)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = "FSDFSD";
            return json;
        }

    }
}

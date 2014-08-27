using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicPoint.Models;

namespace PicPoint.Controllers
{
    public class UpdatePictureCaptionController : Controller
    {
        //
        // GET: /UpdatePictureCaption/

        public bool Get(string picId, string caption)
        {
            List<Photos> list = DBEntities.Proxy.Photos.Where(x => x.photo_id == picId).ToList();
            if (list != null && list.Count == 1 && list[0] != null)
            {
                list[0].caption = caption;
                DBEntities.Proxy.SaveChanges();
                return true;
            }
            return false;
        }

    }
}

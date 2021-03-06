﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PicPoint.Models;

namespace PicPoint.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "val1ue1", "value2" };
        }

        // GET api/values/5
        public bool Get(string id)
        {
            var obj = DBEntities.Proxy.Users.Where(x => x.username == id);
            if (obj == null)
            {
                Users newUser = Users.CreateUsers(id, "");
                DBEntities.Proxy.Users.AddObject(newUser);
                DBEntities.Proxy.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
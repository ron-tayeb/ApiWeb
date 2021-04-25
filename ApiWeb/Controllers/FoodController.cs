using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiWeb.Models;

namespace ApiWeb.Controllers
{
    [RoutePrefix("api/food")]
    public class FoodController : ApiController
    {
        private static DBservices db = new DBservices();

        //Get all users
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(db.getUsers());
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

       
        [Route("{id}")] // id=password
        public IHttpActionResult GetU(string id)
        {
            try
            {
                //Users u = db.getUsers().FirstOrDefault(us => us.password == id);
                Users u = db.getUser(id);
                if (u == null)
                {
                    return Content(HttpStatusCode.NotFound, "User with id=" + id + " was not found");
                }
                return Ok(u);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }
        [HttpDelete]
        [Route("{pass}")]
        public IHttpActionResult Delete(string pass)
        {
            try
            {
                int res = db.DetleteUser(pass);
                return Ok("User deleted");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, $"User with password={pass} was not found");
            }
        }


        [HttpPost]
        public IHttpActionResult Post([FromBody] Users user)
        {
            try
            {
                return Created(new Uri(Request.RequestUri.AbsoluteUri + user.user_name), db.CreatNewUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{pass}")]
        public IHttpActionResult Put(string pass, [FromBody] Users user)
        {
            try
            {
                db.EditUser(pass, user);
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Bike_Store_Api.Models;

namespace Bike_Store_Api.ControllersAPI
{
    public class StaffsApiController : ApiController
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: api/StaffsApi
        public IQueryable<staff> Getstaffs()
        {
            return db.staffs;
        }

        // GET: api/StaffsApi/5
        [ResponseType(typeof(staff))]
        public IHttpActionResult Getstaff(int id)
        {
            staff staff = db.staffs.Find(id);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        // PUT: api/StaffsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstaff(int id, staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staff.staff_id)
            {
                return BadRequest();
            }

            db.Entry(staff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!staffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StaffsApi
        [ResponseType(typeof(staff))]
        public IHttpActionResult Poststaff(staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.staffs.Add(staff);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = staff.staff_id }, staff);
        }

        // DELETE: api/StaffsApi/5
        [ResponseType(typeof(staff))]
        public IHttpActionResult Deletestaff(int id)
        {
            staff staff = db.staffs.Find(id);
            if (staff == null)
            {
                return NotFound();
            }

            db.staffs.Remove(staff);
            db.SaveChanges();

            return Ok(staff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool staffExists(int id)
        {
            return db.staffs.Count(e => e.staff_id == id) > 0;
        }
    }
}
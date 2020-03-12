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
    public class StoresApiController : ApiController
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: api/StoresApi
        public IQueryable<store> Getstores()
        {
            return db.stores;
        }

        // GET: api/StoresApi/5
        [ResponseType(typeof(store))]
        public IHttpActionResult Getstore(int id)
        {
            store store = db.stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // PUT: api/StoresApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstore(int id, store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != store.store_id)
            {
                return BadRequest();
            }

            db.Entry(store).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!storeExists(id))
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

        // POST: api/StoresApi
        [ResponseType(typeof(store))]
        public IHttpActionResult Poststore(store store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.stores.Add(store);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = store.store_id }, store);
        }

        // DELETE: api/StoresApi/5
        [ResponseType(typeof(store))]
        public IHttpActionResult Deletestore(int id)
        {
            store store = db.stores.Find(id);
            if (store == null)
            {
                return NotFound();
            }

            db.stores.Remove(store);
            db.SaveChanges();

            return Ok(store);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool storeExists(int id)
        {
            return db.stores.Count(e => e.store_id == id) > 0;
        }
    }
}
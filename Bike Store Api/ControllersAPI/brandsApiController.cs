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
    public class brandsApiController : ApiController
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: api/brandsApi
        public IQueryable<brand> Getbrands()
        {
            return db.brands;
        }

        // GET: api/brandsApi/5
        [ResponseType(typeof(brand))]
        public IHttpActionResult Getbrand(int id)
        {
            brand brand = db.brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        // PUT: api/brandsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putbrand(int id, brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != brand.brand_id)
            {
                return BadRequest();
            }

            db.Entry(brand).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!brandExists(id))
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

        // POST: api/brandsApi
        [ResponseType(typeof(brand))]
        public IHttpActionResult Postbrand(brand brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.brands.Add(brand);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = brand.brand_id }, brand);
        }

        // DELETE: api/brandsApi/5
        [ResponseType(typeof(brand))]
        public IHttpActionResult Deletebrand(int id)
        {
            brand brand = db.brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            db.brands.Remove(brand);
            db.SaveChanges();

            return Ok(brand);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool brandExists(int id)
        {
            return db.brands.Count(e => e.brand_id == id) > 0;
        }
    }
}
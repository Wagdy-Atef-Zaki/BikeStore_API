using Bike_Store_Api.Models;
using Bike_Store_Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Bike_Store_Api.ControllersAPI
{
    public class ProductVMsApiController : ApiController
    {
        private BikeStoresEntities db = new BikeStoresEntities();
        // GET: api/ProductVMsApi
        public IQueryable<ProductVM> Get()
        {
            List<ProductVM> productVMs = new List<ProductVM>();
            var productobj = (from a in db.products
                              join b in db.brands
                              on a.brand_id equals b.brand_id
                              join c in db.categories
                              on a.category_id equals c.category_id
                              select new {a.product_id,a.product_name, a.model_year, a.list_price, b.brand_name, c.category_name }).ToList();

            foreach (var item in productobj)
            {
                ProductVM PvmObj = new ProductVM();
                PvmObj.product_id = item.product_id;
                PvmObj.product_name = item.product_name;
                PvmObj.model_year = item.model_year;
                PvmObj.list_price= item.list_price;
                PvmObj.category_name = item.category_name;
                PvmObj.brand_name = item.brand_name;
                productVMs.Add(PvmObj);
            }
            return productVMs.AsQueryable();
        }
        public IQueryable<ProductVM> Getproduct(int id)
        {
            List<ProductVM> productVMs = new List<ProductVM>();
            var productobj = (from a in db.products
                              join b in db.brands
                              on a.brand_id equals b.brand_id
                              join c in db.categories
                              on a.category_id equals c.category_id
                              where a.product_id == id
                              select new { a.product_id,a.product_name, a.model_year, a.list_price, b.brand_name, c.category_name }).ToList();

            foreach (var item in productobj)
            {
                ProductVM PvmObj = new ProductVM();
                PvmObj.product_id = item.product_id;
                PvmObj.product_name = item.product_name;
                PvmObj.model_year = item.model_year;
                PvmObj.list_price = item.list_price;
                PvmObj.category_name = item.category_name;
                PvmObj.brand_name = item.brand_name;
                productVMs.Add(PvmObj);
            }
            return productVMs.AsQueryable();
        }
       

        [ResponseType(typeof(product))]
        public IHttpActionResult Putproduct(int id, product product)//try to remove id and  [ResponseType(typeof(void))]
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.product_id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productVMExists(id))
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

        [ResponseType(typeof(product))]
        public IHttpActionResult Postproduct(product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.product_id }, product);
        }

        [ResponseType(typeof(product))]
        public IHttpActionResult Deleteproduct(int id)
        {
            var product = db.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        private bool productVMExists(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}

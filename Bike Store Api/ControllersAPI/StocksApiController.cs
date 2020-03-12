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
using Bike_Store_Api.ViewModels;

namespace Bike_Store_Api.ControllersAPI
{
    public class StocksApiController : ApiController
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: api/StocksApi
        public IQueryable<StockVM> Getstocks()
        {
            List<StockVM> stockVMs = new List<StockVM>();
            var StockObj = (from a in db.stocks
                           join b in db.stores
                           on a.store_id equals b.store_id
                           join c in db.products
                           on a.product_id equals c.product_id
                           select new {b.store_name,c.product_name,a.quantity }).ToList();
            foreach (var item in StockObj)
            {
                StockVM stockVMObj = new StockVM();
                stockVMObj.quantity= item.quantity;
                stockVMObj.store_name= item.store_name;
                stockVMObj.product_name= item.product_name;
                stockVMs.Add(stockVMObj);
            }
            return stockVMs.AsQueryable();
        }

        // GET: api/StocksApi/5
        [ResponseType(typeof(StockVM))]
        public IHttpActionResult Getstock(StockVM stockVMVM)
        {

            List<StockVM> stockVMs = new List<StockVM>();
            var StockObj = (from a in db.stocks
                            join b in db.stores
                            on a.store_id equals b.store_id
                            join c in db.products
                            on a.product_id equals c.product_id
                            where stockVMVM.product_id == a.product_id && stockVMVM.store_id==a.store_id
                            select new { b.store_name, c.product_name, a.quantity }).ToList();
            foreach (var item in StockObj)
            {
                StockVM stockVMObj = new StockVM();
                stockVMObj.quantity = item.quantity;
                stockVMObj.store_name = item.store_name;
                stockVMObj.product_name = item.product_name;
                stockVMs.Add(stockVMObj);
            }
            
            if (StockObj == null)
            {
                return NotFound();
            }

            return Ok(stockVMs);
        }

        // PUT: api/StocksApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstock(int id, StockVM stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.store_id)
            {
                return BadRequest();
            }

            db.Entry(stock).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!stockExists(id))
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

        // POST: api/StocksApi
        [ResponseType(typeof(stock))]
        public IHttpActionResult Poststock(stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.stocks.Add(stock);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (stockExists(stock.store_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = stock.store_id }, stock);
        }

        // DELETE: api/StocksApi/5
        [ResponseType(typeof(StockVM))]
        public IHttpActionResult Deletestock(int id)
        {
            stock stock = db.stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            db.stocks.Remove(stock);
            db.SaveChanges();

            return Ok(stock);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool stockExists(int id)
        {
            return db.stocks.Count(e => e.store_id == id) > 0;
        }
    }
}
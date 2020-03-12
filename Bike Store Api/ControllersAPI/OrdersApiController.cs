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
    public class OrdersApiController : ApiController
    {
        private BikeStoresEntities db = new BikeStoresEntities();

        // GET: api/OrdersApi
        public IQueryable<OrderVM> Getorders()
        {
            List<OrderVM> orderVMs = new List<OrderVM>();
            var OrderObj = (from a in db.orders
                           join b in db.customers
                           on a.customer_id equals b.customer_id
                           join c in db.staffs
                           on a.staff_id equals c.staff_id
                           join d in db.stores
                           on a.store_id equals d.store_id
                           select new {a.order_id,d.store_name,c.staff_first_name,b.first_name,a.shipped_date,a.required_date
                           ,a.order_date,a.order_status}).ToList();

            foreach (var item in OrderObj)
            {
                OrderVM orderVMObj = new OrderVM();
                orderVMObj.order_id = item.order_id;
                orderVMObj.order_status= item.order_status;
                orderVMObj.required_date= item.required_date;
                orderVMObj.shipped_date= item.shipped_date;
                orderVMObj.staff_first_name= item.staff_first_name;
                orderVMObj.store_name= item.store_name;
                orderVMObj.first_name= item.first_name;
                orderVMObj.order_date= item.order_date;
                orderVMs.Add(orderVMObj);
            }
            return orderVMs.AsQueryable();
        }

        // GET: api/OrdersApi/5
        [ResponseType(typeof(order))]
        public IHttpActionResult Getorder(int id)
        {
            List<OrderVM> orderVMs = new List<OrderVM>();
            var OrderObj = (from a in db.orders
                            join b in db.customers
                            on a.customer_id equals b.customer_id
                            join c in db.staffs
                            on a.staff_id equals c.staff_id
                            join d in db.stores
                            on a.store_id equals d.store_id
                            where a.order_id== id
                            select new
                            {
                                a.order_id,
                                d.store_name,
                                c.staff_first_name,
                                b.first_name,
                                a.shipped_date,
                                a.required_date ,
                                a.order_date,
                                a.order_status
                            }).ToList();
            if (OrderObj == null)
            {
                return NotFound();
            }

            foreach (var item in OrderObj)
            {
                OrderVM orderVMObj = new OrderVM();
                orderVMObj.order_id = item.order_id;
                orderVMObj.order_status = item.order_status;
                orderVMObj.required_date = item.required_date;
                orderVMObj.shipped_date = item.shipped_date;
                orderVMObj.staff_first_name = item.staff_first_name;
                orderVMObj.store_name = item.store_name;
                orderVMObj.first_name = item.first_name;
                orderVMObj.order_date = item.order_date;
                orderVMs.Add(orderVMObj);
            }
           

            return Ok(orderVMs);
        }

        // PUT: api/OrdersApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putorder(int id, order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.order_id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!orderExists(id))
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

        // POST: api/OrdersApi
        [ResponseType(typeof(order))]
        public IHttpActionResult Postorder(order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.order_id }, order);
        }

        // DELETE: api/OrdersApi/5
        [ResponseType(typeof(order))]
        public IHttpActionResult Deleteorder(int id)
        {
            order order = db.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool orderExists(int id)
        {
            return db.orders.Count(e => e.order_id == id) > 0;
        }
    }
}
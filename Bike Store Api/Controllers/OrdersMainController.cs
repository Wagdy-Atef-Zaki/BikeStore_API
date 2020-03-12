using Bike_Store_Api.Models;
using Bike_Store_Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Bike_Store_Api.Controllers
{
    public class OrdersMainController : Controller
    {
        BikeStoresEntities db = new BikeStoresEntities();
        // GET: OrdersMain
        public ActionResult Index(int? i)
        {
            IEnumerable<OrderVM> orderr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/OrdersApi");
                //HTTP GET
                var responseTask = client.GetAsync("OrdersApi");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderVM>>();
                    readTask.Wait();

                    orderr = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    orderr = Enumerable.Empty<OrderVM>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(orderr.ToPagedList(i ?? 1, 8));
        }

        // GET: OrdersMain/Details/5
        public ActionResult Details(int id)
        {
            OrderVM orderr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/OrdersApi");
                //HTTP GET
                var responseTask = client.GetAsync("OrdersApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<OrderVM>();
                    readTask.Wait();

                    orderr = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..


                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(orderr);
        }

        // GET: OrdersMain/Create
        public ActionResult Create()
        {
            ViewBag.store = db.stores.ToList();
            return View();
        }

        // POST: OrdersMain/Create
        [HttpPost]
        public ActionResult Create(order orderr)
        {
            ViewBag.store = db.stores.ToList();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/OrdersApi");
                var postTask = client.PostAsJsonAsync<order>("OrdersApi", orderr);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }

            return View("Create");
        }

        // GET: OrdersMain/Edit/5
        public ActionResult Edit(int id)
        {
           IEnumerable< OrderVM> orderr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/OrdersApi");
                //HTTP GET
                var responseTask = client.GetAsync("OrdersApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<OrderVM>>();
                    readTask.Wait();

                    orderr = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(orderr);
        }

        // POST: OrdersMain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, OrderVM orderr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/OrdersApi");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<OrderVM>("OrdersApi?id=" + id.ToString(), orderr);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(orderr);
        }

        // GET: OrdersMain/Delete/5
        public ActionResult Delete(int id)
        {
            IEnumerable < OrderVM> orderr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/OrdersApi");
                //HTTP GET
                var responseTask = client.GetAsync("OrdersApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync< IList<OrderVM>>();
                    readTask.Wait();

                    orderr = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(orderr);
        }

        // POST: OrdersMain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, order orderr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/OrdersApi");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("OrdersApi/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}

using Bike_Store_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Bike_Store_Api.Controllers
{
    public class OrderItemsMainController : Controller
    {
        // GET: OrderItemsMain
        public ActionResult Index()
        {
            IEnumerable<order_items> order_itemss = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/Order_ItemsApi");
                //HTTP GET
                var responseTask = client.GetAsync("Order_ItemsApi");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<order_items>>();
                    readTask.Wait();

                    order_itemss = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    order_itemss = Enumerable.Empty<order_items>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(order_itemss);
        }

        // GET: OrderItemsMain/Details/5
        public ActionResult Details(int id)
        {
            order_items order_itemss = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/Order_ItemsApi");
                //HTTP GET
                var responseTask = client.GetAsync("Order_ItemsApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<order_items>();
                    readTask.Wait();

                    order_itemss = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..


                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(order_itemss);
        }

        // GET: OrderItemsMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderItemsMain/Create
        [HttpPost]
        public ActionResult Create(order_items order_itemss)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/Order_ItemsApi");
                var postTask = client.PostAsJsonAsync<order_items>("Order_ItemsApi", order_itemss);
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

        // GET: OrderItemsMain/Edit/5
        public ActionResult Edit(int id)
        {
            order_items order_itemss = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/Order_ItemsApi");
                //HTTP GET
                var responseTask = client.GetAsync("Order_ItemsApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<order_items>();
                    readTask.Wait();

                    order_itemss = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(order_itemss);
        }

        // POST: OrderItemsMain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, order_items order_itemss)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/Order_ItemsApi");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<order_items>("Order_ItemsApi?id=" + id.ToString(), order_itemss);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(order_itemss);
        }

        // GET: OrderItemsMain/Delete/5
        public ActionResult Delete(int id)
        {
            order_items order_itemss = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/Order_ItemsApi");
                //HTTP GET
                var responseTask = client.GetAsync("Order_ItemsApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<order_items>();
                    readTask.Wait();

                    order_itemss = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(order_itemss);
        }
    

         // POST: OrderItemsMain/Delete/5
          [HttpPost]
          public ActionResult Delete(int id, order_items order_itemss)
          {
            using (var client = new HttpClient())
            {
               client.BaseAddress = new Uri("http://localhost:54964/api/Order_ItemsApi");

              //HTTP DELETE
               var deleteTask = client.DeleteAsync("Order_ItemsApi/" + id.ToString());
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


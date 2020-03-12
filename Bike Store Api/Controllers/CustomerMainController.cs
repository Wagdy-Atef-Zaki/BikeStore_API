using Bike_Store_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Bike_Store_Api.Controllers
{
    public class CustomerMainController : Controller
    {
        // GET: CustomerMain
        public ActionResult Index()
        {


            IEnumerable<customer> customerr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CustomersApi");
                //HTTP GET
                var responseTask = client.GetAsync("CustomersApi");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<customer>>();
                    readTask.Wait();

                    customerr = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    customerr = Enumerable.Empty<customer>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(customerr);
        }

        // GET: CustomerMain/Details/5
        public ActionResult Details(int id)
        {
            customer customerr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CustomersApi");
                //HTTP GET
                var responseTask = client.GetAsync("CustomersApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<customer>();
                    readTask.Wait();

                    customerr = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..


                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(customerr);
        }

        // GET: CustomerMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerMain/Create
        [HttpPost]
        public ActionResult Create(customer customerr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CustomersApi");
                var postTask = client.PostAsJsonAsync<customer>("CustomersApi", customerr);
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

        // GET: CustomerMain/Edit/5
        public ActionResult Edit(int id)
        {
            customer customerr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CustomersApi");
                //HTTP GET
                var responseTask = client.GetAsync("CustomersApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<customer>();
                    readTask.Wait();

                    customerr = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(customerr);
        }

        // POST: CustomerMain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,customer customerr)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CustomersApi");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<customer>("CustomersApi?id=" + id.ToString(), customerr);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(customerr);
        }

        // GET: CustomerMain/Delete/5
        public ActionResult Delete(int id)
        {
            customer customerr = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CustomersApi");
                //HTTP GET
                var responseTask = client.GetAsync("CustomersApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<customer>();
                    readTask.Wait();

                    customerr = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(customerr);
        }

        // POST: CustomerMain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, customer customerr)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CustomersApi");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("CustomersApi/" + id.ToString());
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

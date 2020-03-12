using Bike_Store_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Bike_Store_Api.Controllers
{
    public class StoresMainController : Controller
    {
        // GET: StoresMain
        public ActionResult Index()
        {
            IEnumerable<store> storee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StoresApi");
                //HTTP GET
                var responseTask = client.GetAsync("StoresApi");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<store>>();
                    readTask.Wait();

                    storee = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    storee = Enumerable.Empty<store>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(storee);
        }

        // GET: StoresMain/Details/5
        public ActionResult Details(int id)
        {
            store storee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StoresApi");
                //HTTP GET
                var responseTask = client.GetAsync("StoresApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<store>();
                    readTask.Wait();

                    storee = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..


                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(storee);
        }

        // GET: StoresMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoresMain/Create
        [HttpPost]
        public ActionResult Create(store storee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StoresApi");
                var postTask = client.PostAsJsonAsync<store>("StoresApi", storee);
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

        // GET: StoresMain/Edit/5
        public ActionResult Edit(int id)
        {
            store storee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StoresApi");
                //HTTP GET
                var responseTask = client.GetAsync("StoresApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<store>();
                    readTask.Wait();

                    storee = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(storee);
        }

        // POST: StoresMain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, store storee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StoresApi");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<store>("StoresApi?id=" + id.ToString(), storee);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(storee);
        }

        // GET: StoresMain/Delete/5
        public ActionResult Delete(int id)
        {
            store storee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StoresApi");
                //HTTP GET
                var responseTask = client.GetAsync("StoresApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<store>();
                    readTask.Wait();

                    storee = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(storee);
        }

        // POST: StoresMain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, store storee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StoresApi");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("StoresApi/" + id.ToString());
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

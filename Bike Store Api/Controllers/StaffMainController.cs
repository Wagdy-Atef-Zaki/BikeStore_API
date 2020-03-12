using Bike_Store_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Bike_Store_Api.Controllers
{
    public class StaffMainController : Controller
    {
        // GET: StaffMain
        public ActionResult Index()
        {
            IEnumerable<staff> stafff = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StaffsApi");
                //HTTP GET
                var responseTask = client.GetAsync("StaffsApi");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<staff>>();
                    readTask.Wait();

                    stafff = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    stafff = Enumerable.Empty<staff>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(stafff);
        }

        // GET: StaffMain/Details/5
        public ActionResult Details(int id)
        {
            staff stafff = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StaffsApi");
                //HTTP GET
                var responseTask = client.GetAsync("StaffsApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<staff>();
                    readTask.Wait();

                    stafff = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..


                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(stafff);
        }

        // GET: StaffMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffMain/Create
        [HttpPost]
        public ActionResult Create(staff stafff)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StaffsApi");
                var postTask = client.PostAsJsonAsync<staff>("StaffsApi", stafff);
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

        // GET: StaffMain/Edit/5
        public ActionResult Edit(int id)
        {
            staff stafff = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StaffsApi");
                //HTTP GET
                var responseTask = client.GetAsync("StaffsApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<staff>();
                    readTask.Wait();

                    stafff = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(stafff);
        }

        // POST: StaffMain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, staff stafff)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StaffsApi");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<staff>("StaffsApi?id=" + id.ToString(), stafff);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(stafff);
        }

        // GET: StaffMain/Delete/5
        public ActionResult Delete(int id)
        {
            staff stafff = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StaffsApi");
                //HTTP GET
                var responseTask = client.GetAsync("StaffsApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<staff>();
                    readTask.Wait();

                    stafff = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(stafff);
        }

        // POST: StaffMain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, staff stafff)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StaffsApi");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("StaffsApi/" + id.ToString());
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

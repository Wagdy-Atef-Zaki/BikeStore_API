using Bike_Store_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Bike_Store_Api.Controllers
{
    public class CategoriesMainController : Controller
    {
        // GET: CategoriesMain
        public ActionResult Index()
        {

            IEnumerable<category> categoryy = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CategoriesApi");
                //HTTP GET
                var responseTask = client.GetAsync("CategoriesApi");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<category>>();
                    readTask.Wait();

                    categoryy = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    categoryy = Enumerable.Empty<category>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(categoryy);
        }

        // GET: CategoriesMain/Details/5
        public ActionResult Details(int id)
        {
            category categoryy = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CategoriesApi");
                //HTTP GET
                var responseTask = client.GetAsync("CategoriesApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<category>();
                    readTask.Wait();

                    categoryy = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..


                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(categoryy);
        }

        // GET: CategoriesMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesMain/Create
        [HttpPost]
        public ActionResult Create(category categoryy)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CategoriesApi");
                var postTask = client.PostAsJsonAsync<category>("CategoriesApi", categoryy);
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

        // GET: CategoriesMain/Edit/5
        public ActionResult Edit(int id)
        {
            category categoryy = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CategoriesApi");
                //HTTP GET
                var responseTask = client.GetAsync("CategoriesApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<category>();
                    readTask.Wait();

                    categoryy = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(categoryy);
        }

        // POST: CategoriesMain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, category categoryy)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CategoriesApi");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<category>("CategoriesApi?id=" + id.ToString(), categoryy);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(categoryy);
        }

        // GET: CategoriesMain/Delete/5
        public ActionResult Delete(int id)
        {

            category categoryy = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CategoriesApi");
                //HTTP GET
                var responseTask = client.GetAsync("CategoriesApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<category>();
                    readTask.Wait();

                    categoryy = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(categoryy);
        }

        // POST: CategoriesMain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, category categoryy)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/CategoriesApi");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("CategoriesApi/" + id.ToString());
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

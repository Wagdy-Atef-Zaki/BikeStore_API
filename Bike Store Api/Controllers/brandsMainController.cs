using Bike_Store_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Bike_Store_Api.Controllers
{
    public class brandsMainController : Controller
    {
        // GET: brandsMain
        public ActionResult Index()
        {

            IEnumerable<brand> brandss = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/brandsApi");
                 //HTTP GET
                var responseTask = client.GetAsync("brandsApi");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<brand>>();
                    readTask.Wait();

                    brandss = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    brandss = Enumerable.Empty<brand>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(brandss);

        }

        // GET: brandsMain/Details/5
        public ActionResult Details(int id)
        {
            brand brandss = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/brandsApi");
                //HTTP GET
                var responseTask = client.GetAsync("brandsApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<brand>();
                    readTask.Wait();

                    brandss = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..


                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(brandss);

        }

        // GET: brandsMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: brandsMain/Create
        [HttpPost]
        public ActionResult Create(brand brand)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/brandsApi");
                var postTask = client.PostAsJsonAsync<brand>("brandsApi", brand);
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

        // GET: brandsMain/Edit/5
        public ActionResult Edit(int id)
        {
            brand brandss = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/brandsApi");
                //HTTP GET
                var responseTask = client.GetAsync("brandsApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<brand>();
                    readTask.Wait();

                    brandss = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(brandss);

        }

        // POST: brandsMain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, brand brand)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/brandsApi");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<brand>("brandsApi?id=" + id.ToString(), brand);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(brand);

        }

        // GET: brandsMain/Delete/5
        public ActionResult Delete(int id)
        {
            brand brandss = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/brandsApi");
                //HTTP GET
                var responseTask = client.GetAsync("brandsApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<brand>();
                    readTask.Wait();

                    brandss = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(brandss);

        }

        // POST: brandsMain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, brand brand)
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

using Bike_Store_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Bike_Store_Api.ViewModels;

namespace Bike_Store_Api.Controllers
{
    public class ProductMainController : Controller
    {
        BikeStoresEntities db = new BikeStoresEntities();
        // GET: ProductMain
        public ActionResult Index(int? i)
        {
            IEnumerable<ProductVM> productt = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/ProductVMsApi");
                //HTTP GET
                var responseTask = client.GetAsync("ProductVMsApi");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductVM>>();
                    readTask.Wait();

                    productt = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    productt = Enumerable.Empty<ProductVM>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(productt.ToPagedList(i ?? 1,8));
        }

        // GET: ProductMain/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<ProductVM> productt = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/ProductVMsApi");
                //HTTP GET
                var responseTask = client.GetAsync("ProductVMsApi?id=" + id.ToString());
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductVM>>();
                    readTask.Wait();

                    productt = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    productt = Enumerable.Empty<ProductVM>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(productt);

        }


       
        public ActionResult Create()
        {
            ViewBag.brand = db.brands.ToList();
            ViewBag.category = db.categories.ToList();
            return View();
        }

        // POST: ProductMain/Create
        [HttpPost]
        public ActionResult Create(product productt)
        {
            ViewBag.brand = db.brands.ToList();
            ViewBag.category = db.categories.ToList();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/ProductVMsApi");
                var postTask = client.PostAsJsonAsync<product>("ProductVMsApi", productt);
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

        // GET: ProductMain/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.brand = db.brands.ToList();
            ViewBag.category = db.categories.ToList();

            IEnumerable<ProductVM> productt = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/ProductVMsApi");
                //HTTP GET
                var responseTask = client.GetAsync("ProductVMsApi?id=" + id.ToString());
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductVM>>();
                    readTask.Wait();

                    productt = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    productt = Enumerable.Empty<ProductVM>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(productt);

            //product productt = null;

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:54964/api/ProductVMsApi");
            //    //HTTP GET
            //    var responseTask = client.GetAsync("ProductVMsApi?id=" + id.ToString());
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<product>();
            //        readTask.Wait();

            //        productt = readTask.Result;
            //    }
            //    else //web api sent error response 
            //    {
            //        //log response status here..
            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //    }
            //}
            //return View(productt);
        }

        // POST: ProductMain/Edit/5
        [HttpPost]
        public ActionResult Edit( product productt)
        {
            ViewBag.brand = db.brands.ToList();
            ViewBag.category = db.categories.ToList();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/ProductVMsApi");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<product>("ProductVMsApi?id=" + productt.product_id.ToString(), productt);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(productt);
        }

        // GET: ProductMain/Delete/5
        public ActionResult Delete(int id)
        {
            IEnumerable<ProductVM> productt = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/ProductVMsApi");
                //HTTP GET
                var responseTask = client.GetAsync("ProductVMsApi?id=" + id.ToString());
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ProductVM>>();
                    readTask.Wait();

                    productt = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    productt = Enumerable.Empty<ProductVM>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(productt);
        }

        // POST: ProductMain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, product productt)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/ProductVMsApi");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("ProductVMsApi/" + id.ToString());
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

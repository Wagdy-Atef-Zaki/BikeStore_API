using Bike_Store_Api.Models;
using Bike_Store_Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Bike_Store_Api.Controllers
{
    public class StocksMainController : Controller
    {
        BikeStoresEntities db = new BikeStoresEntities();
        // GET: StocksMain
        public ActionResult Index()
        {
            IEnumerable<StockVM> stockk = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StocksApi");
                //HTTP GET
                var responseTask = client.GetAsync("StocksApi");
                responseTask.Wait();

                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<StockVM>>();
                    readTask.Wait();

                    stockk = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    stockk = Enumerable.Empty<StockVM>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(stockk);
        }

        // GET: StocksMain/Details/5
        public ActionResult Details(int id)
        {

            IEnumerable < StockVM> stockk = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StocksApi");
                //HTTP GET
                var responseTask = client.GetAsync("StocksApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<StockVM>>();
                    readTask.Wait();

                    stockk = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..


                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(stockk);
        }

        // GET: StocksMain/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StocksMain/Create
        [HttpPost]
        public ActionResult Create(stock stockk)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StocksApi");
                var postTask = client.PostAsJsonAsync<stock>("StocksApi", stockk);
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

        // GET: StocksMain/Edit/5
        public ActionResult Edit(int id)
        {
            IEnumerable < StockVM> stockk = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StocksApi");
                //HTTP GET
                var responseTask = client.GetAsync("StocksApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<StockVM>>();
                    readTask.Wait();

                    stockk = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(stockk);
        }

        // POST: StocksMain/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, stock stockk)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StocksApi");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<stock>("StocksApi?id=" + id.ToString(), stockk);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(stockk);
        }

        // GET: StocksMain/Delete/5
        public ActionResult Delete(int id)
        {

            IEnumerable <StockVM> stockk = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StocksApi");
                //HTTP GET
                var responseTask = client.GetAsync("StocksApi?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<StockVM>>();
                    readTask.Wait();

                    stockk = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(stockk);
        }

        // POST: StocksMain/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, StockVM stockk)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54964/api/StocksApi");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("StocksApi/" + id.ToString());
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

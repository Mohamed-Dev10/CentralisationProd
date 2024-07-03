using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentralisationV0.Controllers
{
    public class DonneeController : Controller
    {
        // GET: Donnee
        public ActionResult Index()
        {
            return View();
        }

        // GET: Donnee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Donnee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donnee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Donnee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Donnee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Donnee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Donnee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelBlog.Controllers
{
    public class LocationController : Controller
    {
        private TravelBlogContext db = new TravelBlogContext();
        public IActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var thisLocation = db.Locations.FirstOrDefault(Location => Location.LocationId == id);
            ViewBag.LocationId = new SelectList(db.Locations, "Name");
            return View(thisLocation);
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            db.Entry(location).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public IActionResult Delete(int id)
        //{
        //    var thisLocation = db.Locations.FirstOrDefault(Location => Location.LocationId == id);
        //    return View(thisLocation);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    var thisLocation = db.Locations.FirstOrDefault(Location => Location.LocationId == id);
        //    db.Locations.Remove(thisLocation);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}

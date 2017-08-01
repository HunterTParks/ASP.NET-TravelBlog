using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace TravelBlog.Controllers
{
    public class ExperienceController : Controller
    {
        private TravelBlogContext db = new TravelBlogContext();
        public IActionResult Index()
        {
            return View(db.Experiences.Include(Experiences => Experiences.Location).ToList());
        }
        public IActionResult Details(int id)
        {
            var selectedLocation = db.Locations.Include(Locations => Locations.Experiences).FirstOrDefault(Location => Location.LocationId == id);
            return View(selectedLocation);
        }
        public IActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Experience experience)
        {
            db.Experiences.Add(experience);
            db.SaveChanges();
            return RedirectToAction("Confirm");
        }
        public IActionResult Edit(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(Experience => Experience.ExperienceId == id);
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View(thisExperience);
        }

        [HttpPost]
        public IActionResult Edit(Experience Experience)
        {
            db.Entry(Experience).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Confirm");
        }
        public IActionResult Delete(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(Experience => Experience.ExperienceId == id);
            return View(thisExperience);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisExperience = db.Experiences.FirstOrDefault(Experience => Experience.ExperienceId == id);
            db.Experiences.Remove(thisExperience);
            db.SaveChanges();
            return RedirectToAction("Confirm");
        }

        public IActionResult Confirm()
        {
            return View();
        }
    }
}

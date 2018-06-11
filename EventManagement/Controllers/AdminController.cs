﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManagement.Models;
using EventManagement.Views.Events;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using EventsManagement.Models;
using System.Net;

namespace EventManagement.Controllers
{
    public class AdminController : Controller
    {
        private EventsManagement.Models.AppContext db = new EventsManagement.Models.AppContext();

        // GET: Admin
        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            var events = db.Events.Include(m => m.Author).Include(m => m.State);
            return View(events.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddIndex(int? id)   ///// id - EVENT ID /////////////
        {
            var users = db.Users.Include(u => u.OrganizationalUnit);
            var takePart = db.User_Event.Where(m => m.EventId == id).Select(m => m.UserId).ToList();

            ViewData["takePart"] = takePart;
            TempData["Event_ID"] = id;
            //TempData["Event_ID"] = id;
            //return View(users.ToList());
            //List<object[]> list = new List<object[]> { new object[] { users, takePart, Event_id } };
            return View(users.ToList());
        }

        [Authorize]
        public ActionResult Add(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User @user = db.Users.Include(u => u.OrganizationalUnit).Where(x => x.Id == id).First();
            //string a = "sdad";
            //var x = db.Users.Where(m => m.Name.Contains(a));
            if (@user == null)
            {
                return HttpNotFound();
            }
            return View(@user);
        }

        // POST: Events/Add/5
        [Authorize]
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddConfirmed(string id)
        {
            User @user = db.Users.Find(id);
           // var eventId = TempData["Event_ID"].ToString();
            var user_event = new User_Event();
            user_event.UserId = id;
            user_event.EventId = (int)TempData["Event_ID"];
            db.User_Event.Add(@user_event);
            db.SaveChanges();
            return RedirectToAction("AddIndex", "Admin");
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(m => m.Author).Include(m => m.State).Where(m => m.Id == id).First();
            var evDetails = new EventDetailsViewModel();
            //evDetails.Participants = db.Users.Join(db.User_Event, m => m.Id == s => s.)
            evDetails.AuthorId = @event.AuthorId;
            evDetails.Title = @event.Title;
            evDetails.Description = @event.Description;
            evDetails.Place = @event.Place;
            evDetails.EventDate = @event.EventDate;
            evDetails.EventTime = @event.EventTime;
            evDetails.EventAdd = @event.EventAdd;
            evDetails.StateId = @event.StateId;
            var x = from user in db.Users
                    join user_event in db.User_Event
                    on user.Id equals user_event.UserId
                    where id == user_event.EventId
                    select user;
            List<String> primes = new List<String>();
            if (x.Any())
            {
                foreach (var item in x)
                {
                    primes.Add(item.PelneNazwisko);
                }
            }
            else
                primes.Add("Brak uczestników, dołącz jako pierwszy!");

            ViewData["Participants"] = primes;

            if (@event == null)
            {
                return HttpNotFound();
            }
            //return Content(text);
            return View(@event);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
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

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
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
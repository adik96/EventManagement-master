using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventManagement.Models;
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
        public ActionResult AddIndex(int? id) 
        {
            var users = db.Users.Include(u => u.OrganizationalUnit);
            var takePart = db.User_Event.Where(m => m.EventId == id).Select(m => m.UserId).ToArray();

            ViewData["takePart"] = takePart;
            TempData["Event_ID"] = id;
            ViewBag.EventName = db.Events.Where(m => m.Id == id).First().Title;
            return View(users.ToList());
        }

        [Authorize(Roles = "admin")]
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
            //user_event.EventId = (int)TempData["Event_ID"];
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
            var x = from user in db.Users
                    join user_event in db.User_Event
                    on user.Id equals user_event.UserId
                    where id == user_event.EventId
                    select user;
            List<String> participating = new List<String>();
            if (x.Any())
            {
                foreach (var item in x)
                {
                    participating.Add(item.PelneNazwisko);
                }
            }
            else
                participating.Add("Brak uczestników, dołącz jako pierwszy!");

            ViewData["participating"] = participating;

            if (@event == null)
            {
                return HttpNotFound();
            }
            //return Content(text);
            return View(@event);
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

        [Authorize(Roles = "admin")]
        [Route("Admin/Include/{EventId}/{id}")]
        public ActionResult Include(int EventId, string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(m => m.State).Where(x => x.Id == EventId).First();
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Include/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Include")]
        [ValidateAntiForgeryToken]
        [Route("Admin/Include/{EventId}/{id}")]
        public ActionResult IncludeConfirmed(int EventId, string id)
        {
            var user_event = new User_Event();
            user_event.UserId = id;
            user_event.EventId = EventId;
            db.User_Event.Add(@user_event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [Route("Admin/ExitEvent/{EventId}/{id}")]
        public ActionResult ExitEvent(int EventId, string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(m => m.State).Where(x => x.Id == EventId).First();
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Exit/5
        [Authorize(Roles = "admin")]
        [Route("Admin/ExitEvent/{EventId}/{id}")]
        [HttpPost, ActionName("ExitEvent")]
        [ValidateAntiForgeryToken]
        public ActionResult ExitEventConfirmed(int EventId, string id)
        {
            User_Event @user_event = db.User_Event.Where(m => m.EventId == EventId).Where(x => x.UserId == id).First();
            db.User_Event.Remove(@user_event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

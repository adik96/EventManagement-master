using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventManagement.Models;
//using EventManagement.Views.Events;
using EventsManagement.Models;
using Microsoft.AspNet.Identity;

namespace EventManagement.Controllers
{
    public class EventsController : Controller
    {
        private EventsManagement.Models.AppContext db = new EventsManagement.Models.AppContext();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events.Include(m => m.Author).Include(m => m.State);
            return View(events.ToList());
        }

        // GET: Events/Details/5
        /*public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Include(m => m.Author).Include(m => m.State).Where(x => x.Id == id).First();
            //string x = id.ToString();
            //@event = db.Events.Include(m => m.Author).Include(m => m.State);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }*/

        // GET: Events/Create
        [Authorize]
        public ActionResult Create()
        {

            //ViewBag.AuthorId = new SelectList(db.Users, "Id", "Email");
            ViewBag.StateId = new SelectList(db.States, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Place,EventDate,EventTime,EventAdd,AuthorId,StateId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                var authorId = User.Identity.GetUserId();
                @event.AuthorId = authorId;
                @event.EventAdd = DateTime.Now;
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Email", @event.AuthorId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", @event.StateId);
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Email", @event.AuthorId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", @event.StateId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Place,EventDate,EventTime,EventAdd,AuthorId,StateId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Users, "Id", "Email", @event.AuthorId);
            ViewBag.StateId = new SelectList(db.States, "Id", "Name", @event.StateId);
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // kome ///////////////////////////////////////////////////////////////////////////////////////////// take part in event
        [Authorize]
        public ActionResult Include(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Event @event = db.Events.Find(id);
            Event @event = db.Events.Include(m => m.State).Where(x => x.Id == id).First();
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Include/5
        [Authorize]
        [HttpPost, ActionName("Include")]
        [ValidateAntiForgeryToken]
        public ActionResult IncludeConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            
            var userId = User.Identity.GetUserId();
            var user_event = new User_Event();
            user_event.UserId = userId;
            user_event.EventId = id;
            db.User_Event.Add(@user_event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ExitEvent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Event @event = db.Events.Find(id);
            Event @event = db.Events.Include(m => m.State).Where(x => x.Id == id).First();
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Include/5
        [Authorize]
        [HttpPost, ActionName("ExitEvent")]
        [ValidateAntiForgeryToken]
        public ActionResult ExitEventConfirmed(int id)
        {
            //Event @event = db.Events.Find(id);
            User_Event @user_event = db.User_Event.Where(m => m.EventId == id).First();
            db.User_Event.Remove(@user_event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult MyEvents(string id)
        {
            var userId = User.Identity.GetUserId();
            var myEvents = db.Events.Include(m => m.State).Where(m => m.AuthorId == userId);

            var takePart = db.User_Event.Where(m => m.UserId == userId).Select(m => m.EventId);
            ViewData["takePart"] = takePart.ToArray();

            return View(myEvents.ToList());
        }

        // GET: Events/Details/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

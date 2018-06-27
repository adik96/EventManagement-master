using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.SqlServer;
using System.Data.Entity;

namespace EventManagement.Controllers
{
    public class NotificationController : Controller
    {
        private EventsManagement.Models.AppContext db = new EventsManagement.Models.AppContext();

        // GET: Notification
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var ntf = from ev in db.Events
                           join ue in db.User_Event on ev.Id equals ue.EventId
                           //where userId == ue.UserId && ev.EventDate.Subtract(DateTime.Now).TotalDays < 2
                           //where userId == ue.UserId && (int)SqlFunctions.DiffDays("dd", ev.EventDate, DateTime.Now) <= 2 //&& (int)SqlFunctions.DateDiff("hour", ev.EventDate, DateTime.Now) >=0
                           //where userId == ue.UserId && ev.EventDate.CompareTo(now)
                           //where userId == ue.UserId && DbFunctions.DiffDays(ev.EventDate, DateTime.Now) <= 2
                           where userId == ue.UserId && DbFunctions.DiffDays(DateTime.Now, ev.EventDate) <= 2 && DbFunctions.DiffDays(DateTime.Now, ev.EventDate) >=0
                           select ev;

            return View(ntf);
        }

        // GET: Notification/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Notification/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notification/Create
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

        // GET: Notification/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Notification/Edit/5
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

        // GET: Notification/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Notification/Delete/5
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

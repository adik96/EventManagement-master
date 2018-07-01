using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.SqlServer;
using System.Data.Entity;
using EventManagement.Models;
using EventsManagement.Models;

namespace EventManagement.Controllers
{
    public class NotificationController : Controller
    {
        private EventsManagement.Models.AppContext db = new EventsManagement.Models.AppContext();

        // GET: Notification
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var all_ev_ntf = from ev in db.Events
                           join ue in db.User_Event on ev.Id equals ue.EventId
                           where userId == ue.UserId && DbFunctions.DiffDays(DateTime.Now, ev.EventDate) <= 2 && DbFunctions.DiffDays(DateTime.Now, ev.EventDate) >=0 //&& userId != n.UserId
                      select ev;
            var ntf_hide_user = db.NtfHides.Where(q => q.UserId == userId);
            var wynik = all_ev_ntf.Where(a => !ntf_hide_user.Any(h => a.Id.ToString().Contains(h.EventId.ToString())));

            //var test2NotInTest1 = test2.Where(t2 => !test1.Any(t1 => t2.Contains(t1)));

            return View(wynik);
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

        // GET: Notification/Hide/5
        public ActionResult Hide(int id)
        {
            var x = db.Events.Find(id);
            return View(x);
        }

        // POST: Notification/Hide/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Hide(int id, NtfHide @ntf)
        {
            @ntf.EventId = id;
            @ntf.UserId = User.Identity.GetUserId();
            db.NtfHides.Add(@ntf);
            db.SaveChanges();
            return RedirectToAction("Index");

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

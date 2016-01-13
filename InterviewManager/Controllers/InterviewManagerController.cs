using InterviewManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewManager.Controllers
{
    public class InterviewManagerController : Controller
    {
        private IEWSAPIClient _client;
        public InterviewManagerController()
        {
            _client = new EWSIntegrationClient();
        }

        public ActionResult Load(string user)
        {
            var response = _client.GetAvailability(new AvailabilityRequest { DurationMinutes = 60, NumberOfDaysFromNow = 40, Users = new List<string> { user} });
            var list = new List<EventObject>();
            foreach (var res in response.AvailabilityResult)
            {
               foreach(var ev in res.Availability)
                {
                    var e = new EventObject
                    {
                        start = ev.Start.ToString("o"),
                        end = ev.End.ToString("o"),
                        title = ev.Status,
                        allDay = false
                    };
                    list.Add(e);
                }
            }

            var model = new AppointmentManager { Events = list };

            return PartialView("_CalendarPartial", model);
        }
        // GET: InterviewManager
        public ActionResult Index()
        {
            
            var list = new List<EventObject>
            {
                new EventObject
                {
                    title = "Event 1",
                    start = new DateTime(2016, 1, 13, 13, 0, 0).ToString("o"),
                    end = new DateTime(2016, 1, 13, 14, 0, 0).ToString("o"),
                    allDay = false


        },
                new EventObject
                {
                    title = "Event 2",
                    start = new DateTime(2016, 1, 14, 13, 0, 0).ToString("o"),
                    end = new DateTime(2016, 1, 14, 14, 0, 0).ToString("o"),
                    allDay = false
                }
            };
            var model = new AppointmentManager { Events = list};
            return View(model);
        }

        public JsonResult GetInterviewerCalendar(DateTime start, DateTime end)
        {
            var startLocal = start.ToUniversalTime();
            var loacalEnd = end.ToUniversalTime();

            var list = new List<EventObject>
            {
                new EventObject
                {
                    title = "Event 1",
                    start = new DateTime(2016, 1, 13, 13, 0, 0).ToUniversalTime().ToString("o"),
                    end = new DateTime(2016, 1, 13, 14, 0, 0).ToUniversalTime().ToString("o"),
                    allDay = false
                    

        },
                new EventObject
                {
                    title = "Event 2",
                    start = new DateTime(2016, 1, 14, 13, 0, 0).ToUniversalTime().ToString("o"),
                    end = new DateTime(2016, 1, 14, 14, 0, 0).ToUniversalTime().ToString("o"),
                    allDay = false
                }
            };
            return Json(list);
        }

        // GET: InterviewManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InterviewManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InterviewManager/Create
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

        // GET: InterviewManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InterviewManager/Edit/5
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

        // GET: InterviewManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InterviewManager/Delete/5
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

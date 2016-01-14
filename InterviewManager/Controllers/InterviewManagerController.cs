using InterviewManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<ActionResult> Load(string user)
        {
            var rep = new EWSIntegrationClient();

            var request = new AvailabilityRequest
            {
                DurationMinutes = 60,
                NumberOfDaysFromNow = 30,
                Users = new List<string> { user }
            };

            var result = await rep.GetAvailability(request);

            var obj = result.AvailabilityResult;
            int i = 0;
            var list = new List<EventObject>();

            foreach (var avai in result.AvailabilityResult)
            {
                foreach (var block in avai.Availability)
                {
                    var eventObject = new EventObject
                    {
                        title = "Interviewer " + i + " Status: " + block.Status,
                        start = block.Start.ToString("o"),
                        end = block.End.ToString("o"),
                        allDay = false,
                        backgroundColor = "red"
                    };
                    list.Add(eventObject);
                }
                i++;
            }

            var model = new AppointmentManager { Events = list };

            return PartialView("_CalendarPartial", model);
        }

        // GET: InterviewManager
        public async Task<ActionResult> Index()
        {
            var rep = new EWSIntegrationClient();

            var request = new AvailabilityRequest
            {
                DurationMinutes = 60,
                NumberOfDaysFromNow = 30,
                Users = new List<string> { "mario@rossrmsdemo.onmicrosoft.com" }
            };

            var result = await rep.GetAvailability(request);

            var obj = result.AvailabilityResult;
            int i = 0;
            var list = new List<EventObject>();

            foreach (var avai in result.AvailabilityResult)
            {
                foreach(var block in avai.Availability)
                {
                    var eventObject = new EventObject
                    {
                        title = "Interviewer " + i + " Status: " + block.Status,
                        start = block.Start.ToString("o"),
                        end = block.End.ToString("o"),
                        allDay = false,
                        backgroundColor = "red"
                    };
                    list.Add(eventObject);
                }
                i++;
            }
            
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

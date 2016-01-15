using InterviewManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InterviewManager.Controllers
{
    public class InterviewManagerController : Controller
    {
        private IEWSAPIClient _client;
        private List<string> _colors;
        public InterviewManagerController()
        {
            _client = new EWSIntegrationClient();
            _colors = new List<string> { "#DDD", "##DFF0D8", "#D9EDF7", "FCF8E3" };
        }

        [HttpGet]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult CreateEventFormPopUp(string start, List<string> users)
        {
            var startTime = DateTime.Parse(start);
            var first = true;
            var list = new List<SelectListItem>();
            var time = startTime.AddMinutes(30);
            while (true)
            {
                list.Add(new SelectListItem
                {
                    Selected = first,
                    Text = time.ToString("t"),
                    Value = time.ToString("o")
                });
                first = false;
                if (time.Hour == 0)
                {
                    break;
                }
                time = time.AddMinutes(30);

            }

            EventObject model = new EventObject { start = startTime.ToString("o"), displayStart = startTime.ToString("t") };
            var viewModel = new CreateEventViewModel { EndTimeList = list, Event = model, Users = users };
            return PartialView("_CreateEventPartial", viewModel);
        }

        public async Task<ActionResult> CreateEvent(CreateEventViewModel eventObject)
        {
            var model = eventObject;

            // Create EWS Appointment

            var request = new CreateAppointmentRequest
            {
                Body = "Created From Web App",
                End = DateTime.Parse(eventObject.Event.end).ToString(),
                Start = DateTime.Parse(eventObject.Event.start).ToString(),
                Location = "Web",
                Subject = eventObject.Event.title,

                Recipients = eventObject.Users

            };
            var service = new EWSIntegrationClient();
            var resp = await service.CreateAppointment(request);

            // Send Emails
            var emailRequest = new SendEmailRequest
            {
                Recipients = eventObject.Users,
                Body = "Automated Email After Appointment Creation",
                FileAttachments = new List<string>(),
                Subject = "Automated Email After Appointment Creation"
            };
            var sendEmailResponse = await service.SendEmail(emailRequest);

            return RedirectToAction("Index");
        }

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> GetJsonEvents(List<string> users, string start, string end)
        {
            var rep = new EWSIntegrationClient();

            var request = new AvailabilityRequest
            {
                DurationMinutes = 60,
                Start = DateTime.Parse(start).ToString(),
                End = DateTime.Parse(end).ToString(),
                Users = users == null ? new List<string>() : users
            };

            var result = await rep.GetAvailability(request);

            var obj = result.AvailabilityResult;
            int i = 0;

            // Part of a hack. for this demo we only expect <=5 users.
            var colorIndex = 0;
            var list = new List<EventObject>();

            foreach (var avai in result.AvailabilityResult)
            {

                var color = _colors[(i == _colors.Count) ? colorIndex = 0 : colorIndex++];
                foreach (var block in avai.Availability)
                {
                    var eventObject = new EventObject
                    {
                        title = "Interviewer " + i + " Status: " + block.Status,
                        start = block.Start.ToString("o"),
                        end = block.End.ToString("o"),
                        allDay = false,
                        backgroundColor = color
                    };
                    list.Add(eventObject);
                }
                i++;
            }

            var model = new AppointmentManager { Events = list };

            return Json( model.Events,JsonRequestBehavior.AllowGet);
        }

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<ActionResult> Load(List<string> users, string start, string end)
        {
            var rep = new EWSIntegrationClient();

            var request = new AvailabilityRequest
            {
                DurationMinutes = 60,
                Start = DateTime.Parse(start).ToString(),
                End = DateTime.Parse(end).ToString(),
                Users = users == null ? new List<string>() : users
            };

            var result = await rep.GetAvailability(request);

            var obj = result.AvailabilityResult;
            int i = 0;

            // Part of a hack. for this demo we only expect <=5 users.
            var colorIndex = 0;
            var list = new List<EventObject>();

            foreach (var avai in result.AvailabilityResult)
            {

                var color = _colors[(i == _colors.Count) ? colorIndex = 0 : colorIndex++];
                foreach (var block in avai.Availability)
                {
                    var eventObject = new EventObject
                    {
                        title = "Interviewer " + i + " Status: " + block.Status,
                        start = block.Start.ToString("o"),
                        end = block.End.ToString("o"),
                        allDay = false,
                        backgroundColor = color
                    };
                    list.Add(eventObject);
                }
                i++;
            }

            var model = new AppointmentManager { Events = list };

            return PartialView("_CalendarPartial", model);
        }

        // GET: InterviewManager
        public ActionResult Index()
        {
            var rep = new EWSIntegrationClient();

            //var request = new AvailabilityRequest
            //{
            //    DurationMinutes = 60,
            //    NumberOfDaysFromNow = 30,
            //    Users = new List<string> { "mario@rossrmsdemo.onmicrosoft.com" }
            //};

            //var result = await rep.GetAvailability(request);

            //var obj = result.AvailabilityResult;
            //int i = 0;
            //var list = new List<EventObject>();

            //foreach (var avai in result.AvailabilityResult)
            //{
            //    foreach(var block in avai.Availability)
            //    {
            //        var eventObject = new EventObject
            //        {
            //            title = "Interviewer " + i + " Status: " + block.Status,
            //            start = block.Start.ToString("o"),
            //            end = block.End.ToString("o"),
            //            allDay = false,
            //            backgroundColor = "red"
            //        };
            //        list.Add(eventObject);
            //    }
            //    i++;
            //}
            
            var model = new AppointmentManager { Events = new List<EventObject>()};
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

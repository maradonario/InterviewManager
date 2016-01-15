using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterviewManager.Models;
using System.Threading.Tasks;

namespace InterviewManager.Controllers
{
    public class InterviewScheduleController : Controller
    {
        private IEWSAPIClient _client;

        public InterviewScheduleController()
        {
            _client = new EWSIntegrationClient();
        }

        public ActionResult SendFollowUpEmail(InterviewScheduleViewModel model)
        {
            return RedirectToAction("Index");
        }

        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
        public async Task<JsonResult> GetJsonEvents(string start, string end)
        {
            var model = await CreateModel(start, end);
            return Json(model.Events, JsonRequestBehavior.AllowGet);
        }


        // GET: InterviewSchedule
        public async Task<ActionResult> Index()
        {
            var start = DateTime.UtcNow.ToString();
            var end = DateTime.UtcNow.AddDays(30).ToString();
            var model = await CreateModel(start, end);
            
            return View(model);
        }

        private async Task<InterviewScheduleViewModel> CreateModel(string start, string end)
        {


            var request = new GetAppointmentsRequest
            {
                Start = start,
                End = end
            };

            var response = await _client.GetAppointments(request);

            var events = new List<EventObject>();
            int i = 0;
            foreach (var app in response.Appointments)
            {
                events.Add(new EventObject
                {
                    title = app.Subject,
                    start = app.Start.ToString("o"),
                    end = app.End.ToString("o"),
                    allDay = false,
                    backgroundColor = "green"
                });

                response.Appointments.ElementAt(i).StartViewTime = response.Appointments.ElementAt(i).Start.ToString("t");
                response.Appointments.ElementAt(i).EndViewTime = response.Appointments.ElementAt(i).End.ToString("t");
                i++;
            }
            var model = new InterviewScheduleViewModel
            {
                Appointments = response.Appointments,
                Events = events,
                Start = start,
                End = end
            };

            return model;
        }

    }
}
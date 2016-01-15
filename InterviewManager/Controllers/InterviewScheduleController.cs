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

        // GET: InterviewSchedule
        public async Task<ActionResult> Index()
        {
            var start = DateTime.UtcNow.ToString();
            var end = DateTime.UtcNow.ToString();

            var request = new GetAppointmentsRequest
            {
                Start = start,
                End = end
            };

            var response = await _client.GetAppointments(request);

            var model = new InterviewScheduleViewModel
            {
                Appointments = response.Appointments
            };
            return View(model);
        }
    }
}
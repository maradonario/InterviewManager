using InterviewManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewManager
{
    interface IEWSAPIClient
    {
        AvailabilityResponse GetAvailability(AvailabilityRequest request);

        SendEmailResponse SendEmail(SendEmailRequest request);

        CreateAppointmentResponse CreateAppointment(CreateAppointmentRequest request);
    }
}

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
        Task<AvailabilityResponse> GetAvailability(AvailabilityRequest request);

        Task<SendEmailResponse> SendEmail(SendEmailRequest request);

        Task<CreateAppointmentResponse> CreateAppointment(CreateAppointmentRequest request);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewManager.Models;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;

namespace InterviewManager
{
    public class EWSIntegrationClient : IEWSAPIClient
    {
        private const string URL = "EWS_URL";

        private string _url;
        public EWSIntegrationClient()
        {
            _url = ConfigurationManager.AppSettings[URL];
        }

        public EWSIntegrationClient(string url)
        {
            _url = url;
        }


        public CreateAppointmentResponse CreateAppointment(CreateAppointmentRequest request)
        {
            var result = Create(request);

            return result.Result;
        }

        public  AvailabilityResponse GetAvailability(AvailabilityRequest request)
        {
            var result = Get(request);

            return result.Result;
        }

        private async Task<AvailabilityResponse> Get(AvailabilityRequest request)
        {
            var response = new AvailabilityResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage resp = await client.PostAsJsonAsync("/api/appointments/availability", request);
                if (resp.IsSuccessStatusCode)
                {
                    response = await resp.Content.ReadAsAsync<AvailabilityResponse>();

                    return response;
                }
            }

            return response;
        }

        private async Task<SendEmailResponse> Send(SendEmailRequest request)
        {
            var response = new SendEmailResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage resp = await client.PostAsJsonAsync("/api/email", request);
                if (resp.IsSuccessStatusCode)
                {
                    response = await resp.Content.ReadAsAsync<SendEmailResponse>();

                    return response;
                }
            }

            return response;
        }

        private async Task<CreateAppointmentResponse> Create(CreateAppointmentRequest request)
        {
            var response = new CreateAppointmentResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage resp = await client.PostAsJsonAsync("/api/appointments", request);
                if (resp.IsSuccessStatusCode)
                {
                    response = await resp.Content.ReadAsAsync<CreateAppointmentResponse>();

                    return response;
                }
            }

            return response;
        }

        public SendEmailResponse SendEmail(SendEmailRequest request)
        {
            var result = Send(request);

            return result.Result;
        }
    }
}

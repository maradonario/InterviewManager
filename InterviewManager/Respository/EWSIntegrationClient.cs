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

        public async Task<GetAppointmentsResponse> GetAppointments(GetAppointmentsRequest request)
        {
            var response = new GetAppointmentsResponse();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage resp = await client.PostAsJsonAsync("/api/appointments/details", request);
                if (resp.IsSuccessStatusCode)
                {
                    response = await resp.Content.ReadAsAsync<GetAppointmentsResponse>();

                    return response;
                }
            }
            return response;
        }

        public async Task<CreateAppointmentResponse> CreateAppointment(CreateAppointmentRequest request)
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

        public async Task<AvailabilityResponse> GetAvailability(AvailabilityRequest request)
        {
            var response = new AvailabilityResponse();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(_url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage resp =  await client.PostAsJsonAsync("/api/appointments/availability", request);
                if (resp.IsSuccessStatusCode)
                {
                    response = await resp.Content.ReadAsAsync<AvailabilityResponse>();

                    return response;
                }
            }
            return response;
        }

        public async Task<SendEmailResponse> SendEmail(SendEmailRequest request)
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
    }
}

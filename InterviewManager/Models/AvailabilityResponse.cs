using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InterviewManager.Models
{
    public class AvailabilityResponse
    {
        public ICollection<AvailabilityUser> AvailabilityResult { get; set; }

        public static implicit operator HttpResponseMessage(AvailabilityResponse v)
        {
            throw new NotImplementedException();
        }
    }

    public class AvailabilityUser
    {
        public ICollection<TimeBlock> Availability { get; set; }
    }

    public class TimeBlock
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewManager.Models
{
    public class AvailabilityRequest
    {
        public ICollection<string> Users { get; set; }

        public int DurationMinutes { get; set; }

        public int NumberOfDaysFromNow { get; set; }
    }
}

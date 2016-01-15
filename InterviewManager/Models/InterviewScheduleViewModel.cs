using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewManager.Models
{
    public class InterviewScheduleViewModel
    {
        public ICollection<Interview> Appointments { get; set; }

        public ICollection<EventObject> Events { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

    }
}

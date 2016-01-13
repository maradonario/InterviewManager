using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewManager.Models
{
    public class AppointmentManager
    {
        public ICollection<EventObject> Events { get; set; } 
    }
}

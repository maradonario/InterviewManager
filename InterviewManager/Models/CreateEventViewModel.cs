using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InterviewManager.Models
{
    public class CreateEventViewModel
    {
        public EventObject Event { get; set; }

        public IEnumerable<SelectListItem> EndTimeList { get; set; }

        public List<string> Users { get; set; }
    }
}

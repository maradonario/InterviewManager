using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewManager.Models
{
    public class EventObject
    {
        public string id { get; set; }

        public string title { get; set; }

        public bool allDay { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public string url { get; set; }

        public string className { get; set; }

        public bool editable { get; set; }

        public bool startEditable { get; set; }

        public bool durationEditable { get; set; }

        public string backgroundColor { get; set; }

    }
}

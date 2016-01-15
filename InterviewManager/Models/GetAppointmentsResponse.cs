﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewManager.Models
{
    /// <summary>
    /// GetAppointmentsResponse
    /// </summary>
    public class GetAppointmentsResponse
    {
        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        /// <value>
        /// The appointments.
        /// </value>
        public ICollection<Interview> Appointments { get; set; }
    }

    /// <summary>
    /// Interview
    /// </summary>
    public class Interview
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public DateTime Start { get; set; }

        /// <summary>
        /// Gets or sets the start view time.
        /// </summary>
        /// <value>
        /// The start view time.
        /// </value>
        public string StartViewTime { get; set; }
        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public DateTime End { get; set; }

        /// <summary>
        /// Gets or sets the end view time.
        /// </summary>
        /// <value>
        /// The end view time.
        /// </value>
        public string EndViewTime { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>
        /// The time zone.
        /// </value>
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the attendees.
        /// </summary>
        /// <value>
        /// The attendees.
        /// </value>
        public ICollection<RequiredAttendees> Attendees { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RequiredAttendees
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public string Response { get; set; }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewManager.Models
{
    public class SendEmailResponse
    {
        /// <summary>
        /// Gets or sets the recipients.
        /// </summary>
        /// <value>
        /// The recipients.
        /// </value>
        public ICollection<string> Recipients { get; set; }
    }
}

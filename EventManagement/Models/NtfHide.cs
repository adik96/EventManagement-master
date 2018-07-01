using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Models
{
    public class NtfHide
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
    }
}
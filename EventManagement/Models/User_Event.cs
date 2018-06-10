using EventsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Models
{
    public class User_Event
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        public virtual User User { get; set; }
        public virtual Event Event { get; set; }
    }
}
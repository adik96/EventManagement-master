using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsManagement.Models
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Stan")]
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
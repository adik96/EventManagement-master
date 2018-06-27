using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventsManagement.Models
{
    public class Event
    {
        public Event()
        {
            this.User_Event = new HashSet<User_Event>();
        }
        

        public int Id { get; set; }

        [Display(Name = "Tytuł")]
        [MaxLength(72)]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Miejsce organzacji wydarzenia")]
        [MaxLength(70)]
        public string Place { get; set; }

        [Display(Name = "Data organizacji")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Czas organizacji")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EventTime { get; set; }

        [Display(Name = "Data dodania")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EventAdd { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        [Display(Name = "Autor")]
        public User Author { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }

        [Display(Name = "Stan")]
        public State State { get; set; }
        
        [Display(Name = "Uczestnicy")]
        public virtual ICollection<User_Event> User_Event { get; set; }
        
    }
}
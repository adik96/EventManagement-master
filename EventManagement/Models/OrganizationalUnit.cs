using System.ComponentModel.DataAnnotations;

namespace EventsManagement.Models
{
    public class OrganizationalUnit
    {
        public int Id { get; set; }

        [Display(Name = "Jednostka organizacyjna")]
        public string Name { get; set; }
    }
}
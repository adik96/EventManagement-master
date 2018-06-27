using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using EventsManagement.Models;
using EventManagement.Models;

namespace EventsManagement.Models
{

    // You can add profile data for the user by adding more properties to your User class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public User()
        {
            this.User_Event = new HashSet<User_Event>();
        }

        public int? OrganizationalUnitId { get; set; }

        [Display(Name = "Dział")]
        public OrganizationalUnit OrganizationalUnit { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        public string PasswordHashed { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [InverseProperty("Author")]
        public ICollection<Event> CreateEvents { get; private set; }
        public ICollection<User_Event> User_Event { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #region dodatkowe pole notmapped
        [NotMapped]
        [Display(Name = "Pan/Pani:")]
        public string PelneNazwisko
        {
            get { return Name + " " + Surname; }
        }
        #endregion
    }
}
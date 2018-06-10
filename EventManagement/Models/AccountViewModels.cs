using EventsManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagement.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Kod")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Pamiętasz tę przeglądarkę?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Adres e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętać Cię?")]
        public bool RememberMe { get; set; }
    }
    /*
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Musisz wprowadzić imię")]
        [Display(Name = "Imię")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić nazwisko")]
        [Display(Name = "Nazwisko")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić hasło")]
        [StringLength(100, ErrorMessage = "Twoje hasło musi mieć conajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Wprowadzone hasła są różne.")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("OrganizationalUnit")]
        [Required(ErrorMessage = "Musisz wybrać dział z listy")]
        public int Id { get; set; }
        [Display(Name = "Dział")]
        public virtual OrganizationalUnit Name { get; set; }
    }*/

    public class RegisterEmployeeViewModel
    {
        [Required(ErrorMessage = "Musisz wprowadzić imię")]
        [Display(Name = "Imię")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić naziwsko")]
        [Display(Name = "Nazwisko")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić hasło")]
        [StringLength(100, ErrorMessage = "Twoje hasło musi mieć conajmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Wprowadzone hasła są różne.")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("OrganizationalUnit")]
        [Required(ErrorMessage = "Musisz wybrać zakład/katedrę z listy")]
        public int Id { get; set; }
        [Display(Name = "Dział")]
        public virtual OrganizationalUnit Name { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie są niezgodne.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }
    }
}

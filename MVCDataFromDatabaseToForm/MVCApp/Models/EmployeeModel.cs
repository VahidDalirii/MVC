using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class EmployeeModel
    {
        [Range(100000,999999,ErrorMessage ="You need to enter a valid Employee ID.")]
        [Display(Name ="Employee ID")]
        public int EmployeeId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage ="You need to give us your first name.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You need to give us your last name.")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "You need to give us your email address.")]
        public string EmailAddress { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name ="Confirm Email Address")]
        [Compare("EmailAddress",ErrorMessage ="The email and confirm email must match")]
        public string ConfirmEmailAddress { get; set; }

        [Display(Name ="Password")]
        [Required(ErrorMessage ="You must have a password")]
        [DataType(DataType.Password)]
        [StringLength(100,MinimumLength =8,ErrorMessage ="You need to provide a long enough password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "The password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
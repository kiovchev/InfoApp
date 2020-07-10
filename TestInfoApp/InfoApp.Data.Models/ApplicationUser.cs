using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Length can not be less then 2 and more then 200 symbols!")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 200, ErrorMessage = "Length can not be less then 2 and more then 200 symbols!")]
        public string LastName { get; set; }
    }
}

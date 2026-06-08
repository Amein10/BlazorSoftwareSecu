using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlazorSoftwareSecu.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(11, MinimumLength = 10)]
        public string CPR { get; set; } = string.Empty;
    }
}
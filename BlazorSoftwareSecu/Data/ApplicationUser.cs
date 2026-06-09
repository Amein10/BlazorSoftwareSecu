using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlazorSoftwareSecu.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string CPR { get; set; } = string.Empty;
    }
}
using System.ComponentModel.DataAnnotations;

namespace BlazorSoftwareSecu.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new();
    }
}
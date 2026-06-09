using BlazorSoftwareSecu.Data;

namespace BlazorSoftwareSecu.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int ActivityId { get; set; }
        public Activity Activity { get; set; } = null!;
    }
}
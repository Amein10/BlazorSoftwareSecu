using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlazorSoftwareSecu.Models;

namespace BlazorSoftwareSecu.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace HR_App.Models
{
    public class AppDBContext : DbContext
    {
        public DbSet<Applicants> applicants {get;set;}
        public DbSet<Attendance> attendance {get;set;}
        public DbSet<User> user {get;set;}
        public DbSet<Employees> employees {get;set;}
        public DbSet<LeaveRequest> leaveRequests { get; set; }
        public DbSet<Events> events { get; set; }

        public AppDBContext (DbContextOptions options):base(options)
        {
            
        }
    }
}
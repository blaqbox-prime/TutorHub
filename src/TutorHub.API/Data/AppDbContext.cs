using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TutorHub.API.Models;

namespace TutorHub.API.Data
{
    public class AppDbContext(DbContextOptions options): IdentityDbContext<AppUser>(options)
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
    }
}

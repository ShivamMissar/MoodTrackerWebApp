using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MoodTracker.Models
{
    public class DBcontext : IdentityDbContext<AppUser>
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options)
        { }


       
        public DbSet<MoodEntry>moodEntries { get; set; }
      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //For each mood entry it is associated to a single App User
            builder.Entity<MoodEntry>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            
           
        }
    }
}

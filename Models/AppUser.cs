using Microsoft.AspNetCore.Identity;

namespace MoodTracker.Models
{
    public class AppUser : IdentityUser
    {
        public String Firstname { get; set; }
        public String Lastname { get; set; }

        public int LoginStreak { get; set; }

        public DateTime  LastLoginDate { get; set; }


    }
}

using Microsoft.AspNetCore.Identity;

namespace MoodTracker.Models
{
    public class AppUser : IdentityUser
    {
        public String Firstname { get; set; }
        public String Lastname { get; set; }


    }
}

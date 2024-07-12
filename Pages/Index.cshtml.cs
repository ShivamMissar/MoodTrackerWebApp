using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoodTracker.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DBcontext _dbcontext;

        public IndexModel(UserManager<AppUser> userManager, DBcontext dbcontext)
        {
            _userManager = userManager;
            _dbcontext = dbcontext;
        }

        public List<MoodEntry> MoodEntries { get; set; } = new List<MoodEntry>();

        [BindProperty]
        public string UserName { get; set; }


        public int LoginStreak { get; set; }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if(user != null)
                {
                    UserName = user.Firstname;
                    LoginStreak = user.LoginStreak;
                   
                    
                }
                

            }
            else
            {
                // Handle if user is not authenticated (though this should be handled by [Authorize])
                return;
            }

            await GetUserLogs();
           
        }

        public string GetInsight(MoodEntry moodEntry, List<MoodEntry> userEntries)
        {
            string mood = moodEntry.MoodName.ToLower();
            var recentEntries = userEntries.OrderByDescending(m => m.EntryDate).Take(5);
            int moodStreak = recentEntries.Count(m => m.MoodName.ToLower() == mood);

            if (mood == "happy")
            {
                if (moodStreak > 3) return $"Great to see that you are really happy recently, {UserName}!";
                return "You're doing well! Keep embracing those positive moments.";
            }
            else if (mood == "sad")
            {
                if (moodStreak > 3) return $"{UserName}, you've been sad for a few days. Reflect on what's causing this sadness and take steps to address it.";
                return "It's okay to feel sad sometimes. Take some time for self-care and do things that make you feel better.";
            }
            else if (mood == "angry")
            {
                if (moodStreak > 3) return $"Hey {UserName}, noticed you've been angry for a few days now. What is causing these angry emotions?";
                return "Have some cold water, relax a bit, go for a walk.";
            }
            return "Keep track of your mood and try to understand the patterns. Regularly reviewing your mood entries can help you gain better insights.";
        }

        public string GetRecommendations(MoodEntry moodEntry, List<MoodEntry> userEntries)
        {
            string mood = moodEntry.MoodName.ToLower();
            var recentEntries = userEntries.OrderByDescending(m => m.EntryDate).Take(5);
            int moodStreak = recentEntries.Count(m => m.MoodName.ToLower() == mood);

            if (mood == "happy")
            {
                if (moodStreak > 3) return "Carry on with what you're doing. Great to see you smile :)";
                return "To get your mood even happier, keep participating in social activities, keep yourself busy.";
            }
            else if (mood == "sad")
            {
                if (moodStreak > 3) return $"Sorry to see you sad, {UserName}. Recommend changing your diet, eating food that makes you feel excited, changing your environment, and getting out more!";
                return "Try and get more sunlight, sit near the window when you're indoors, make your surroundings feel more comfortable and airy.";
            }
            else if (mood == "angry")
            {
                if (moodStreak > 3) return $"Seen that you've been angry for a while now, {UserName}. Try and relax your body, recommend trying mindful techniques like yoga, taking cold showers, talking to someone about your anger.";
                return "Recommend doing some physical activities to work off your anger, do something with your hands like art, spend time in a park and try bringing nature into your life.";
            }
            return "Keep track of your mood and try to understand the patterns. Regularly reviewing your mood entries can help you gain better insights.";
        }

        public async Task GetUserLogs()
        {
            var userId = _userManager.GetUserId(User);



            DateTime today = DateTime.Today;
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(4);

            // Retrieve mood entries for the current week
            MoodEntries = await _dbcontext.moodEntries
                .Where(entry => entry.UserId == userId && entry.EntryDate >= startOfWeek && entry.EntryDate <= endOfWeek)
                .ToListAsync();

            // Update insights and recommendations if they are null or empty
            foreach (var entry in MoodEntries)
            {
                if (string.IsNullOrEmpty(entry.mood_insight) || string.IsNullOrEmpty(entry.recommendation))
                {
                    entry.mood_insight = GetInsight(entry, MoodEntries);
                    entry.recommendation = GetRecommendations(entry, MoodEntries);
                    _dbcontext.Update(entry); // Update each entry individually
                }
            }

            await _dbcontext.SaveChangesAsync();
        }       
    }
}

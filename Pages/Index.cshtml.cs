using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;
using System.Collections.Generic;

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

        public List<MoodEntry> MoodEntries { get; set; } = [];
        public Dictionary<int, MoodAnalysis> MoodAnalysis { get; set; } = [];

     

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            DateTime startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);

            // Get the end of the current week (Friday)
            DateTime endOfWeek = startOfWeek.AddDays(4);

            MoodEntries = await _dbcontext.moodEntries
                .Where(entry => entry.UserId == userId && entry.EntryDate >= startOfWeek && entry.EntryDate <= endOfWeek)
                .ToListAsync();

            foreach(var entry in MoodEntries) 
            {
                var insights = GetInsight(entry,MoodEntries);
                var recommendations = GetRecommendations(entry,MoodEntries);


                MoodAnalysis[entry.MoodEntryId] = new MoodAnalysis
                {
                    mood_insight = insights,
                    recommendation = recommendations,
                    UserId = entry.UserId
               

                };

                _dbcontext.moodAnalysis.Add(MoodAnalysis[entry.MoodEntryId]);

            }
            await _dbcontext.SaveChangesAsync();

           

        }



        public string GetInsight(MoodEntry moodEntry, List<MoodEntry> userEntries) 
        {
            string mood = moodEntry.MoodName.ToLower();
            var get_userEntries = userEntries.OrderByDescending(m => m.EntryDate).Take(5);


            switch(mood) 
            {
                case "happy":
                    int h_count_streak = get_userEntries.Count(m => m.MoodName.ToLower() == "happy");
                    if(h_count_streak > 3) 
                    {
                        return "Great to see that you are really happy recently!";
                    }
                    return "You are doing great!";

                case "sad":
                    int s_count_streak = get_userEntries.Count(m => m.MoodName.ToLower() == "sad");
                    if (s_count_streak > 3)
                    {
                        return "You've been sad 3 days or more, think about what is making you sad. Avoid doing activies that make you sad.!";
                    }
                    return "Cheer up! Think about happy moments that get you to smile";

                case "angry":
                    int a_count_streak = get_userEntries.Count(m => m.MoodName.ToLower() == "angry");
                    if (a_count_streak > 3)
                    {
                        return "Hey, noticed you've been angry for a few days now. What is causing these angry emotions?";
                    }
                    return "Have some cold water, relax a bit, go for a walk";
                default:
                    return "Keep track of your mood and try to understand the patterns. Regularly reviewing your mood entries can help you gain better insights.";
            }

        }

        public string GetRecommendations(MoodEntry moodEntry, List<MoodEntry> userEntries)
        {
            var mood_name = moodEntry.MoodName.ToLower();
            var get_userEntries = userEntries.OrderByDescending(m => m.EntryDate).Take(5);


            switch (mood_name)
            {
                case "happy":
                    int h_count_streak = get_userEntries.Count(m => m.MoodName.ToLower() == "happy");
                    if (h_count_streak > 3)
                    {
                        return "Carry on with what you're doing. Great to see you smile :)";
                    }
                    return "To get your mood even happier, keep particpating in social activies, keep yourself busy";

                case "sad":
                    int s_count_streak = get_userEntries.Count(m => m.MoodName.ToLower() == "sad");
                    if (s_count_streak > 3)
                    {
                        return "Sorry to see you sad :C. Recommend change of diet, eat food that makes you feel excited, change of enviroment and get out more!";
                    }
                    return "Try and get more sunglight, sit near the window when you're indoors, make your surroundings feel more comfortable and more airy as possible";

                case "angry":
                    int a_count_streak = get_userEntries.Count(m => m.MoodName.ToLower() == "angry");
                    if (a_count_streak > 3)
                    {
                        return "Seen that you've been angry for a while now. Try and relax your body, recommend trying mindful techniques like yoga, take cold showers, talk to someone about your angry and open yoursel up";
                    }
                    return "Recommend doing some physical activies to work off your anger, do something your hands like art, spend time in a park and try bring nature into your life";
                default:
                    return "Keep track of your mood and try to understand the patterns. Regularly reviewing your mood entries can help you gain better insights.";
            }

        }
    }
}

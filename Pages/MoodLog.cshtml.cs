using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;

namespace MoodTracker.Pages
{
    public class MoodLogModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DBcontext _dbcontext;

        public MoodLogModel(UserManager<AppUser> userManager, DBcontext dbcontext)
        {
            _userManager = userManager;
            _dbcontext = dbcontext;
        }

        public List<MoodEntry> moodEntries { get; set; } = new List<MoodEntry>();
        public List<MoodAnalysis> MoodAnalysis { get; set; } = new List<MoodAnalysis>();

        public string userId { get; set; }

        public async Task OnGetAsync()
        {
            userId = _userManager.GetUserId(User);
            moodEntries = await _dbcontext.moodEntries.Where(entry => entry.UserId == userId).ToListAsync();
            MoodAnalysis = await _dbcontext.moodAnalysis.Where(entry => entry.UserId == userId).ToListAsync();



        }
    }
}

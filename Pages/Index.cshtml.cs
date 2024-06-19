using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;

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

        public List<MoodEntry> MoodEntries { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            DateTime startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);

            // Get the end of the current week (Friday)
            DateTime endOfWeek = startOfWeek.AddDays(4);

            MoodEntries = await _dbcontext.moodEntries
                .Where(entry => entry.UserId == userId && entry.EntryDate >= startOfWeek && entry.EntryDate <= endOfWeek)
                .ToListAsync();
        }
    }
}

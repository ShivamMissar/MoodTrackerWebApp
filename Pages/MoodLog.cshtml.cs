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

        public List<MoodEntry> MoodEntries { get; set; } = new List<MoodEntry>();
       

        public string userId { get; set; }

        [BindProperty]
        public DateOnly search { get; set; }



        public async Task OnGetAsync()
        {
            userId = _userManager.GetUserId(User);
            MoodEntries = await _dbcontext.moodEntries.Where(entry => entry.UserId == userId).ToListAsync();
           





        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            DateOnly searchDate = search; 
            userId = _userManager.GetUserId(User);
            MoodEntries = await _dbcontext.moodEntries.Where(entry => entry.User.Id == userId && DateOnly.FromDateTime(entry.EntryDate) == searchDate).ToListAsync(); 
            return Page();
        }
    }
}

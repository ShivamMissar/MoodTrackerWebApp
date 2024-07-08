using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;

namespace MoodTracker.Pages
{
    public class MoodFormModel : PageModel
    {
     

        private readonly UserManager<AppUser> _userManager;
        private readonly DBcontext _dbcontext;


        [BindProperty]
        public MoodEntry moodEntry { get; set; }

        public MoodFormModel(UserManager<AppUser> userManager, DBcontext dbcontext)
        {
            _userManager = userManager;
            _dbcontext = dbcontext;
            moodEntry = new MoodEntry();
         
        }

   

    

        [BindProperty]
        public string userId { get; set; }
        public void OnGet()
        {
            userId = _userManager.GetUserId(User);
        }

        public IActionResult OnPost()
        { 

            string finduser = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(finduser)) 
            {

                moodEntry.UserId = finduser;
                moodEntry.EntryDate = DateTime.Now;
                _dbcontext.Add(moodEntry);
                _dbcontext.SaveChanges();
            }
            return RedirectToPage("/Index");
        }



        

    }
}

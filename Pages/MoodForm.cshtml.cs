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

        public MoodFormModel(UserManager<AppUser> userManager, DBcontext dbcontext)
        {
            _userManager = userManager;
            _dbcontext = dbcontext;
         
        }

        [BindProperty]
        public Mood mood { get; set; }

    

        [BindProperty]
        public string userId { get; set; }
        public void OnGet()
        {
            userId = _userManager.GetUserId(User);
        }


        

    }
}

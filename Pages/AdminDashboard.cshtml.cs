using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;

namespace MoodTracker.Pages
{
    
    public class AdminDashboardModel : PageModel
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IEnumerable<IdentityRole> Roles { get; set; }

        public AdminDashboardModel(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _userManger = userManager;
            _roleManager = roleManager;
        }


       

        [BindProperty]
        public string new_role_name { get; set; }
        public async void OnGet()
        {
            Roles = await _roleManager.Roles.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _roleManager.CreateAsync(new IdentityRole(new_role_name.Trim()));
            return RedirectToPage("/AdminDashboard");
        }


    }
}

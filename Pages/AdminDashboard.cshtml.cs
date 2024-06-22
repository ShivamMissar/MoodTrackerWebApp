using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;

namespace MoodTracker.Pages
{
    
    public class AdminDashboardModel : PageModel
    {
        private const String PAGENAME = "/AdminDashboard";
        private readonly UserManager<AppUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IEnumerable<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
        public IEnumerable<AppUser> users { get; set; } = new List<AppUser>();

        public AdminDashboardModel(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _userManger = userManager;
            _roleManager = roleManager;
        }


        [BindProperty]
        public string new_role_name { get; set; }


        [BindProperty]
        public string new_password { get; set; }

        [BindProperty]
        public string new_email { get; set; }

        [BindProperty]
        public string new_user_role { get; set; }
        public async Task OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();
            users = await _userManger.Users.ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateNewRoleAsync()
        {
            await _roleManager.CreateAsync(new IdentityRole(new_role_name.Trim()));
            return RedirectToPage(PAGENAME);
        }

        public async Task<IActionResult> OnGetDeleteRoleAsync(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            await _roleManager.DeleteAsync(role);
            return RedirectToPage(PAGENAME);  
        }


    }
}

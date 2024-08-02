using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoodTracker.Models;
using System.ComponentModel.DataAnnotations;

namespace MoodTracker.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardModel : PageModel
    {
        
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
        public string NewPassword { get; set; }

        [BindProperty]
        [EmailAddress]
        public string NewEmail { get; set; }

        [BindProperty]
        public string NewUserRole { get; set; }
        public async Task OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();
            users = await _userManger.Users.ToListAsync();
        }

        public async Task<IActionResult> OnPostCreateNewRoleAsync()
        {
            var create_role = await _roleManager.CreateAsync(new IdentityRole(new_role_name.Trim()));
            if(create_role.Succeeded)
            {
                TempData["RoleCreated"] = "Role has been created successfully.";
            }
            return RedirectToPage("/AdminDashboard");
        }

        public async Task<IActionResult> OnGetDeleteRoleAsync(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            var result =  await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["RoleDeletedMessage"] = "Role has been removed successfully.";
            }
             

                

            return RedirectToPage("/AdminDashboard");  
        }

        public async Task<IActionResult> OnGetDeleteAsync(string Id)
        {
            var user = await _userManger.FindByIdAsync(Id);
            var result = await _userManger.DeleteAsync(user);

            if (result.Succeeded) 
            {
                TempData["UserDeletedMessage"] = "User has been removed successfully.";
            }
            return RedirectToPage("/AdminDashboard");
        }
        public async Task<IActionResult> OnPostUpdateEmailAsync(string Id)
        {
            var user = await _userManger.FindByIdAsync(Id);
            if(user != null)
            {
                user.UserName = NewEmail;
                user.Email = NewEmail;
            }
            
            var result = await _userManger.UpdateAsync(user);
            if(result.Succeeded) 
            {
                TempData["EmailUpdateMessage"] = "Email has been updated successfully.";
            }

            return RedirectToPage("/AdminDashboard");

        }

        public async Task<IActionResult> OnPostUpdatePasswordAsync(String Id)
        {
            var user = await _userManger.FindByIdAsync(Id);
            //This will remove the current existing password
            if(user != null) 
            {
                await _userManger.RemovePasswordAsync(user);
            }
            var result = await _userManger.AddPasswordAsync(user, NewPassword);
            if(result.Succeeded) 
            {
                TempData["PasswordUpdateMessage"] = "Password has been updated successfully.";
            }
            return RedirectToPage("/AdminDashboard");
        }


        public async Task<IActionResult> OnPostUpdateRoleAsync(string Id)
        {
            if (string.IsNullOrEmpty(NewUserRole))
            {
                TempData["RoleUpdateMessage"] = "Role cannot be empty.";
                return RedirectToPage("/Admindashboard");
            }

            var finduser = await _userManger.FindByIdAsync(Id);
            if (finduser == null)
            {
                TempData["RoleUpdateMessage"] = "User not found.";
                return RedirectToPage("/Admindashboard");
            }

            if (!await _roleManager.RoleExistsAsync(NewUserRole))
            {
                TempData["RoleNotExist"] = "The role you are trying to enter does not exist.";
                return RedirectToPage("/Admindashboard");
            }
            if (await _roleManager.RoleExistsAsync(NewUserRole))
            {
                TempData["RoleExistAlready"] = "The role you are trying to enter already exists.";
                return RedirectToPage("/Admindashboard");
            }

            var result = await _userManger.AddToRoleAsync(finduser, NewUserRole);
            if (result.Succeeded)
            {
                TempData["RoleUpdateMessage"] = "User role updated successfully.";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    TempData["RoleUpdateError"] += $"{error.Description} ";
                }
            }

            return RedirectToPage("/Admindashboard");
        }



    }
}

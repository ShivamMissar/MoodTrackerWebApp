using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MoodTracker.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoodTracker.Pages
{
    public class AccountModel : PageModel
    {

        private readonly UserManager<AppUser> _userManger;

        public AccountModel(UserManager<AppUser> userManger)
        {
            _userManger = userManger;
        }


        [BindProperty]
        public string Update_FName { get; set; }
        [BindProperty]
        public string Update_LName { get; set; }

        [BindProperty]
     
        public string Update_Email { get; set; }


        
        [BindProperty]
        [PasswordPropertyText]
        
        public string Update_Password { get; set; }

      
      
        public string UserId { get; set; }





        public async Task<IActionResult> OnGetAsync()
        {

            UserId = _userManger.GetUserId(User);
            var user = await _userManger.GetUserAsync(User);
            Update_FName = user.Firstname;
            Update_LName = user.Lastname;
            Update_Email = user.Email;


        
            return Page();
        }


        public async Task<IActionResult> OnPostUpdateNameAsync(string Id)
        {

            var user = await _userManger.FindByIdAsync(Id);
            
            
            if(user != null)
            {
                user.Firstname = Update_FName;
                user.Lastname = Update_LName;
            }
            var result = await _userManger.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["NameUpdateMessage"] = "Your name has been updated successfully.";
            }

            return RedirectToPage("/Account");
        }

        public async Task<IActionResult> OnPostUpdateEmailAsync(string Id)
        {
            var user = await _userManger.FindByIdAsync(Id);
            if (user != null)
            {
                user.Email = Update_Email;
            }

            var result = await _userManger.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["EmailUpdateMessage"] = "Your email has been updated successfully.";
            }

            return RedirectToPage("/Account");
        }

        public async Task<IActionResult> OnPostUpdatePasswordAsync(String Id)
        {
            var user = await _userManger.FindByIdAsync(Id);
            //This will remove the current existing password
            if (user != null)
            {
                await _userManger.RemovePasswordAsync(user);
            }
            var result = await _userManger.AddPasswordAsync(user, Update_Password);
            if (result.Succeeded)
            {
                TempData["PasswordUpdateMessage"] = "Password has been updated successfully.";

            }
            return RedirectToPage("/AdminDashboard");
        }


    }
}

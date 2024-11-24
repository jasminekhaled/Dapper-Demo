using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Dtos.UserDtos.RequestDto;
using RazorDemo.Helpers;
using RazorDemo.Models;
using RazorDemo.Services.IServices;

namespace RazorDemo.Pages.AuthPages
{
    public class SignUpModel : PageModel
    {
        private readonly IAuthServices _authServices;

        public SignUpModel(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [BindProperty]
        public AddUserDto SignupUser { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _authServices.SignUp(SignupUser);
            if(result.IsSuccess)
            {
                Response.Cookies.Append("JwtToken", result.Data);
                return RedirectToPage("/ArticlePages/Articles");
            }
            else
            {
                ErrorMessage = result.Message;
                return Page();
            }


            
            
        }
    }

}

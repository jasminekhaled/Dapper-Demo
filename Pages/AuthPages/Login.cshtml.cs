using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Dtos.UserDtos.RequestDto;
using RazorDemo.Helpers;
using RazorDemo.Models;
using RazorDemo.Services.IServices;

namespace RazorDemo.Pages.AuthPages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthServices _authServices;
        private readonly JwtService _jwtService;

        public LoginModel(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [BindProperty]
        public LoginDto loginUser { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _authServices.Login(loginUser);
            if (result.IsSuccess)
            {
                Response.Cookies.Append("JwtToken", result.Data.Token);
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

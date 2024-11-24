using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Dtos.UserDtos.RequestDto;
using RazorDemo.Helpers;
using RazorDemo.Services.IServices;

namespace RazorDemo.Pages.AuthPages
{
    public class ForgetPasswordModel : PageModel
    {
        private readonly IAuthServices _authServices;
        private readonly JwtService _jwtService;

        public ForgetPasswordModel(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [BindProperty]
        public UpdateUserDto User { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsDone { get; set; }

        public async Task OnPostAsync()
        {
            var result = await _authServices.ForgetPassword(User);
            if (result.IsSuccess)
            {
                IsDone = true;
            }
            else
            {
                ErrorMessage = result.Message;
                IsDone = false;
            }
            
            

        }
    }
}

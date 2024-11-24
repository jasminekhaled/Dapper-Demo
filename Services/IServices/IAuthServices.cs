using RazorDemo.Dtos;
using RazorDemo.Dtos.UserDtos.RequestDto;
using RazorDemo.Dtos.UserDtos.ResponseDto;
using RazorDemo.Models;

namespace RazorDemo.Services.IServices
{
    public interface IAuthServices
    {
        Task<UserResponseDto> GetByIdAsync(int id);
        Task<GeneralResponse<string>> SignUp(AddUserDto dto);
        Task<GeneralResponse<string>> ForgetPassword(UpdateUserDto dto);
        Task<GeneralResponse<LoginResponseDto>>  Login(LoginDto dto);
    }
}

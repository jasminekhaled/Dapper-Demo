using Dapper;
using Microsoft.Data.SqlClient;
using RazorDemo.Dtos;
using RazorDemo.Dtos.UserDtos.RequestDto;
using RazorDemo.Dtos.UserDtos.ResponseDto;
using RazorDemo.Helpers;
using RazorDemo.Models;
using RazorDemo.Services.IServices;
using System.Data;

namespace RazorDemo.Services.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IDbConnection _db;
        private readonly JwtService _jwtService;

        public AuthServices(IDbConnection db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }
        public async Task<GeneralResponse<string>> SignUp(AddUserDto dto)
        {
            try
            {
                var query = "INSERT INTO Users (UserName, Email, Password) VALUES (@UserName, @Email, @Password)";
                dto.Password = HashingService.GetHash(dto.Password);
                await _db.ExecuteAsync(query, dto);
                return new GeneralResponse<string>
                {
                    Message = "Operation is done successfully.",
                    IsSuccess = true,
                    Data = _jwtService.GenerateJwtToken(dto.UserName)
                };

            }
            catch (SqlException ex)  
            {
                return new GeneralResponse<string>
                {
                    Message = "Email or UserName is already exits.",
                    IsSuccess = false,
                    Error = ex
                };
            }

        }

        public async Task<UserResponseDto> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            var user = await _db.QuerySingleOrDefaultAsync<UserResponseDto>(query, new { Id = id });
            if (user == null)
            {
                throw new Exception($"No user found with ID {id}.");
            }
            return user;
        }

        public async Task<UserResponseDto> GetByUserNameAsync(string userName)
        {
            var query = "SELECT * FROM Users WHERE UserName = @UserName";
            var user = await _db.QuerySingleOrDefaultAsync<UserResponseDto>(query, new { UserName = userName });
            if (user == null)
            {
                throw new Exception($"No user found with UserName {userName}.");
            }
            return user;
        }

        public async Task<GeneralResponse<string>> ForgetPassword(UpdateUserDto dto)
        {
            try
            {
                if (dto.NewPassword != dto.ConfirmNewPassword)
                {
                    return new GeneralResponse<string>
                    {
                        Message = "the new password is different rom the confirmed one.",
                        IsSuccess = false,
                    };
                }

                
                var query = "SELECT * FROM Users WHERE UserName = @UserName and Password = @Password";
                var user = await _db.QuerySingleOrDefaultAsync<User>(query, new { UserName = dto.UserName, Password = HashingService.GetHash(dto.OldPassword) });
                if (user == null)
                {
                    return new GeneralResponse<string>
                    {
                        Message = "No user found with the same userName and old password.",
                        IsSuccess = false,
                    };
                }

                var updateUser = "UPDATE Users SET Password = @Password WHERE UserName = @UserName";
                await _db.ExecuteAsync(updateUser, new { UserName = dto.UserName, Password = HashingService.GetHash(dto.NewPassword) });
                return new GeneralResponse<string>
                {
                    IsSuccess = true,
                    Message = "Operation is done successfully"
                };
            }
            catch (SqlException ex)
            {
                return new GeneralResponse<string>
                {
                    Message = "Something went wrong",
                    IsSuccess = false,
                    Error = ex
                };
            }


        }

        public async Task<GeneralResponse<LoginResponseDto>> Login(LoginDto dto)
        {
            try
            {
                var password = HashingService.GetHash(dto.Password);
                var query = "SELECT * FROM Users WHERE UserName = @UserName and Password = @Password";
                var user = await _db.QuerySingleOrDefaultAsync<LoginResponseDto>(query,
                    new
                    {
                        UserName = dto.UserName,
                        Password = password
                    });

                if (user == null)
                {
                    return new GeneralResponse<LoginResponseDto>
                    {
                        Message = "No user found with this userName and password.",
                        IsSuccess = false,
                    };
                }

                var token = _jwtService.GenerateJwtToken(dto.UserName);
                user.Token = token;

                return new GeneralResponse<LoginResponseDto>
                {
                    IsSuccess = true,
                    Message = "Operation is done successfully",
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<LoginResponseDto>
                {
                    Message = "Something Went Wrong.",
                    IsSuccess = false,
                    Error = ex
                };
            }


        }
    }
}

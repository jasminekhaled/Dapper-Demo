using Dapper;
using Microsoft.Data.SqlClient;
using RazorDemo.Dtos;
using RazorDemo.Dtos.ArticleDtos.RequestDtos;
using RazorDemo.Dtos.UserDtos.ResponseDto;
using RazorDemo.Helpers;
using RazorDemo.Models;
using RazorDemo.Services.IServices;
using System.Data;
using System.Security.Claims;

namespace RazorDemo.Services.Services
{
    public class ArticleServices : IArticleServices
    {
        private readonly IDbConnection _db;
        private readonly JwtService _jwtService;
       private readonly IHttpContextAccessor _httpContextAccessor;
        public ArticleServices(IDbConnection db, JwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<GeneralResponse<string>> CreateArticle(ArticleRequestDto dto)
        {
            try
            {
                var userName = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var check = "SELECT Id FROM Users WHERE UserName = @UserName";
                dto.UserId = await _db.QuerySingleOrDefaultAsync<int>(check, new { UserName = userName });

                var query = "INSERT INTO Articles (Title, Body, UserId) VALUES (@Title, @Body, @UserId)";
                await _db.ExecuteAsync(query, dto);
                return new GeneralResponse<string>
                {
                    Message = "Operation is done successfully.",
                    IsSuccess = true
                };

            }
            catch (SqlException ex)
            {
                return new GeneralResponse<string>
                {
                    Message = "Something went wrong.",
                    IsSuccess = false,
                    Error = ex
                };
            }
        }

        public async Task<string> DeleteArticle(int id)
        {
            var query = "DELETE FROM Articles WHERE Id = @Id";
            await _db.ExecuteAsync(query, new { Id = id });
            return "Deleted Successfully";
        }

        public async Task<IEnumerable<ArticleResponseDto>> GetAllArticles()
        {
            var query = @"
                        SELECT a.Id, a.Title, a.Body, u.UserName
                        FROM Articles a
                        INNER JOIN Users u ON a.UserId = u.Id";

            return await _db.QueryAsync<ArticleResponseDto>(query);
            
        }

        public async Task<IEnumerable<ArticleResponseDto>> GetAllArticlesByUserId()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var id_Query = "SELECT Id FROM Users WHERE UserName = @UserName";
            var UserId = await _db.QuerySingleOrDefaultAsync<int>(id_Query, new { UserName = userName });

            var query = @"
                        SELECT a.Id, a.Title, a.Body, u.UserName
                        FROM Articles a
                        INNER JOIN Users u ON a.UserId = u.Id
                        WHERE a.UserId = @UserId";

            return await _db.QueryAsync<ArticleResponseDto>(query, new { UserId = UserId });
        }

        public async Task<ArticleResponseDto> GetArticleById(int id)
        {
            var query = "select * from Articles where Id = @id";
            return await _db.QuerySingleOrDefaultAsync<ArticleResponseDto>(query, new { Id = id });
        }

        public async Task<GeneralResponse<string>> UpdateArticle(int articleId, ArticleRequestDto dto)
        {
            try
            {
                var query = "SELECT * FROM Articles WHERE Id = @Id ";
                var article = await _db.QuerySingleOrDefaultAsync<ArticleResponseDto>(query, new { Id = articleId });
                
                var updateArticle = "UPDATE Articles SET Title = @Title, Body = @Body WHERE Id = @Id";
                await _db.ExecuteAsync(updateArticle, new { Title = dto.Title, Body = dto.Body, Id = articleId });
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
    }
}

using RazorDemo.Dtos;
using RazorDemo.Dtos.ArticleDtos.RequestDtos;

namespace RazorDemo.Services.IServices
{
    public interface IArticleServices
    {
        Task<GeneralResponse<string>> CreateArticle(ArticleRequestDto dto);
        Task<string> DeleteArticle(int id);
        Task<GeneralResponse<string>> UpdateArticle(int articleId, ArticleRequestDto dto);
        Task<IEnumerable<ArticleResponseDto>> GetAllArticles();
        Task<ArticleResponseDto> GetArticleById(int id);
        Task<IEnumerable<ArticleResponseDto>> GetAllArticlesByUserId();

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Dtos.ArticleDtos.RequestDtos;
using RazorDemo.Dtos.UserDtos.ResponseDto;
using RazorDemo.Services.IServices;

namespace RazorDemo.Pages.ArticlePages
{
    [Authorize]
    public class ArticlesModel : PageModel
    {
        private readonly IArticleServices _articleServices;

        public ArticlesModel(IArticleServices articleServices)
        {
            _articleServices = articleServices;
        }

        public IEnumerable<ArticleResponseDto> Articles { get; set; }
        public async Task OnGet()
        {
            Articles = await _articleServices.GetAllArticles();
        }
    }
}

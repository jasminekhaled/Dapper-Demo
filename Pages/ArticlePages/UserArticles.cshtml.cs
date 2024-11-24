using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Dtos.ArticleDtos.RequestDtos;
using RazorDemo.Services.IServices;

namespace RazorDemo.Pages.ArticlePages
{
    [Authorize]
    public class UserArticlesModel : PageModel
    {
        private readonly IArticleServices _articleServices;

        public UserArticlesModel(IArticleServices articleServices)
        {
            _articleServices = articleServices;
        }

        public IEnumerable<ArticleResponseDto> Articles { get; set; }
        public async Task OnGet()
        {
            Articles = await _articleServices.GetAllArticlesByUserId();
        }

        public async Task OnPostAsync(int id)
        {
            await _articleServices.DeleteArticle(id);
            
        }
    }

}

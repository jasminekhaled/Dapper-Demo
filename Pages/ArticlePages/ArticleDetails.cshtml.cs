using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Dtos.ArticleDtos.RequestDtos;
using RazorDemo.Services.IServices;

namespace RazorDemo.Pages.ArticlePages
{
    [Authorize]
    public class ArticleDetailsModel : PageModel
    {
        private readonly IArticleServices _articleServices;
        public ArticleDetailsModel(IArticleServices articleServices)
        {
            _articleServices = articleServices;
        }

        public ArticleResponseDto article { get; set; }
        public async Task OnGet(int id)
        {
             article = await _articleServices.GetArticleById(id);
           
        }
    }
}

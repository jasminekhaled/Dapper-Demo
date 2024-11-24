using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Services.IServices;

namespace RazorDemo.Pages.ArticlePages
{
    [Authorize]
    public class DeleteArticleModel : PageModel
    {
        private readonly IArticleServices _articleServices;

        public DeleteArticleModel(IArticleServices articleServices)
        {
            _articleServices = articleServices;
        }
        public async void OnGet(int id)
        {
            await _articleServices.DeleteArticle(id);
        }
    }
}

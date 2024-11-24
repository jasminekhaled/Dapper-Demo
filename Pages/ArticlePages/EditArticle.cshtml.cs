using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Dtos.ArticleDtos.RequestDtos;
using RazorDemo.Models;
using RazorDemo.Services.IServices;

namespace RazorDemo.Pages.ArticlePages
{
    [Authorize]
    public class EditArticleModel : PageModel
    {
        private readonly IArticleServices _articleServices;

        public EditArticleModel(IArticleServices articleServices)
        {
            _articleServices = articleServices;
        }

        [BindProperty]
        public ArticleRequestDto Article { get; set; }
        [BindProperty]
        public ArticleResponseDto oldArticle { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsDone { get; set; }
        public async Task OnGet(int id)
        {
            oldArticle = await _articleServices.GetArticleById(id);
        }

        public async Task OnPostAsync(int id)
        {
            var result = await _articleServices.UpdateArticle(id, Article);
            if (!result.IsSuccess)
            {
                ErrorMessage = result.Message;
                IsDone = false;
            }
            else
            {
                IsDone = true;
            }

        }
    }
}

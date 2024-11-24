using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDemo.Dtos.ArticleDtos.RequestDtos;
using RazorDemo.Dtos.UserDtos.RequestDto;
using RazorDemo.Services.IServices;

namespace RazorDemo.Pages.ArticlePages
{
    [Authorize]
    public class CreateArticleModel : PageModel
    {
        private readonly IArticleServices _articleServices;

        public CreateArticleModel(IArticleServices articleServices)
        {
            _articleServices = articleServices;
        }

        [BindProperty]
        public ArticleRequestDto Article { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsDone { get; set; }
        public async Task OnPostAsync()
        {
            var result = await _articleServices.CreateArticle(Article);
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core.CommentAgg.Contracts;
using Usta.Domain.Core.CommentAgg.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Usta.Presentation.RazorPages.Areas.Customer.Pages.Comments
{
    public class CreateCommentModel : PageModel
    {
        private readonly ICommentAppService _commentAppService;

        public CreateCommentModel(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }

        [BindProperty]
        public CommentInputDto NewComment { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }

        public void OnGet()
        {
            NewComment.OrderId = OrderId;
        }

        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _commentAppService.CreateComment(NewComment, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message!);
                return Page();
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToPage("/OrdersAndOffers/Index", new { area = "Customer" });
        }
    }
}
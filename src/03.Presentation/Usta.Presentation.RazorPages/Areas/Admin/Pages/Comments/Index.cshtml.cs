using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usta.Domain.Core._common;
using Usta.Domain.Core.CommentAgg.Contracts;
using Usta.Domain.Core.CommentAgg.Dtos;

namespace Usta.Presentation.RazorPages.Areas.Admin.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ICommentAppService _commentService;

        public IndexModel(ICommentAppService commentService)
        {
            _commentService = commentService;
        }

        public PagedResult<CommentDto> Comments { get; set; } = new PagedResult<CommentDto>();

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        private const int PageSize = 10;

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Comments = await _commentService.GetAllAsync(PageNumber, PageSize, cancellationToken);
        }

        public async Task<IActionResult> OnPostApproveAsync(int id, CancellationToken cancellationToken)
        {
            await _commentService.ApproveAsync(id, cancellationToken);
            return RedirectToPage(new { PageNumber });
        }

        public async Task<IActionResult> OnPostRejectAsync(int id, CancellationToken cancellationToken)
        {
            await _commentService.RejectAsync(id, cancellationToken);
            return RedirectToPage(new { PageNumber });
        }
    }
}
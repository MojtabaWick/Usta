using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Usta.Presentation.RazorPages.Extentions
{
    public class BasePageModel : PageModel
    {
        public int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                return userId;
            return null;
        }

        public bool IsExpert()
        {
            return User.Claims.Any(c =>
                c.Type == ClaimTypes.Role && c.Value == "Expert");
        }
    }
}
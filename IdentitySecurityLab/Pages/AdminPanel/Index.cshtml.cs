using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentitySecurityLab.Pages.AdminPanel;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}

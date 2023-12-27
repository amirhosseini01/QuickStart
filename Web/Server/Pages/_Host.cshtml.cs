using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Server.Pages;

public class IndexModel : PageModel
{

    public IndexModel()
    {
    }

    public string? CurrentUserName { get; set; }

    public void OnGet()
    {
        CurrentUserName = User.Identity!.Name;
    }
}
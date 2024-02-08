using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Server.Pages;

public class IndexModel : PageModel
{

    public IndexModel()
    {
    }

    public string? CurrentUserName { get; set; }

	public void OnGet() => this.CurrentUserName = this.User.Identity!.Name;
}
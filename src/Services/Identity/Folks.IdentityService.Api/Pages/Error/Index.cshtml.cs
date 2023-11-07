using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Duende.IdentityServer.Services;

using Folks.IdentityService.Api.Filters;

namespace Folks.IdentityService.Api.Pages.Error
{
    [AllowAnonymous]
    [SecurityHeaders]
    public class IndexModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IWebHostEnvironment _environment;

        public IndexModel(IIdentityServerInteractionService interaction, IWebHostEnvironment environment)
        {
            _interaction = interaction;
            _environment = environment;
        }

        public required ViewModel View { get; set; }

        public async Task OnGet(string errorId)
        {
            View = new ViewModel();

            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message is null)
            {
                return;
            }

            View.Error = message;
            if (!_environment.IsDevelopment())
            {
                message.ErrorDescription = null;
            }
        }
    }
}

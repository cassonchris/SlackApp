using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlackApp.Services;

namespace SlackApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Grant(string code)
        {
            if (await _authorizationService.GrantAsync(code))
            {
                return Ok("User authorization granted.");
            }
            else
            {
                return RedirectToAction("Index", "Install");
            }
        }
    }
}

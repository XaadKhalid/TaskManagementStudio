using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementStudio.Models;

namespace TaskManagementStudio.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            return claimsPrincipal.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if(user.Email == "user@email.com" && user.Password == "123")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            ViewData["ValidityMessage"] = "User Not Found...";
            return View();
        }
    }
}

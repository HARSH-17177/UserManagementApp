using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UserRoleManagementFrontend.Infrastructure;
using UserRoleManagementFrontend.Models;

namespace UserRoleManagementFrontend.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAuthenticateService _authService;
        public AccountsController(IAuthenticateService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new AuthenticationRequest();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var authResponse = await _authService.Authenticate(model);
            if (authResponse == null)
            {
                ModelState.AddModelError("", "Bad Username/Password.");
                return View(model);
            }
            HttpContext.Session.SetString("Token", authResponse.Token);
            HttpContext.Session.SetString("Name", authResponse.FullName);
            HttpContext.Session.SetInt32("UserId", authResponse.UserId);

            var user = await _authService.GetUserModel(authResponse.UserId, authResponse.Token);
            if (user != null)
            {
                var str = ConvertData.ObjectToJsonString(user);
                HttpContext.Session.SetString("User", str);
            }

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }


        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}

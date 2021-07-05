using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using zoeira.ModelViews;

namespace zoeira.Controllers
{
  public class LoginController : Controller
  {
    public IActionResult Index(string returnUrl)
    {
      TempData["returnUrl"] = returnUrl;

      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("Email, Password")] LoginMV body)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, body.Email),
        new Claim(ClaimTypes.Role, "Normal"),
      };

      var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

      // Auth Properties
      var authProperties = new AuthenticationProperties
      {
        RedirectUri = "/Login"
      };

      await HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(claimsIdentity),
        authProperties
      );

      if (TempData["returnUrl"] != null && Url.IsLocalUrl(TempData["returnUrl"] as string))
      {
        return Redirect(TempData["returnUrl"] as string);
      }

      return RedirectToAction("Index", "Dashboard");
    }

    [HttpPost]
    public async Task<IActionResult> Destroy()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

      return RedirectToAction(nameof(Index));
    }
  }
}
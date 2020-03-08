using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualHotel.Data.Services;
using VirtualHotel.Web.Models;

namespace VirtualHotel.Web.Controllers
{
    
    public class FrontDeskController : Controller
    {
        [AllowAnonymous]
        
        public IActionResult Welcome(string returnUrl = "/")
        {
            return View("Index",new LogInViewModel() { ReturnUrl = returnUrl});
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CheckIn(LogInViewModel model)
        {
            var user = AutheticationService.CheckPassword(model.Email, model.Password);
            if (user == null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("AuthorizedPersonType", user.AuthorizedPersonType.ToString()),
            };

            if(user.AuthorizedPersonType == Data.AuthorizedPersonType.Guest)
            {
                claims.Add(new Claim("RoomNumber", new Random().Next(10, 55).ToString()));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal
                , new AuthenticationProperties { IsPersistent = model.RememberLogin });

            return LocalRedirect(model.ReturnUrl);
        }

        
        [HttpPost]
        public async Task<IActionResult> CheckOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/");
        }
    }
}
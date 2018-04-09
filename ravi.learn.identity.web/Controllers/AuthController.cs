using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ravi.learn.identity.web.Models;
using ravi.learn.identity.domain.Services;
using ravi.learn.identity.domain.Entities;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ravi.learn.identity.web.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("signin")]
        public IActionResult SignIn()
        {
            //return View(new SignInModel());
            //return Challenge(new AuthenticationProperties { RedirectUri = "/" });
            return View();
        }

        [Route("signin/{provider}")]
        public IActionResult SignIn(string provider, string returnUrl = null)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);
        }

        /* First chapter
        [Route("signin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel signInModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                if (await _userService.ValidateCredentials(signInModel.UserName, signInModel.Password, out user))
                {
                    await SignInUser(user.UserName);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(signInModel);
        }
        */

        [Route("signup")]        
        public  IActionResult SignUp()
        {
            return View(new SignUpModel());
        }

        [Route("signup")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.AddUser(signUpModel.UserName, signUpModel.Password))
                {
                    await SignInUser(signUpModel.UserName);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Error", "Username exists. Please choose another one");
            }
            return View(signUpModel);
        }

        [Route("signout")]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userName),
                new Claim(ClaimTypes.Name, userName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, null);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);


        }
    }
}
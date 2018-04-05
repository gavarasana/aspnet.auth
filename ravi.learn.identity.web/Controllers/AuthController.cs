using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ravi.learn.identity.web.Models;
using ravi.learn.identity.domain.Services;
using ravi.learn.identity.domain.Entities;
using System;

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
            return View(new SignInModel());
        }

        [Route("signin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                if (await _userService.ValidateCredentials(signInModel.UserName, signInModel.Password, out user))
                {
                    await SignInUser(user.UserName);
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(signInModel);
        }

        private async Task SignInUser(string userName)
        {
            
        }
    }
}
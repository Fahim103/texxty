using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Texxty.Models;
using Texxty.Repository.Classes;

namespace Texxty.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserRepository userRepository;

        public AccountsController()
        {
            userRepository = new UserRepository();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user, string Password2)
        {
            if (ModelState.IsValid)
            {
                if(!ValidateRegister(user, Password2))
                {
                    return View();
                }

                // TODO: Insert to DB
                user.ActiveStatus = false; // Will set it to true when email is verified
                userRepository.Insert(user)

                // TODO: Send email to user


                // Set success message
                TempData["RegistrationMessage"] = "Account created. Please verify your email to activate your account.";

            }
            TempData["RegistrationMessage"] = "Error. Please check your information again";
            return View();
        }

        [NonAction]
        public bool ValidateRegister(User user, string Password2)
        {
            if (user.Password != Password2)
            {
                TempData["RegistrationMessage"] = "Password doesn't match. Please try again";
                return false;
            }
            if (userRepository.CheckUsernameAvailable(user))
            {
                if (userRepository.CheckEmailAvailable(user))
                {
                    return true;
                }
                else
                {
                    TempData["RegistrationMessage"] = "Account exists with this email";
                    return false;
                }
            }
            else
            {
                TempData["RegistrationMessage"] = "Username not available";
                return false;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}
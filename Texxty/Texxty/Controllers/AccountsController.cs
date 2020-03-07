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
        private readonly TexxtyDBEntities context;

        public AccountsController()
        {
            userRepository = new UserRepository();
            context = new TexxtyDBEntities();
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

                user.ActiveStatus = false; // Will set it to true when email is verified
                userRepository.Insert(user);

                // TODO: Send email to user

                // Set success message
                TempData["RegistrationMessage"] = "Account created. Please verify your email to activate your account.";
                return RedirectToAction("Login");

            }
            TempData["RegistrationMessage"] = "Error. Please check your information again";
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            // If ModelState is not valid
            if (!ModelState.IsValid)
                return View();

            // Model State Valid
            try
            {
                var logged_user = context.Users
                   .Where(x => x.Username == user.Username && x.Password == user.Password)
                   .FirstOrDefault();

                // If credentials are correct
                if (logged_user != null)
                {
                    // If account is active
                    if(logged_user.ActiveStatus == true)
                    {
                        Session["username"] = logged_user.Username;
                        Session["user_id"] = logged_user.UserID;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Your account is not active. Please verify your email.";
                        return View();
                    }
                    
                }
                else
                {
                    ViewBag.Message = "Incorrect credentials.";
                    return View();
                }
            }
            catch
            {
                ViewBag.Message = "Failed to log in.";
                return View();
            }
        }



        [NonAction]
        public bool ValidateRegister(User user, string Password2)
        {
            if (user.Password != Password2)
            {
                TempData["RegistrationMessage"] = "Password doesn't match. Please try again";
                return false;
            }
            
            if (userRepository.CheckEmailAvailable(user))
            {
                
                if (userRepository.CheckUsernameAvailable(user))
                {
                    return true;
                }
                else
                {
                    TempData["RegistrationMessage"] = "Username not available";
                    return false;
                }
            }
            else
            {
                TempData["RegistrationMessage"] = "Account exists with this email";
                return false;
            }
        }
    }
}
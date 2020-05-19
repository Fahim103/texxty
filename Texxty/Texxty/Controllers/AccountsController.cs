using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Texxty.Models;
using Texxty.Repository.Classes;


namespace Texxty.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserRepository userRepository;
        private readonly TexxtyDBEntitiesMVC context;

        public AccountsController()
        {
            userRepository = new UserRepository();
            context = new TexxtyDBEntitiesMVC();
        }

        [NonAction]
        public bool AuthorizeUser()
        {
            return Session["user_id"] != null;
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

                user.ActiveStatus = true;
                userRepository.Insert(user);


                // Set success message
                TempData["RegistrationMessage"] = "Account created. Please login to continue using the site.";
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
                    Session["username"] = logged_user.Username;
                    Session["user_id"] = logged_user.UserID;
                    return RedirectToAction("Feed", "Home");
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

        public ActionResult Logout()
        {
            Session["user_id"] = null;
            Session["username"] = null;
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login");

            var user = userRepository.Get(id);
            user.Password = string.Empty;
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
                return View(user);
            try
            {
                var dbUser = context.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                if (dbUser.Email != user.Email)
                {
                    if (!userRepository.CheckEmailAvailable(user))
                    {
                        ViewBag.Message = "Email not available";
                        dbUser.Email = string.Empty;
                        return View(dbUser);
                    }
                }
                user.Password = dbUser.Password;
                user.ActiveStatus = dbUser.ActiveStatus;
                userRepository.Update(user);
                TempData["Message"] = "Information Updated";
                return RedirectToAction("Details", new { id = user.UserID });

            }
            catch
            {
                return View(user);
            }
            
        }


        [HttpGet]
        public ActionResult Details(int id)
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login");

            var user = userRepository.Get(id);
            user.Password = string.Empty;
            return View(user);
        }


        [HttpGet]
        public ActionResult ChangePassword(int id)
        {
            if (!AuthorizeUser())
                return RedirectToAction("Login");

            var user = userRepository.Get(id);
            user.Password = string.Empty;
            return View(user);
        }

        [HttpPost]
        public ActionResult ChangePassword(User user, string NewPassword, string NewPasswordRetype)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(NewPasswordRetype))
            {
                ViewBag.Message = "Please fill out all fields";
                return View(user);
            }
            else if (!NewPassword.Equals(NewPasswordRetype))
            {
                ViewBag.Message = "New passwords fields doesn't match";
                return View(user);
            }
            else
            {
                try
                {
                    var dbUser = context.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
                    if (dbUser.Password != user.Password)
                    {
                        ViewBag.Message = "Current password value didn't match";
                        dbUser.Password = string.Empty;
                        return View(dbUser);
                    }

                    dbUser.Password = NewPassword;
                    userRepository.Update(dbUser);
                    TempData["Message"] = "Information Updated";
                    return RedirectToAction("Details", new { id = user.UserID });

                }
                catch
                {
                    return View(user);
                }
            }

        }

        [NonAction]
        public bool ValidateRegister(User user, string Password2)
        {
            Regex namePattern = new Regex("^[a-z A-Z]*$");
            bool pattern = true;
            if (!namePattern.IsMatch(user.FullName))
            {
                ModelState.AddModelError("FullName", "Name can only contain alphabets, must have upper case and lower case alphabet");
                pattern = false;
            }

            Regex usernamePattern = new Regex(@"^(?=.*[a-z])|(?=.*[A-Z])");
            if (!usernamePattern.IsMatch(user.Username))
            {
                ModelState.AddModelError("Username", "Username must contain upper and lower case Alphabet and Numbers");
                pattern = false;
            }

            if (user.Password.Length < 8)
            {
                ModelState.AddModelError("Password", "Password length must be at least 8 character long");
                pattern = false;
            }
            if (user.Password != Password2)
            {
                TempData["RegistrationMessage"] = "Password doesn't match. Please try again";
                pattern = false;
            }

            if (!pattern)
                return pattern;

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
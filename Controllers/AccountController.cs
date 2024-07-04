using CentralisationV0.App_Start.IdentityConfig;
using CentralisationV0.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CentralisationV0.Controllers
{


    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        // GET: Account

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void EnsureRoleExists(string roleName)
        {
            using (var context = new CentralisationContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                if (!roleManager.RoleExists(roleName))
                {
                    var role = new IdentityRole(roleName);
                    roleManager.Create(role);
                }
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        

        public ActionResult indexUser()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Parametre()
        {
            var model = new ParametreViewModel
            {
                RegisterViewModel = new RegisterViewModel(), // Crée une instance de RegisterViewModel
                Users = UserManager.Users.ToList() // Récupère tous les utilisateurs
            };

            return View(model);
        }


        public ActionResult ParametreUser()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(ParametreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registerModel = model.RegisterViewModel;
                var user = new ApplicationUser
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    Nom = registerModel.Nom,
                    Prenom = registerModel.Prenom,
                    DateNaissance = registerModel.DateNaissance,
                    Address = registerModel.Address,
                    Proffession = registerModel.Proffession,
                    isActivated=true       
                };

                var result = await UserManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    EnsureRoleExists("User"); // Ensure the "User" role exists
                    await UserManager.AddToRoleAsync(user.Id, "User"); // Or "Admin" if creating admin users
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ActivateAccount(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.isActivated = true;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false, message = "Erreur d'activation du compte" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeactivateAccount(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.isActivated = false;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false, message = "Erreur de désactivation du compte." });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Return errors to the client if the model is invalid
                return Json(new { success = false, message = "Invalid data", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList() });
            }

            var user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user == null)
            {
                return Json(new { success = false, message = "Utilisateur n'éxiste pas " });
            }

            user.Nom = model.Nom;
            user.Prenom = model.Prenom;

            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.CurrentPassword) && !string.IsNullOrEmpty(model.Password))
                {
                    var passwordChangeResult = await UserManager.ChangePasswordAsync(user.Id, model.CurrentPassword, model.Password);
                    if (!passwordChangeResult.Succeeded)
                    {
                        return Json(new { success = false, message = string.Join(", ", passwordChangeResult.Errors) });
                    }
                }
                return Json(new { success = true });
            }

            return Json(new { success = true, message = "Informations mises à jour avec succès." });
        }


        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return Json(new { success = false, message = "Utilisateur non trouvé." });
                }

                if (!string.IsNullOrEmpty(user.PasswordResetCode))
                {
                    // Si un code a déjà été envoyé, retournez un message approprié
                    return Json(new { success = false, message = "Le code de réinitialisation a déjà été envoyé." });
                }

                try
                {
                    // Générer un code numérique aléatoire
                    Random random = new Random();
                    string code = random.Next(100000, 999999).ToString();

                    // Enregistrer le code sur l'utilisateur
                    user.PasswordResetCode = code;

                    if (code != null && user.PasswordResetCode != null)
                    {
                        var updateResult = await UserManager.UpdateAsync(user);

                        if (!updateResult.Succeeded)
                        {
                            return Json(new { success = false, message = "Échec de la mise à jour de l'utilisateur avec le code de réinitialisation." });
                        }

                        // Envoyer le code à l'e-mail de l'utilisateur
                        var senderEmail = new MailAddress("emm31640@gmail.com", "gmail");
                        var password = "elrmfxgsmymtpipy";
                        var receiverEmail = new MailAddress(user.Email, "User");
                        var subject = "Réinitialisation du mot de passe";
                        var body = $"Merci d'insérer le code de vérification dans le champ de mot de passe et de créer un nouveau mot de passe. Votre code de vérification est : {code}";

                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };

                        using (var message = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = subject,
                            Body = body
                        })
                        {
                            await smtp.SendMailAsync(message);
                        }
                    }
                    

                   

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    // Logguer l'exception
                    // Par exemple, utiliser un logger :
                    // _logger.LogError(ex, "Erreur lors de l'envoi de l'e-mail de code de réinitialisation.");
                    return Json(new { success = false, message = "Échec de l'envoi de l'e-mail de code de réinitialisation." });
                }
            }

            return Json(new { success = false, message = "Requête invalide." });
        }







        [HttpGet]
        [AllowAnonymous]
        public ActionResult VerifyCode(string email)
        {
            return View(new VerifyCodeViewModel { Email = email });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid verification code." });
            }

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null || user.PasswordResetCode != model.VerificationCode)
            {
                return Json(new { success = false, message = "Invalid verification code." });
            }

            return Json(new { success = true });
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string userId)
        {
            var model = new ResetPasswordViewModel { UserId = userId };
            return View(model);
        }

       





        public ActionResult Login()
        {
            var model = new LoginViewModel(); // Create an instance of LoginViewModel
            return View(model); // Pass the model to the view
        }

       

[HttpPost]
[AllowAnonymous]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Login(LoginViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var user = await UserManager.FindByEmailAsync(model.Email);

    if (user != null && user.IsReseted)
    {
        var passwordHasher = new PasswordHasher();
        var verificationResult = passwordHasher.VerifyHashedPassword(user.PasswordHash, model.Password);

        if (verificationResult == PasswordVerificationResult.Success)
        {
            return RedirectToAction("ResetPassword", new { userId = user.Id });
        }
        else
        {
            ModelState.AddModelError("", "Invalid login.");
            return View(model);
        }
    }

    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, shouldLockout: false);
    switch (result)
    {
        case SignInStatus.Success:
            if (user != null)
            {
                 if (!user.isActivated) {
                    ModelState.AddModelError("", "Votre compte est désactivé. Veuillez contacter l'administrateur.");
                    return View(model);
                 }
                if (await UserManager.IsInRoleAsync(user.Id, "Admin"))
                {
                    return RedirectToAction("Index", "Account");
                }
                else if (await UserManager.IsInRoleAsync(user.Id, "User"))
                {
                    return RedirectToAction("indexUser", "Account");
                }
            }

            ModelState.AddModelError("", "Rôle d'utilisateur non reconnu.");
            return View(model);

        case SignInStatus.LockedOut:
            return View("Lockout");
        case SignInStatus.RequiresVerification:
            return RedirectToAction("SendCode", new { ReturnUrl = "/", RememberMe = false });
        case SignInStatus.Failure:
        default:
            ModelState.AddModelError("", "Tentative de connexion non valide.");
            return View(model);
    }
}





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }


        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
       




        // POST: Account/Create
      

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

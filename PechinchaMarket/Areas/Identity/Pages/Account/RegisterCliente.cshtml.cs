// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;
using Microsoft.AspNet.Identity;
using PechinchaMarket.Services;

namespace PechinchaMarket.Areas.Identity.Pages.Account
{
    public class RegisterClienteModel : PageModel
    {
        private readonly SignInManager<PechinchaMarketUser> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;
        private readonly Microsoft.AspNetCore.Identity.IUserStore<PechinchaMarketUser> _userStore;
        private readonly Microsoft.AspNetCore.Identity.IUserEmailStore<PechinchaMarketUser> _emailStore;
        private readonly ILogger<RegisterClienteModel> _logger;
        // private readonly IEmailSender _emailSender;
        private readonly DBPechinchaMarketContext _context;


        public RegisterClienteModel(
            Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager,
            Microsoft.AspNetCore.Identity.IUserStore<PechinchaMarketUser> userStore,
            SignInManager<PechinchaMarketUser> signInManager,
            ILogger<RegisterClienteModel> logger,
            IEmailSender emailSender,
            DBPechinchaMarketContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (Microsoft.AspNetCore.Identity.IUserEmailStore<PechinchaMarketUser>)GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public List<Categoria> SelectedCategories { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nome")]
            public string UserName { get; set; }


            [DataType(DataType.Text)]
            [Display(Name = "localizacao")]
            public string Localizacao { get; set; }


            [Display(Name = "Preferencias")]
            public List<Categoria> SelectedCategories { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                Input.SelectedCategories = Request.Form["categorias"].Select(c => Enum.Parse<Categoria>(c)).ToList();
               
                
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {

                   
                    await _userManager.AddToRoleAsync(user, "Cliente");
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    Cliente cliente = new Cliente()
                    {
                        UserId = userId,
                        Preferencias = Input.SelectedCategories,
                        Localizacao = Input.Localizacao,
                        Name= Input.UserName

                    };

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl, context = _context },
                        protocol: Request.Scheme);

                    EmailSender emailSenderService = new EmailSender(_context);

                    emailSenderService.SendEmail("Confirme seu email",Input.Email,Input.UserName,
                        $"Por favor confirme o seu registo no PechinchaMarket {HtmlEncoder.Default.Encode(callbackUrl)}").Wait();

                    /* await SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");*/

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        _context.Add(cliente);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });

                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        /// <summary>
        /// Enviar email ao utilizador que se registou
        /// </summary>
        /// <param name="email"></param> email do utilizador
        /// <param name="subject"></param> assunto do email
        /// <param name="confirmLink"></param> mensagem com o link de confirmação
        

        /*
        private async Task<bool> SendEmailAsync(string email, string subject, string confirmLink)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                message.From = new MailAddress("pechinchamarket@outlook.com");
                message.To.Add(email);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = confirmLink;

                smtpClient.Port = 587;
                smtpClient.Host = "smtp-mail.outlook.com";


                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("pechinchamarket@outlook.com", "Pechinchamos");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.Send(message);
                await smtpClient.SendMailAsync(message);
              
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }*/

        private PechinchaMarketUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<PechinchaMarketUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(PechinchaMarketUser)}'. " +
                    $"Ensure that '{nameof(PechinchaMarketUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private Microsoft.AspNetCore.Identity.IUserEmailStore<PechinchaMarketUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (Microsoft.AspNetCore.Identity.IUserEmailStore<PechinchaMarketUser>)_userStore;
        }

        
    }
}

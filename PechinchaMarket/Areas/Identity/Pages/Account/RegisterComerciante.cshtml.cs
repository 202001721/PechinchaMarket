﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;
using Microsoft.AspNet.Identity;

namespace PechinchaMarket.Areas.Identity.Pages.Account
{
    public class RegisterComercianteModel : PageModel
    {
        private readonly SignInManager<PechinchaMarketUser> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;
        private readonly Microsoft.AspNetCore.Identity.IUserStore<PechinchaMarketUser> _userStore;
        private readonly Microsoft.AspNetCore.Identity.IUserEmailStore<PechinchaMarketUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly DBPechinchaMarketContext _context;
        private readonly IWebHostEnvironment _environment;


        public RegisterComercianteModel(
            Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager,
            Microsoft.AspNetCore.Identity.IUserStore<PechinchaMarketUser> userStore,
            SignInManager<PechinchaMarketUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            DBPechinchaMarketContext context,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _environment = environment;
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

            [Required]
            [Display(Name = "Contacto")]
            public int Contact { get; set; }


            [Required]
            [Display(Name = "Imagem")]
            public IFormFile Image { get; set; }

            [Required]
            [Display(Name = "Documento")]
            public IFormFile Document { get; set; }
            
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

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                     await _userManager.AddToRoleAsync(user, "Comerciante");
                    var userId = await _userManager.GetUserIdAsync(user);

                    var memoryStreamImg = new MemoryStream();
                    var memoryStreamDoc = new MemoryStream();
                    
                    await Input.Image.CopyToAsync(memoryStreamImg);
                    await Input.Document.CopyToAsync(memoryStreamDoc);

                    Comerciante comerciante = new Comerciante()
                    {
                        UserId = userId,
                        contact = Input.Contact,
                        logo = memoryStreamImg.ToArray(),
                        document = memoryStreamDoc.ToArray(),
                        isApproved = false,
                        Name= Input.UserName

                    };
                    
                    _context.Add(comerciante);
                    user.EmailConfirmed = false;
                    await _context.SaveChangesAsync();
                    

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/WaitForConfirmation",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                  

                    //await SendEmailAsync(Input.Email, "Confirm your email",
                    //  $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        
                        return RedirectToPage("WaitForConfirmation", new { email = Input.Email, returnUrl = returnUrl });
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
        private async Task<bool> SendEmailAsync(string email, string subject, string confirmLink)
        {

            //TODO
            //INSERT YOUR OWN MAIL SERVER CREDENTIALS
            // message.From = ?
            // message.Port = ?
            // message.Host = ?
            // smtpClient.Credentials = new NetworkCredential(?Username,?Password);
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
                smtpClient.Credentials = new NetworkCredential("pechinchamarket@outlook.com", "Pechinchamos"); // verificar a extensao que esta usando 
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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

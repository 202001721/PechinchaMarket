﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PechinchaMarket.Areas.Identity.Data;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.AspNet.Identity;
using PechinchaMarket.Models;

namespace PechinchaMarket.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<PechinchaMarketUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly DBPechinchaMarketContext _context;
        private Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;

        public LoginModel(SignInManager<PechinchaMarketUser> signInManager, ILogger<LoginModel> logger, DBPechinchaMarketContext context, Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
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
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

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
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
     
            

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
              

                var ComercialEmails = from c in _context.Comerciante join u in _context.Users on c.UserId equals u.Id select u.Email;
                var ClientsEmails =  from c in _context.Cliente join u in _context.Users on c.UserId equals u.Id select u.Email;
                if (ComercialEmails.Contains(Input.Email)) //if who is logging in is a Comercial Account
                {
                    var isApproved = from c in _context.Comerciante 
                                     join u in _context.Users 
                                     on c.UserId equals u.Id 
                                     where u.Email == Input.Email 
                                     select c.isApproved;

                    if (isApproved.First() == true)
                    {
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("Utilizador logado");
                            return LocalRedirect(Url.Content("~/Produtos"));
                        }
                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                        }
                        if (result.IsLockedOut)
                        {
                            _logger.LogWarning("A sua conta está bloqueada.");
                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Tentativa de login inválida");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "O utilizador ainda não foi aprovado");
                        return Page();
                    }

                } else if (Input.Email.Equals("admin@gmail.com"))
                {
                    if (result.Succeeded)
                    {

                        _logger.LogInformation("Utilizador logado.");
                        return LocalRedirect("~/Manager/NonAprovedProducts");
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("Conta bloqueada");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email ou senha incorretos");
                        return Page();
                    }
                }
                else
                {
                    if (result.Succeeded)
                    {

                        _logger.LogInformation("Utilizador logado.");
                        return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("Conta bloqueada");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email ou senha incorretos");
                        return Page();
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }


    }
}

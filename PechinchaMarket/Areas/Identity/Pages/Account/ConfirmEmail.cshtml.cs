﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;

namespace PechinchaMarket.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<PechinchaMarketUser> _userManager;

        public string ReturnUrl { get; set; }
        private DBPechinchaMarketContext _context;

        public ConfirmEmailModel(UserManager<PechinchaMarketUser> userManager, DBPechinchaMarketContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
           /* if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }*/
           if(userId == null) { return RedirectToPage("/Index"); }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            // code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            // var result = await _userManager.ConfirmEmailAsync(user, code);
            user.EmailConfirmed = true;
            _context.SaveChanges();
            StatusMessage = user.EmailConfirmed ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}

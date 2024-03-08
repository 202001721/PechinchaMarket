// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Packaging.Signing;
using PechinchaMarket.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNet.Identity;

namespace PechinchaMarket.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;
        private readonly SignInManager<PechinchaMarketUser> _signInManager;
        private readonly DBPechinchaMarketContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager,
            SignInManager<PechinchaMarketUser> signInManager,
            DBPechinchaMarketContext context,
            IWebHostEnvironment webHostEnvironment,
            ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /**
         * 1 - Categoria.Enlatados.ToString(),
         * 2 - Categoria.Frescos.ToString(),
         * 3 -  Categoria.Biologicos.ToString(),
         * 4 -  Categoria.Congelados.ToString(),
         * 5 -  Categoria.Pastelaria.ToString(),
         * 6 -  Categoria.Talho.ToString(),
         * 7 -  Categoria.Peixaria.ToString(),
         * 8 -  Categoria.Charcutaria.ToString(),
         * 9 -  Categoria.Bebidas.ToString(),
         * 10 -  Categoria.Vegan.ToString(),
         * 11 -  Categoria.Doces.ToString(),
         * 12 -  Categoria.Snacks.ToString(),
         * 13 -  Categoria.BebidasAlcoólicas.ToString()
         */
        public string[] PreferencesLabels { get; set; } = Enum.GetNames(typeof(Categoria));


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///  Image that represents the user of type Comerciante
            /// </summary>

            [Required]
            [Display(Name ="Logo")]
            public IFormFile Logo { get; set; }



            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nome")]
            public string UserName { get; set; }


            /// <summary>
            ///     
            /// </summary>
            [Required]
            [Display(Name = "Contacto")]
            public int UserPhone { get; set;}

            [Required]
            [Display(Name = "Localização")]
            public string Location { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Novo email")]
            public string NewEmail { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password atual")]
            public string OldPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "A {0} deve ser pelo menos {2} e no maximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nova password")]
            public string NewPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirme nova password")]
            [Compare("NewPassword", ErrorMessage = "A nova password e a password de confirmação não são iguais.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name ="Selecione uma foto")]
            public string NewPhoto { get; set; }

            
            [Display(Name = "Preferências")]
            public bool[] Preferences { get; set; }

            
        }

        private async Task LoadAsync(PechinchaMarketUser user)
        {
            var userId = _userManager.GetUserId(User);
            var userName = "";

            var userPhoto = 0;
            var userLocation = "";
            bool[] userPreferences = Enumerable.Repeat(false, Enum.GetNames(typeof(Categoria)).Count()).ToArray(); ;
            var userPhone = 0;

            if (user != null)
            {
                var cliente = _context.Cliente.Where(x => x.UserId == _userManager.GetUserId(User)).ToList();

                if (User.IsInRole("Comerciante"))
                {
                    ViewData["UserPhoto"] = ShowImage(
                        _context.Comerciante.Where(x => x.UserId == _userManager.GetUserId(User)).Select(x => x.logo).FirstOrDefault());

                    userName = _context.Comerciante.Where(x => x.UserId.Equals(userId)).Select(x => x.Name).FirstOrDefault();
                    userPhone = _context.Comerciante.Where(x => x.UserId.Equals(userId)).Select(x => x.contact).FirstOrDefault();


                    var email = await _userManager.GetEmailAsync(user);
                    Email = email;

                    Input = new InputModel
                    {
                        UserName = userName,
                        UserPhone = userPhone,
                        Preferences = userPreferences,
                    };
                }
                else if (User.IsInRole("Cliente"))
                {
                    var userPhotoContext = _context.Cliente.Where(x => x.UserId == _userManager.GetUserId(User)).Select(x => x.Image).FirstOrDefault();
                    
                    if (userPhotoContext.IsNullOrEmpty()) {
                        var defaultpath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "userphoto_0.png");
                        byte[] imageData2;
                        using (var stream = new FileStream(defaultpath, FileMode.Open))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await stream.CopyToAsync(memoryStream);
                                imageData2 = memoryStream.ToArray();
                            }
                        }
                        userPhotoContext = imageData2;
                    }
                    ViewData["UserPhoto"] = ShowImage(userPhotoContext);

                    userName = _context.Cliente.Where(x => x.UserId.Equals(userId)).Select(x => x.Name).FirstOrDefault();
                    userLocation = _context.Cliente.Where(x => x.UserId.Equals(userId)).Select(x => x.Localizacao).FirstOrDefault();

                    for (int i = 0; i < 17; i++) {
                        var webRootPath = _webHostEnvironment.WebRootPath;
                        var choosedPhotoPath = Path.Combine(webRootPath, "images", $"userphoto_{i}.png");

                        byte[] imageData;
                        using (var stream = new FileStream(choosedPhotoPath, FileMode.Open))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await stream.CopyToAsync(memoryStream);
                                imageData = memoryStream.ToArray();
                            }
                        }

                        if (CompareBytes(imageData, userPhotoContext)) {
                            userPhoto = i;
                            break;
                        }
                    }

                    var userPreferencesContext = _context.Cliente.Where(x => x.UserId.Equals(userId)).Select(x => x.Preferencias).FirstOrDefault().ToArray();
                    for (int i = 0; i < Enum.GetNames(typeof(Categoria)).Length; i++) {
                        foreach (var contextpreference in userPreferencesContext) {
                            if (contextpreference.ToString().Equals(Enum.GetNames(typeof(Categoria))[i])) {
                                userPreferences[i] = true;
                            }
                        }
                    }

                    var email = await _userManager.GetEmailAsync(user);
                    Email = email;

                    Input = new InputModel
                    {
                        UserName = userName,
                        Location = userLocation,
                        NewPhoto = userPhoto.ToString(),
                        Preferences = userPreferences,
                    };
                }
                else if (User.IsInRole("Manager"))
                {
                    ViewData["UserPhoto"] = ShowImage(null);
                }
            }

            

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            var hasPassword = await _userManager.HasPasswordAsync(user);
            ViewData["HasPassword"] = hasPassword;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            ViewData["HasPassword"] = hasPassword;

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeNameAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            removeAllFromModelStateBut("Input.UserName");
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();
            if (Input.UserName != cliente.Name)
            {
                cliente.Name = Input.UserName;
                var changes = _context.SaveChanges();

                if (changes == 0)
                {
                    StatusMessage = "Ocorreu um erro inesperado ao tentar definir o nome do utilizador.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "O seu perfil foi atualizado.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangeLocationAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            removeAllFromModelStateBut("Input.Location");
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();
            if (Input.Location != cliente.Localizacao)
            {
                cliente.Localizacao = Input.Location;
                var changes = _context.SaveChanges();

                if (changes == 0)
                {
                    StatusMessage = "Ocorreu um erro inesperado ao tentar definir a localização.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "O seu perfil foi atualizado.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePhoneAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            removeAllFromModelStateBut("Input.UserPhone");
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = _userManager.GetUserId(User);
            Comerciante comerciante = _context.Comerciante.Where(x => x.UserId.Equals(userId)).FirstOrDefault();
            if (Input.UserPhone != comerciante.contact)
            {
                comerciante.contact = Input.UserPhone;
                var changes = _context.SaveChanges();

                if (changes == 0)
                {
                    StatusMessage = "Ocorreu um erro inesperado ao tentar definir o contacto.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "O seu perfil foi atualizado.";
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            removeAllFromModelStateBut("Input.NewEmail");
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await SendEmailAsync(
                    Input.NewEmail,
                    "Confirm your new email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                StatusMessage = "Link de confirmação enviado. Porfavor verifique o seu email.";
                return RedirectToPage();
            }

            StatusMessage = "O seu email não foi alterado";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            removeAllFromModelStateBut("Input.NewEmail");
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Link de confirmação enviado. Porfavor verifique o seu email.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSetPasswordAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            removeAllFromModelStateBut([nameof(Input.NewPassword), nameof(Input.ConfirmPassword)]);
            if (!ModelState.IsValid){
                await LoadAsync(user);
                return Page();
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                await LoadAsync(user);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "A sua senha foi definida";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            removeAllFromModelStateBut([nameof(Input.OldPassword), nameof(Input.NewPassword), nameof(Input.ConfirmPassword)]);
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                await LoadAsync(user);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Sua senha foi alterada.";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePhotoAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            removeAllFromModelStateBut("Input.NewPhoto");
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var webRootPath = _webHostEnvironment.WebRootPath;
            var choosedPhotoPath = Path.Combine(webRootPath, "images", $"userphoto_{Input.NewPhoto}.png");

            // Check if the file exists
            if (!System.IO.File.Exists(choosedPhotoPath))
            {
                StatusMessage = "Ocorreu um erro. Não foi possivel localizar o ficheiro da imagem selecionada.";
                return RedirectToPage();
            }

            byte[] imageData;
            using (var stream = new FileStream(choosedPhotoPath, FileMode.Open))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }

            if (imageData.Count() != 0) {
                var userId = _userManager.GetUserId(User);
                Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();
                cliente.Image = imageData;
                var changes = _context.SaveChanges();

                if (changes == 0)
                {
                    StatusMessage = "Ocorreu um erro inesperado ao tentar definir a foto.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "O seu perfil foi atualizado.";
            return RedirectToPage();
        }

        public async Task<ActionResult> OnPostChangePhotoComercianteAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            removeAllFromModelStateBut("Input.Logo");
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

          

            var memoryStreamImg = new MemoryStream();
           if(Input.Logo != null)
            {
                await Input.Logo.CopyToAsync(memoryStreamImg);
            }
           
           

            var userId = _userManager.GetUserId(User);
            Comerciante comerciante = _context.Comerciante.Where(c => c.UserId.Equals(userId)).FirstOrDefault();
            comerciante.logo = memoryStreamImg.ToArray();

            await _context.SaveChangesAsync();

            await _signInManager.RefreshSignInAsync(user);
          
            StatusMessage = "O seu perfil foi atualizado.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePreferencesAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            removeAllFromModelStateBut("Input.Preferences");
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();

            List<Categoria> newPreferences = new List<Categoria>();
            for (int i = 0; i < Input.Preferences.Length; i++)
            {
                if (Input.Preferences[i])
                    newPreferences.Add((Categoria)i);
            }

            if (!ListsAreEqual(newPreferences, cliente.Preferencias))
            {
                cliente.Preferencias = newPreferences;
                var changes = _context.SaveChanges();

                if (changes == 0)
                {
                    StatusMessage = "Ocorreu um erro inesperado ao tentar definir as preferências.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "O seu perfil foi atualizado.";
            return RedirectToPage();
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

        public string ShowImage(byte[]? image)
        {
            if (image == null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "userphoto_0.png");
                if (!System.IO.File.Exists(imagePath))
                    return null;

                return Convert.ToBase64String(System.IO.File.ReadAllBytes(imagePath));
            }

            return Convert.ToBase64String(image);
        }

        public bool CompareBytes(byte[] imageData1, byte[] imageData2) {
            bool equal = false;
            if (imageData1.Length == imageData2.Length)
            {
                equal = true;

                // Compare each byte in the arrays
                for (int i = 0; i < imageData1.Length; i++)
                {
                    if (imageData1[i] != imageData2[i])
                    {
                        equal = false;
                        break;
                    }
                }

            }
            return equal;
        }

        public static bool ListsAreEqual<T>(List<T> list1, List<T> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }

            var dict1 = list1.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            var dict2 = list2.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

            foreach (var key in dict1.Keys)
            {
                if (!dict2.ContainsKey(key) || dict1[key] != dict2[key])
                {
                    return false;
                }
            }

            return true;
        }

        public void removeAllFromModelStateBut(params string[] propertyNames)
        {
            var allPropertyNames = new string[]{
                nameof(Input.UserName),
                "Input.UserName",
                "Input.UserPhone",
                nameof(Input.Location),
                "Input.Location",
                nameof(Input.NewEmail),
                "Input.NewEmail",
                nameof(Input.OldPassword),
                "Input.OldPassword",
                nameof(Input.NewPassword),
                "Input.NewPassword",
                nameof(Input.ConfirmPassword),
                "Input.ConfirmPassword",
                nameof(Input.NewPhoto),
                "Input.NewPhoto",
            };

            foreach (var propertyName in allPropertyNames)
            {
                if (!propertyNames.Contains(propertyName))
                {
                    ModelState.Remove(propertyName);
                }
            }
        }

    }
}
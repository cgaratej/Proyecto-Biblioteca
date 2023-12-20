using Microsoft.AspNetCore.Mvc;
using Proyecto_Biblioteca.Data;
using Proyecto_Biblioteca.Models;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System;


namespace Proyecto_Biblioteca.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            IEnumerable<Users> listaUser = context.Users;
            return View(listaUser);
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Close()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        //Http Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Users _users)
        {
            if (ModelState.IsValid)
            {
                string pass = MD5Encryption(_users.password);
                var user = context.Users.Where(a => a.name.Equals(_users.name) && a.password.Equals(pass)).FirstOrDefault();
                if (user != null)
                {
                    
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, _users.name),
                        new Claim(ClaimTypes.Role, user.rango.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["error"] = "Login failed";
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [Authorize(Roles = "3")]
        public IActionResult Create()
        {
            return View();
        }

        //Http Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Users _user)
        {
            if (ModelState.IsValid)
            {
                string encryp = MD5Encryption(_user.password);
                _user.password = encryp;
                context.Users.Add(_user);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
        public string MD5Encryption(string inputData)
        {
            string hasKey = "}9cxr[L2~8!jKuDNSDqj2k7?5g@}rU}"; //You can use any string here as haskey
            byte[] bData = UTF8Encoding.UTF8.GetBytes(inputData);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripalDES = new TripleDESCryptoServiceProvider();

            tripalDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hasKey));
            tripalDES.Mode = CipherMode.ECB;

            ICryptoTransform trnsfrm = tripalDES.CreateEncryptor();
            byte[] result = trnsfrm.TransformFinalBlock(bData, 0, bData.Length);

            return System.Convert.ToBase64String(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Proyecto_Biblioteca.Data;
using Proyecto_Biblioteca.Models;
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

        //Http Post Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Users _users)
        {
            if (ModelState.IsValid)
            {
                var user = context.Users.SingleOrDefault(a => a.name.Equals(_users.name));
                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(_users.password, user.password))
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
                        ModelState.AddModelError(string.Empty, "Contraseña incorrecta");
                    }
                }
                else
                {
                    ViewData["error"] = "Login failed";
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
                string encryp = BCrypt.Net.BCrypt.HashPassword(_user.password, BCrypt.Net.BCrypt.GenerateSalt(11));
                _user.password = encryp;
                if (_user.isAdmin)
                    _user.rango = 3;
                else
                    _user.rango = 0;
                context.Users.Add(_user);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

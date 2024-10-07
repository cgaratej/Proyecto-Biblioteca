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
    [Route("User")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            IEnumerable<Users> listaUser = context.Users;
            return View(listaUser);
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
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
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
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

        [HttpGet("Edit_User/{id}")]
        public IActionResult Edit_User(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var users = context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpPost("Edit_User")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit_User(Users _user)
        {
            if (ModelState.IsValid)
            {
                string encryp;
                string encryp2;

                if (_user.isPassChage)
                {
                    encryp = BCrypt.Net.BCrypt.HashPassword(_user.password, BCrypt.Net.BCrypt.GenerateSalt(11));
                    encryp2 = BCrypt.Net.BCrypt.HashPassword(_user.confirmPassword, BCrypt.Net.BCrypt.GenerateSalt(11));
                    if (encryp != encryp2)
                    {
                        ViewBag.Message = "Las contraseñas no coinsiden";
                        return View();
                    }
                }
                context.Users.Update(_user);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var users = context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpDelete("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var model = context.Users.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            context.Users.Remove(model);
            context.SaveChanges();
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Proyecto_Biblioteca.Data;
using Proyecto_Biblioteca.Models;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;


namespace Proyecto_Biblioteca.Controllers
{
    public class UserController : Controller
    {
        //const string Sessionid = "";
        //const string SessionRang = "";
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            IEnumerable<Users> listaUser = context.Users;
            //ViewData["rango"] = HttpContext.Session.GetInt32(SessionRang);
            return View(listaUser);
        }
        public IActionResult Login()
        {
            return View();
        }
        //Http Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Users _users)
        {
            if (ModelState.IsValid)
            {
                string pass = MD5Encryption(_users.password);
                var data = context.Users.Where(a => a.name.Equals(_users.name) && a.password.Equals(pass)).ToList();
                if (data.Count() > 0)
                {
                    //HttpContext.Session.SetInt32(Sessionid, _users.id);
                    //HttpContext.Session.SetInt32(SessionRang, _users.rango);
                    TempData["id"] = _users.id;
                    TempData["rango"] = _users.rango;
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
        public IActionResult Create()
        {
            //var id = HttpContext.Session.GetInt32(Sessionid);
            //var users = context.libros.Find(id);
            //if (users == null)
            //{
            //    return RedirectToAction("Index");
            //}
            //return View(users);
            ViewData["rango"] = TempData["rango"];
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

        public string MD5Decryption(string inputData)
        {
            string hasKey = "}9cxr[L2~8!jKuDNSDqj2k7?5g@}rU}"; //You can use any string here as haskey
            byte[] bData = System.Convert.FromBase64String(inputData);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripalDES = new TripleDESCryptoServiceProvider();

            tripalDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hasKey));
            tripalDES.Mode = CipherMode.ECB;

            ICryptoTransform trnsfrm = tripalDES.CreateDecryptor();
            byte[] result = trnsfrm.TransformFinalBlock(bData, 0, bData.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}

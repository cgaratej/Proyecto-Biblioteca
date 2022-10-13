using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Biblioteca.Data;
using Proyecto_Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
namespace Proyecto_Biblioteca.Controllers
{
    public class LibrosController : Controller
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext _context)
        {
            context = _context;
        }
        
        //Http Get Index
        public IActionResult Index()
        {
            IEnumerable<Libros> listaLibros = context.libros;
            return View(listaLibros);
        }
        
        //Http Get Create
        public IActionResult Create()
        {
            return View();
        }
        
        //Http Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Libros model)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(/*Directory.GetCurrentDirectory(),*/ "wwwroot/Docs");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension

                string fileName = model.File.FileName;

                string fileNameWithPath = Path.Combine(path, fileName);
                
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
                model.UrlPdf = fileNameWithPath;
                context.libros.Add(model);
                context.SaveChanges();

                //TempData["mensaje"] = "El libro se ha creado correctamente";
                return RedirectToAction("Index");
            }
            return View();
        }

        //Http Get Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var libro = context.libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        //Http Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Libros model)
        {
            if (ModelState.IsValid)
            {
                model.UrlPdf = model.UrlPdf;
                context.libros.Update(model);
                context.SaveChanges();

                //TempData["mensaje"] = "El libro se ha actualizado correctamente";
                return RedirectToAction("Index");
            }
            return View();
        }
        
        //Http Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var libro = context.libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }
        //Http Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteLibro(int? id)
        {
            //Obtener el libro por id
            var model = context.libros.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            context.libros.Remove(model);
            context.SaveChanges();

            //TempData["mensaje"] = "El libro se ha creado correctamente";
            return RedirectToAction("Index");

        }
        //PDF
        public IActionResult PDF(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var libro = context.libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }
    }
}

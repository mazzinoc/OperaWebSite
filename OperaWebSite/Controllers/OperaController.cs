using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OperaWebSite.Data;
using OperaWebSite.Models;
using System.Data.Entity;
using System.Diagnostics;
using OperaWebSite.filters;

namespace OperaWebSite.Controllers
{
    [MyFilterAction]
    public class OperaController : Controller
    {
        //Crear instancia del DBContext

        private OperaDbContext context = new OperaDbContext();

        // GET: Opera
        public ActionResult Index()
        {
            //Traer todas las operas usando EF
            var operas = context.Operas.ToList();

            //el controller retorna una vista "Index" con la lista de operas
            return View("Index", operas);
        }

        //Creamos dos metodos para la insercion de la opera en la DB

        //primer create por GET para retornar la vista de registro
        [HttpGet]
        public ActionResult Create()
        {
            //creamos la instancia con valores en las propiedades
            Opera opera = new Opera();

            //retornamos la vista "Create" que tiene el objeto opera
            return View("Create", opera);
        }
        //Segundo create es por Post para insertar la nueva opera en la base
        //cuando el usuario en la vista Create hace click en enviar
        [HttpPost]
        public ActionResult Create(Opera opera)
        {
            if (ModelState.IsValid)
            {
                context.Operas.Add(opera);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create", opera);
        }
        //GET
        //Opera/Detail/id
        //Opera/Detail/2
        public ActionResult Detail(int id)
        {
            Opera opera = context.Operas.Find(id);

            if (opera != null)
            {
                return View("Details", opera);
            }
            else
            {
                return HttpNotFound();
            }
        }
        //GET/Opera/Delete/Id
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Opera opera = context.Operas.Find(id);

            if (opera != null)
            {
                return View("Delete", opera);
            }
            else
            {
                return HttpNotFound();
            }
        }
        // /Opera/Delete/
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Opera opera = context.Operas.Find(id);

            if (opera != null)
            {
                context.Operas.Remove(opera);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }        
        public ActionResult Edit(int id)
        {
            Opera opera = context.Operas.Find(id);
            if (opera != null)
            {
                return View("Edit", opera);
            }
            else
            {
                return HttpNotFound();
            }

        }
        [HttpPost]
        public ActionResult Edit(Opera opera)
        {
            if (ModelState.IsValid)
            {
                context.Entry(opera).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", opera);

        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var controller = filterContext.RouteData.Values["controller"];
        //    var action = filterContext.RouteData.Values["action"];
        //    Debug.WriteLine("Controller: " + controller + " Action: " + action + " paso por OnActionExecuting");
        //}
        //protected override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    var controller = filterContext.RouteData.Values["controller"];
        //    var action = filterContext.RouteData.Values["action"];
        //    Debug.WriteLine("Controller: " + controller + " Action: " + action + " paso por OnActionExecuted");
        //}

    }

}
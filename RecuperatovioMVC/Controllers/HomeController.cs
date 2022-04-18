using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RecuperatorioMVCCore.DAL;
using RecuperatorioMVCCore.Models;

namespace RecuperatorioMVCCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly NombresDBContext _Context;

        public HomeController()
        {
            _Context = new NombresDBContext();
        }

        [HttpGet]
        public IActionResult Index()
        {
            IndexVM model = new IndexVM();
            if (_Context.Nombres.Any())
            {
                model.Nombres = _Context.Nombres.ToList();
                //Craga el primer cliente de la grilla                
                Nombre nombre = model.Nombres[model.Nombres.Count - 1];
                model.Id = nombre.Id;
                model.Valor = nombre.Valor;
            }

            model.Nombres = _Context.Nombres.ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexVM model)
        {
            Nombre nombre = new Nombre();
            if (ModelState.IsValid)
            {
                nombre.Valor = model.Valor;

                _Context.Nombres.Add(nombre);

                _Context.SaveChanges();

                model.Id = nombre.Id;
            }


            model.Nombres = _Context.Nombres.ToList();

            return View(model);
        }


        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            IndexVM model = new IndexVM();

            
            Nombre nombre = _Context.Nombres.Find(id);
            _Context.Nombres.Remove(nombre);
            _Context.SaveChanges();

            model.Nombres = _Context.Nombres.ToList();

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult EliminarTodos()
        {
            List<Nombre> todosLosValores = _Context.Nombres.ToList();
            
            foreach (Nombre n in todosLosValores)
            {
                _Context.Nombres.Remove(n);
                _Context.SaveChanges();
            }

            IndexVM model = new IndexVM();
            model.Nombres = _Context.Nombres.ToList();

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult PasarMayusculas()
        {
            List<Nombre> todosLosValores = _Context.Nombres.ToList();

            foreach (Nombre n in todosLosValores)
            {
                n.Valor = n.Valor.ToUpper();
                _Context.SaveChanges();
            }

            IndexVM model = new IndexVM();
            model.Nombres = _Context.Nombres.ToList();

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Renumerar()
        {
            List<Nombre> todosLosValores = _Context.Nombres.ToList();

            foreach (Nombre n in todosLosValores)
            {
                _Context.Nombres.Remove(n);
                _Context.SaveChanges();
            }

            foreach (Nombre n in todosLosValores)
            {
                n.Id = 0;
                _Context.Nombres.Add(n);
                _Context.SaveChanges();
            }
            IndexVM model = new IndexVM();
            model.Nombres = _Context.Nombres.ToList();

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Permutar()
        {
            List<Nombre> todosLosValores = _Context.Nombres.ToList();

            string n = todosLosValores[0].Valor;
            todosLosValores[0].Valor = todosLosValores[todosLosValores.Count - 1].Valor;
            _Context.SaveChanges();
            todosLosValores[todosLosValores.Count - 1].Valor = n;
            _Context.SaveChanges();

            IndexVM model = new IndexVM();
            model.Nombres = _Context.Nombres.ToList();

            return View("Index", model);
        }

                [HttpGet]
        public IActionResult CantCaracteres()
        {
            List<Nombre> todosLosValores = _Context.Nombres.ToList();
            try
            {
                foreach (Nombre n in todosLosValores)
                {
                    int car = n.Valor.Length;
                    n.Valor = n.Valor + "(" + car + ")";
                    _Context.SaveChanges();
                }
            }
            catch
            {

            }


            IndexVM model = new IndexVM();
            model.Nombres = _Context.Nombres.ToList();

            return View("Index", model);
        }



        [HttpGet]
        public IActionResult Seleccionar(int id)
        {
            IndexVM model = new IndexVM();

            Nombre nombre = _Context.Nombres.Find(id);
            model.Id = nombre.Id;
            model.Valor = nombre.Valor;

            model.Nombres = _Context.Nombres.ToList();


            return View("Index", model);
        }

    }
}

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

        private readonly MateDBContext _Context;

        public HomeController()
        {
            _Context = new MateDBContext();
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Operacion> todasLasOperaciones = _Context.Operaciones.ToList();

            foreach (Operacion o in todasLasOperaciones)
            {
                _Context.Operaciones.Remove(o);
                _Context.SaveChanges();
            }

            IndexVM model = new IndexVM();
                //Craga el primer cliente de la grilla                

                model.OperadorA = 4;
                model.OperadorB = 1;
                model.Resultado = 0;
            

            model.Operaciones = _Context.Operaciones.ToList();

            return View(model);
        }

        private void EliminarTodos()
        {
            List<Operacion> todasLasOperaciones = _Context.Operaciones.ToList();

            foreach (Operacion o in todasLasOperaciones)
            {
                _Context.Operaciones.Remove(o);
                _Context.SaveChanges();
            }

            IndexVM model = new IndexVM();
            model.Operaciones = _Context.Operaciones.ToList();

        }

        [HttpPost]
        public IActionResult Index(IndexVM model,
            string sumar,
            string multiplicar,)
        {
            if (ModelState.IsValid)
            {
                if (sumar != null)
                    this.sumar(model);
                else if (multiplicar != null)
                    this.multiplicar(model);
                this.ModelState.Clear();
            }

            _Context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;

            model.Operaciones = _Context.Operaciones.ToList();

            return View(model);
        }


        private void sumar(IndexVM model)
        {
            Operacion operacion = new Operacion();
            if (ModelState.IsValid)
            {
                model.Resultado = model.OperadorA + model.OperadorB;
                operacion.Valor = model.OperadorA.ToString() + "+" + model.OperadorB.ToString() + "=" + model.Resultado.ToString();

                _Context.Operaciones.Add(operacion);

                _Context.SaveChanges();

                model.Id = operacion.Id;
            }
        }
        private void multiplicar(IndexVM model)
        {
            Operacion op = new Operacion();
            if (ModelState.IsValid)
            {
                model.Resultado = model.OperadorA * model.OperadorB;
                op.Valor = model.OperadorA.ToString() + "*" + model.OperadorB.ToString() + "=" + model.Resultado.ToString();

                _Context.Operaciones.Add(op);

                _Context.SaveChanges();

                model.Id = op.Id;
            }
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            IndexVM model = new IndexVM();


            Operacion op = _Context.Operaciones.Find(id);
            _Context.Operaciones.Remove(op);
            _Context.SaveChanges();

            model.Operaciones = _Context.Operaciones.ToList();

            return View("Index", model);
        }


        private void sumar111y1(IndexVM model)
        {
            this.EliminarTodos();
            Operacion op = new Operacion();
            if (ModelState.IsValid)
            {
                model.OperadorA = 111;
                model.OperadorB = 1;
                model.Resultado = model.OperadorA + model.OperadorB;
                op.Valor = model.OperadorA.ToString() + "+" + model.OperadorB.ToString() + "=" + model.Resultado.ToString();

                _Context.Operaciones.Add(op);

                _Context.SaveChanges();

                model.Id = op.Id;
            }
        }


        private void OpaMasOpb(IndexVM model)
        {
            string OpamasOpb = model.OperadorA.ToString() + "+" + model.OperadorB.ToString();
            List<Operacion> operacionesElegidas = _Context.Operaciones.Where(op => op.Valor.Contains(OpamasOpb)).ToList();
            model.Operaciones = operacionesElegidas;
            this.ModelState.Clear();

        }


        [HttpGet]
        public IActionResult EliminarATodos()
        {
            IndexVM model = new IndexVM();
            this.EliminarTodos();
            model.Operaciones = _Context.Operaciones.ToList();
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult DejarUltimosCinco()
        {
            IndexVM model = new IndexVM();
            List<Operacion> todasLasOperaciones = _Context.Operaciones.ToList();
            if (todasLasOperaciones.Count > 5)
            {
 
                for (int i = 0; i < todasLasOperaciones.Count-5; i++)
                {
                    Operacion op = _Context.Operaciones.Find(todasLasOperaciones[i].Id);
                    _Context.Operaciones.Remove(op);
                    _Context.SaveChanges();
                }
            }
              
            
            model.Operaciones = _Context.Operaciones.ToList();
            return View("Index", model);
        }

    }
}

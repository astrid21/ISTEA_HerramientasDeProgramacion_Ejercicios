using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecuperatorioMVCCore.DAL;
using System.ComponentModel.DataAnnotations;

namespace RecuperatorioMVCCore.Models
{
    public class IndexVM
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string Valor { get; set; }

        public List<Nombre> Nombres = new List<Nombre>();


    }
}

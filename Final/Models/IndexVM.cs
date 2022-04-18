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

        [Required(ErrorMessage = "El {0} es obligatoria.")]
        public int OperadorA { get; set; }

        [Required(ErrorMessage = "El {0} es obligatoria.")]
        public int OperadorB { get; set; }

        [Required(ErrorMessage = "El {0} es obligatoria.")]
        public int Resultado { get; set; }

        public string Valor { get; set; }

        public List<Operacion> Operaciones = new List<Operacion>();
    }
}

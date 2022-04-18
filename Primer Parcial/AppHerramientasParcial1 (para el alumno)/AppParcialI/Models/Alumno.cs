using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AppParcialI.Models
{
    public class Alumno
    {
        [Required(ErrorMessage = "El {0} es de ingreso obligatorio.")]
        [Range(100,999)]
        public int Legajo { set; get; }

        [Required(ErrorMessage = "El {0} es de ingreso obligatorio.")]
        [MaxLength(10, ErrorMessage = "El {0} tiene un máximo de {1} caracteres.")]
        public string Apellido { set; get; }

        [Required(ErrorMessage = "El {0} es de ingreso obligatorio.")]
        [Range(0, 10, ErrorMessage = "El {0} es debe estar entre {1} y {2}.")]
        public int Promedio { set; get; }        
    }
}

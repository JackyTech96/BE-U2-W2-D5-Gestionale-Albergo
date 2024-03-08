using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_U2_W2_D5_Albergo.Models
{
    public class Camera
    {
        [ScaffoldColumn(false)]
        public int IDCamera { get; set; }

        [Required(ErrorMessage = "Il Numero della camera è obbligatorio")]
        public int NumeroCamera {  get; set; }

        [Required(ErrorMessage = "Il campo {0} è obbligatorio.")]
        [StringLength(100, ErrorMessage = "La {0} deve essere lunga massimo {1} caratteri.")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Inserire Singola o Doppia")]
        [CheckTipoCamera(AllowType = "Singola,Doppia", ErrorMessage = ("Scegli tra: 'Singola', 'Doppia'"))]
        public string Tipologia { get; set; }
    }
}
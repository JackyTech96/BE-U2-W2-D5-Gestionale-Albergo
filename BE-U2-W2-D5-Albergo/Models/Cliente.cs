using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_U2_W2_D5_Albergo.Models
{
    public class Cliente
    {
        [ScaffoldColumn(false) ]
        public int IDCliente { get; set; }

        [Required(ErrorMessage = "Il Nome è obbligatorio!")]
        [StringLength(50, ErrorMessage = "Il Nome deve contenere al massimo 50 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il Cognome è obbligatorio!")]
        [StringLength(50, ErrorMessage = "Il Cognome deve contenere al massimo 50 caratteri")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il Codice FIscale è obbligatorio!")]
        [StringLength(16, ErrorMessage ="Il Codice Fiscale deve contenere esattamente 16 caratteri")]
        public string CodiceFiscale { get; set; }

        [Required(ErrorMessage = "La Città è obbligatoria!")]
        [Display(Name = "Città")]
        public string Citta { get; set; }

        [Required(ErrorMessage = "La Provincia è obbligatoria!")]
        [StringLength(2, ErrorMessage = "La Provincia deve contenere massimo 2 caratteri")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "L'Email è obbligatoria!")]
        [EmailAddress(ErrorMessage = "L'Email deve essere un indirizzo email valido.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Il Telefono deve essere un numero di telefono valido.")]
        public string Telefono { get; set; }

        [Phone(ErrorMessage = "Il Cellulare deve essere un numero di telefono valido.")]
        public string Cellulare { get; set; }
    }
}
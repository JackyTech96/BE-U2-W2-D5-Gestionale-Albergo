using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_U2_W2_D5_Albergo.Models
{
    public class ServizioAggiuntivo
    {
        [ScaffoldColumn(false)]
        public int IDServizioAggiuntivo { get; set; }

        [Display(Name = "ID Prenotazione")]
        [Required(ErrorMessage = "Il campo ID Prenotazione è obbligatorio.")]
        public int IDPrenotazione { get; set; }

        [Display(Name = "Data Servizio")]
        [Required(ErrorMessage = "Il campo Data Servizio è obbligatorio.")]
        [DataType(DataType.Date)]
        public DateTime DataServizio { get; set; }

        [Display(Name = "Quantità")]
        [Required(ErrorMessage = "Il campo Quantità è obbligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "La Quantità deve essere maggiore di zero.")]
        public int Quantita { get; set; }

        [Display(Name = "Prezzo")]
        [Required(ErrorMessage = "Il campo Prezzo è obbligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Il Prezzo deve essere maggiore di zero.")]
        public decimal Prezzo { get; set; }

        [Display(Name = "Colazione in Camera")]
        public bool ColazioneInCamera { get; set; }

        [Display(Name = "Bevande Minibar")]
        public bool BevandeMinibar { get; set; }

        [Display(Name = "Internet")]
        public bool Internet { get; set; }

        [Display(Name = "Letto Aggiuntivo")]
        public bool LettoAggiuntivo { get; set; }

        [Display(Name = "Culla")]
        public bool Culla { get; set; }
    }
}
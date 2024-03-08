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
        public int IDPrenotazione { get; set; }
        public DateTime DataServizio { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
        public bool ColazioneInCamera { get; set; }
        public bool BevandeMinibar { get; set; }
        public bool Internet { get; set; }
        public bool LettoAggiuntivo { get; set; }
        public bool Culla { get; set; }
    }
}
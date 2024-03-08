using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_U2_W2_D5_Albergo.Models
{
    public class Prenotazione
    {
        [ScaffoldColumn(false)]
        public int IDPrenotazione { get; set; }

        [Display(Name = "ID Cliente")]
        [Required(ErrorMessage = "Il campo ID Cliente è obbligatorio.")]
        public int IDCliente { get; set; }

        [Display(Name = "ID Camera")]
        [Required(ErrorMessage = "Il campo ID Camera è obbligatorio.")]
        public int IDCamera { get; set; }

        [Display(Name = "Codice Fiscale Cliente")]
        [Required(ErrorMessage = "Il campo Codice Fiscale Cliente è obbligatorio.")]
        [StringLength(16, ErrorMessage = "Il Codice Fiscale deve essere di massimo 16 caratteri.")]
        public string CodiceFiscaleCliente { get; set; }

        [Display(Name = "Numero Camera")]
        [Required(ErrorMessage = "Il campo Numero Camera è obbligatorio.")]
        public int NumeroCamera { get; set; }

        [Display(Name = "Data Prenotazione")]
        [Required(ErrorMessage = "Il campo Data Prenotazione è obbligatorio.")]
        public DateTime DataPrenotazione { get; set; }

        [Display(Name = "Numero Progressivo Anno")]
        [Required(ErrorMessage = "Il campo Numero Progressivo Anno è obbligatorio.")]
        public int NumeroProgressivoAnno { get; set; }

        [Display(Name = "Anno")]
        [Required(ErrorMessage = "Il campo Anno è obbligatorio.")]
        public int Anno { get; set; }

        [Display(Name = "Periodo Dal")]
        [Required(ErrorMessage = "Il campo Periodo Dal è obbligatorio.")]
        public DateTime PeriodoDal { get; set; }

        [Display(Name = "Periodo Al")]
        [Required(ErrorMessage = "Il campo Periodo Al è obbligatorio.")]
        public DateTime PeriodoAl { get; set; }

        [Display(Name = "Caparra Confirmatoria")]
        [Required(ErrorMessage = "Il campo Caparra Confirmatoria è obbligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "Il valore deve essere maggiore o uguale a 0.")]
        public decimal CaparraConfirmatoria { get; set; }

        [Display(Name = "Tariffa Applicata")]
        [Required(ErrorMessage = "Il campo Tariffa Applicata è obbligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "Il valore deve essere maggiore o uguale a 0.")]
        public decimal TariffaApplicata { get; set; }

        [Display(Name = "Mezza Pensione")]
        public bool MezzaPensione { get; set; }

        [Display(Name = "Pensione Completa")]
        public bool PensioneCompleta { get; set; }

        [Display(Name = "Pernottamento Con Colazione")]
        public bool PernottamentoConColazione { get; set; }

        [Display(Name = "Servizi Aggiuntivi")]
        public List<ServizioAggiuntivo> ServiziAggiuntivi { get; set; }

        [Display(Name = "Importo Da Saldare")]
        [Required(ErrorMessage = "Il campo Importo Da Saldare è obbligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "Il valore deve essere maggiore o uguale a 0.")]
        public decimal ImportoDaSaldare { get; set; }
    }
}
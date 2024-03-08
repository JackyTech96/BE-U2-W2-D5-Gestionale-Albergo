using BE_U2_W2_D5_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE_U2_W2_D5_Albergo.Controllers
{
    public class PrenotazioneController : Controller
    {
        // GET: Prenotazione
        public ActionResult Index()
        {
            SqlConnection conn = Utility.GetConnection();
            List<Prenotazione> listaPrenotazioni = new List<Prenotazione>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Prenotazioni";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Prenotazione prenotazione = new Prenotazione
                    {
                        IDPrenotazione = Convert.ToInt32(reader["IDPrenotazione"]),
                        IDCliente = Convert.ToInt32(reader["IDCliente"]),
                        IDCamera = Convert.ToInt32(reader["IDCamera"]),
                        CodiceFiscaleCliente = reader["CodiceFiscaleCliente"].ToString(),
                        NumeroCamera = Convert.ToInt32(reader["NumeroCamera"]),
                        DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                        NumeroProgressivoAnno = Convert.ToInt32(reader["NumeroProgressivoAnno"]),
                        Anno = Convert.ToInt32(reader["Anno"]),
                        PeriodoDal = Convert.ToDateTime(reader["PeriodoDal"]),
                        PeriodoAl = Convert.ToDateTime(reader["PeriodoAl"]),
                        CaparraConfirmatoria = Convert.ToDecimal(reader["CaparraConfirmatoria"]),
                        TariffaApplicata = Convert.ToDecimal(reader["TariffaApplicata"]),
                        MezzaPensione = Convert.ToBoolean(reader["MezzaPensione"]),
                        PensioneCompleta = Convert.ToBoolean(reader["PensioneCompleta"]),
                        PernottamentoConColazione = Convert.ToBoolean(reader["PernottamentoConColazione"])
                    };

                    listaPrenotazioni.Add(prenotazione);
                }
            }
            catch (Exception ex)
            {
                // Utilizza un logger o Debug.WriteLine per registrare l'errore
                Debug.WriteLine($"Si è verificato un errore: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            ViewBag.msgSuccess = TempData["msgSuccess"];
            return View(listaPrenotazioni);
        }

        // GET: Prenotazione/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prenotazione/Create
        [HttpPost]
        public ActionResult Create(Prenotazione prenotazione)
        {
            SqlConnection conn = Utility.GetConnection();

            try
            {
                conn.Open();

                string query = "INSERT INTO Prenotazioni (IDCliente, IDCamera, CodiceFiscaleCliente, NumeroCamera, " +
                               "DataPrenotazione, NumeroProgressivoAnno, Anno, PeriodoDal, PeriodoAl, CaparraConfirmatoria, " +
                               "TariffaApplicata, MezzaPensione, PensioneCompleta, PernottamentoConColazione) " +
                               "VALUES (@IDCliente, @IDCamera, @CodiceFiscaleCliente, @NumeroCamera, @DataPrenotazione, " +
                               "@NumeroProgressivoAnno, @Anno, @PeriodoDal, @PeriodoAl, @CaparraConfirmatoria, @TariffaApplicata, " +
                               "@MezzaPensione, @PensioneCompleta, @PernottamentoConColazione)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@IDCliente", prenotazione.IDCliente);
                cmd.Parameters.AddWithValue("@IDCamera", prenotazione.IDCamera);
                cmd.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente);
                cmd.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                cmd.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                cmd.Parameters.AddWithValue("@NumeroProgressivoAnno", prenotazione.NumeroProgressivoAnno);
                cmd.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                cmd.Parameters.AddWithValue("@PeriodoDal", prenotazione.PeriodoDal);
                cmd.Parameters.AddWithValue("@PeriodoAl", prenotazione.PeriodoAl);
                cmd.Parameters.AddWithValue("@CaparraConfirmatoria", prenotazione.CaparraConfirmatoria);
                cmd.Parameters.AddWithValue("@TariffaApplicata", prenotazione.TariffaApplicata);
                cmd.Parameters.AddWithValue("@MezzaPensione", prenotazione.MezzaPensione);
                cmd.Parameters.AddWithValue("@PensioneCompleta", prenotazione.PensioneCompleta);
                cmd.Parameters.AddWithValue("@PernottamentoConColazione", prenotazione.PernottamentoConColazione);

                cmd.ExecuteNonQuery();


                TempData["msgSuccess"] = "Prenotazione creata con successo!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Utilizza un logger o Debug.WriteLine per registrare l'errore
                Debug.WriteLine($"Si è verificato un errore: {ex.Message}");
                return View(prenotazione);
            }
            finally
            {
                conn.Close();
            }
        }

        public ActionResult Details(int id)
        {
            SqlConnection conn = Utility.GetConnection();
            Prenotazione prenotazione = new Prenotazione();

            try
            {
                conn.Open();

                // Ottieni i dettagli della prenotazione
                string queryPrenotazione = $"SELECT * FROM Prenotazioni WHERE IDPrenotazione = {id}";
                SqlCommand cmdPrenotazione = new SqlCommand(queryPrenotazione, conn);

                using (SqlDataReader readerPrenotazione = cmdPrenotazione.ExecuteReader())
                {
                    if (readerPrenotazione.Read())
                    {
                        prenotazione.NumeroCamera = Convert.ToInt32(readerPrenotazione["NumeroCamera"]);
                        prenotazione.PeriodoDal = Convert.ToDateTime(readerPrenotazione["PeriodoDal"]);
                        prenotazione.PeriodoAl = Convert.ToDateTime(readerPrenotazione["PeriodoAl"]);

                        // Calcola l'importo da saldare
                        prenotazione.TariffaApplicata = Convert.ToDecimal(readerPrenotazione["TariffaApplicata"]);
                        prenotazione.CaparraConfirmatoria = Convert.ToDecimal(readerPrenotazione["CaparraConfirmatoria"]);

                        // Calcola l'importo totale considerando la tariffa, la caparra e altri dettagli
                        prenotazione.ImportoDaSaldare = prenotazione.TariffaApplicata - prenotazione.CaparraConfirmatoria;

                        // Chiudi il primo DataReader prima di aprirne uno nuovo
                        readerPrenotazione.Close();
                    }
                }

                // Recupera i servizi aggiuntivi associati a questa prenotazione
                string queryServizi = $"SELECT * FROM ServiziAggiuntivi WHERE IDPrenotazione = {id}";
                SqlCommand cmdServizi = new SqlCommand(queryServizi, conn);

                using (SqlDataReader readerServizi = cmdServizi.ExecuteReader())
                {
                    List<ServizioAggiuntivo> serviziAggiuntivi = new List<ServizioAggiuntivo>();
                    while (readerServizi.Read())
                    {
                        ServizioAggiuntivo servizio = new ServizioAggiuntivo
                        {
                            ColazioneInCamera = Convert.ToBoolean(readerServizi["ColazioneInCamera"]),
                            BevandeMinibar = Convert.ToBoolean(readerServizi["BevandeMinibar"]),
                            Internet = Convert.ToBoolean(readerServizi["Internet"]),
                            LettoAggiuntivo = Convert.ToBoolean(readerServizi["LettoAggiuntivo"]),
                            Culla = Convert.ToBoolean(readerServizi["Culla"]),
                            Prezzo = Convert.ToDecimal(readerServizi["Prezzo"])
                        };
                        serviziAggiuntivi.Add(servizio);

                        // Somma i costi dei servizi aggiuntivi all'importo da saldare
                        prenotazione.ImportoDaSaldare += servizio.Prezzo;
                    }

                    // Aggiungi la lista dei servizi aggiuntivi al modello Prenotazione
                    prenotazione.ServiziAggiuntivi = serviziAggiuntivi;
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Si è verificato un errore: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return View(prenotazione);
        }

        public JsonResult RicercaPerCodiceFiscale(string codiceFiscale)
        {
            List<Prenotazione> prenotazioni = new List<Prenotazione>();

            // Connessione al database usando ADO.NET
            using (SqlConnection conn = Utility.GetConnection())
            {
                conn.Open();

                // Query SQL per recuperare le prenotazioni per il codice fiscale specificato
                string query = "SELECT * FROM Prenotazioni WHERE CodiceFiscaleCliente = @CodiceFiscale";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CodiceFiscale", codiceFiscale);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Prenotazione prenotazione = new Prenotazione
                        {
                            IDPrenotazione = Convert.ToInt32(reader["IDPrenotazione"]),
                            IDCliente = Convert.ToInt32(reader["IDCliente"]),
                            IDCamera = Convert.ToInt32(reader["IDCamera"]),
                            NumeroCamera = Convert.ToInt32(reader["NumeroCamera"]),
                            DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                            NumeroProgressivoAnno = Convert.ToInt32(reader["NumeroProgressivoAnno"]),
                            Anno = Convert.ToInt32(reader["Anno"]),
                            PeriodoDal = Convert.ToDateTime(reader["PeriodoDal"]),
                            PeriodoAl = Convert.ToDateTime(reader["PeriodoAl"]),
                            CaparraConfirmatoria = Convert.ToDecimal(reader["CaparraConfirmatoria"]),
                            TariffaApplicata = Convert.ToDecimal(reader["TariffaApplicata"]),
                            MezzaPensione = Convert.ToBoolean(reader["MezzaPensione"]),
                            PensioneCompleta = Convert.ToBoolean(reader["PensioneCompleta"]),
                            PernottamentoConColazione = Convert.ToBoolean(reader["PernottamentoConColazione"])
                        };
                        prenotazioni.Add(prenotazione);
                    }
                }
            }

            // Restituisci la vista parziale con i risultati della ricerca
            return Json(prenotazioni, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RicercaPrenotazioniPensioneCompleta()
        {
            int count = 0;

            // Connessione al database usando ADO.NET
            using (SqlConnection conn = Utility.GetConnection())
            {
                conn.Open();

                // Query SQL per contare le prenotazioni con 'Pensione Completa'
                string query = "SELECT COUNT(*) FROM Prenotazioni WHERE PensioneCompleta = 'true'";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Esegui la query e ottieni il conteggio
                count = (int)cmd.ExecuteScalar();
            }

            // Restituisci il conteggio come JSON
            return Json(new { count }, JsonRequestBehavior.AllowGet);
        }
    }
}




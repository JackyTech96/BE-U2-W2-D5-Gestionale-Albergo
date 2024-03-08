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
    public class ServizioAggiuntivoController : Controller
    {
        // GET: ServizioAggiuntivo
        public ActionResult Index()
        {
            SqlConnection conn = Utility.GetConnection();
            List<ServizioAggiuntivo> listaServiziAggiuntivi = new List<ServizioAggiuntivo>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM ServiziAggiuntivi";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ServizioAggiuntivo servizioAggiuntivo = new ServizioAggiuntivo
                    {
                        IDServizioAggiuntivo = Convert.ToInt32(reader["IDServizioAggiuntivo"]),
                        IDPrenotazione = Convert.ToInt32(reader["IDPrenotazione"]),
                        DataServizio = Convert.ToDateTime(reader["DataServizio"]),
                        Quantita = Convert.ToInt32(reader["Quantita"]),
                        Prezzo = Convert.ToDecimal(reader["Prezzo"]),
                        ColazioneInCamera = Convert.ToBoolean(reader["ColazioneInCamera"]),
                        BevandeMinibar = Convert.ToBoolean(reader["BevandeMinibar"]),
                        Internet = Convert.ToBoolean(reader["Internet"]),
                        LettoAggiuntivo = Convert.ToBoolean(reader["LettoAggiuntivo"]),
                        Culla = Convert.ToBoolean(reader["Culla"])
                    };

                    listaServiziAggiuntivi.Add(servizioAggiuntivo);
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
            return View(listaServiziAggiuntivi);
        }

        // GET: ServizioAggiuntivo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServizioAggiuntivo/Create
        [HttpPost]
        public ActionResult Create(ServizioAggiuntivo servizioAggiuntivo)
        {
            SqlConnection conn = Utility.GetConnection();

            try
            {
                conn.Open();

                string query = "INSERT INTO ServiziAggiuntivi (IDPrenotazione, DataServizio, Quantita, Prezzo, " +
                               "ColazioneInCamera, BevandeMinibar, Internet, LettoAggiuntivo, Culla) " +
                               "VALUES (@IDPrenotazione, @DataServizio, @Quantita, @Prezzo, " +
                               "@ColazioneInCamera, @BevandeMinibar, @Internet, @LettoAggiuntivo, @Culla)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@IDPrenotazione", servizioAggiuntivo.IDPrenotazione);
                cmd.Parameters.AddWithValue("@DataServizio", servizioAggiuntivo.DataServizio);
                cmd.Parameters.AddWithValue("@Quantita", servizioAggiuntivo.Quantita);
                cmd.Parameters.AddWithValue("@Prezzo", servizioAggiuntivo.Prezzo);
                cmd.Parameters.AddWithValue("@ColazioneInCamera", servizioAggiuntivo.ColazioneInCamera);
                cmd.Parameters.AddWithValue("@BevandeMinibar", servizioAggiuntivo.BevandeMinibar);
                cmd.Parameters.AddWithValue("@Internet", servizioAggiuntivo.Internet);
                cmd.Parameters.AddWithValue("@LettoAggiuntivo", servizioAggiuntivo.LettoAggiuntivo);
                cmd.Parameters.AddWithValue("@Culla", servizioAggiuntivo.Culla);

                cmd.ExecuteNonQuery();

                TempData["msgSuccess"] = "Servizio aggiuntivo creato con successo!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Utilizza un logger o Debug.WriteLine per registrare l'errore
                Debug.WriteLine($"Si è verificato un errore: {ex.Message}");
                return View(servizioAggiuntivo);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
using BE_U2_W2_D5_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace BE_U2_W2_D5_Albergo.Controllers
{

    // GET: Cliente
    [Authorize]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            SqlConnection conn = Utility.GetConnection();
            List<Cliente> listaClienti = new List<Cliente>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Clienti";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        IDCliente = Convert.ToInt32(reader["IDCliente"]),
                        Nome = reader["Nome"].ToString(),
                        Cognome = reader["Cognome"].ToString(),
                        CodiceFiscale = reader["CodiceFiscale"].ToString(),
                        Citta = reader["Citta"].ToString(),
                        Provincia = reader["Provincia"].ToString(),
                        Email = reader["Email"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Cellulare = reader["Cellulare"].ToString()
                    };
                    listaClienti.Add(cliente);
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

            ViewBag.msgSuccess = TempData["msgSuccess"];
            return View(listaClienti);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            SqlConnection conn = Utility.GetConnection();
            Cliente cliente = new Cliente();

            try
            {
                conn.Open();

                string query = $"SELECT * FROM Clienti WHERE IDCliente = {id}";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    cliente.IDCliente = Convert.ToInt32(reader["IDCliente"]);
                    cliente.Nome = reader["Nome"].ToString();
                    cliente.Cognome = reader["Cognome"].ToString();
                    cliente.CodiceFiscale = reader["CodiceFiscale"].ToString();
                    cliente.Citta = reader["Citta"].ToString();
                    cliente.Provincia = reader["Provincia"].ToString();
                    cliente.Email = reader["Email"].ToString();
                    cliente.Telefono = reader["Telefono"].ToString();
                    cliente.Cellulare = reader["Cellulare"].ToString();
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

            ViewBag.msgSuccess = TempData["msgSuccess"];
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente nuovoCliente)
        {
            SqlConnection conn = Utility.GetConnection();

            try
            {
                conn.Open();

                string query = "INSERT INTO Clienti (Nome, Cognome, CodiceFiscale, Citta, Provincia, Email, Telefono, Cellulare) " +
                               "VALUES (@Nome, @Cognome, @CodiceFiscale, @Citta, @Provincia, @Email, @Telefono, @Cellulare)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", nuovoCliente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", nuovoCliente.Cognome);
                cmd.Parameters.AddWithValue("@CodiceFiscale", nuovoCliente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@Citta", nuovoCliente.Citta);
                cmd.Parameters.AddWithValue("@Provincia", nuovoCliente.Provincia);
                cmd.Parameters.AddWithValue("@Email", nuovoCliente.Email);
                cmd.Parameters.AddWithValue("@Telefono", nuovoCliente.Telefono);
                cmd.Parameters.AddWithValue("@Cellulare", nuovoCliente.Cellulare);

                cmd.ExecuteNonQuery();

                TempData["msgSuccess"] = "Cliente creato con successo!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.msgErrore = "Errore " + ex.Message;
            }
            finally
            {
                conn.Close();
            }

            TempData["msgSuccess"] = "Cliente " + nuovoCliente.Nome + " creato con successo!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Cliente cliente = GetClienteById(id);

            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            SqlConnection conn = Utility.GetConnection();

            try
            {
                conn.Open();

                string query = "UPDATE Clienti " +
                               "SET Nome = @Nome, Cognome = @Cognome, CodiceFiscale = @CodiceFiscale, " +
                               "Citta = @Citta, Provincia = @Provincia, Email = @Email, " +
                               "Telefono = @Telefono, Cellulare = @Cellulare " +
                               "WHERE IDCliente = @IDCliente";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDCliente", cliente.IDCliente);
                cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                cmd.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@Citta", cliente.Citta);
                cmd.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                cmd.Parameters.AddWithValue("@Email", cliente.Email);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Cellulare", cliente.Cellulare);

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                Debug.WriteLine($"Si è verificato un errore durante la modifica: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            TempData["msgSuccess"] = "Cliente aggiornato con successo!";
            return RedirectToAction("Index");
        }

        private Cliente GetClienteById(int id)
        {
            SqlConnection conn = Utility.GetConnection();
            Cliente cliente = new Cliente();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Clienti WHERE IDCliente = @IDCliente";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IDCliente", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cliente.IDCliente = Convert.ToInt32(reader["IDCliente"]);
                    cliente.Nome = reader["Nome"].ToString();
                    cliente.Cognome = reader["Cognome"].ToString();
                    cliente.CodiceFiscale = reader["CodiceFiscale"].ToString();
                    cliente.Citta = reader["Citta"].ToString();
                    cliente.Provincia = reader["Provincia"].ToString();
                    cliente.Email = reader["Email"].ToString();
                    cliente.Telefono = reader["Telefono"].ToString();
                    cliente.Cellulare = reader["Cellulare"].ToString();
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

            return cliente;
        }
    }
}
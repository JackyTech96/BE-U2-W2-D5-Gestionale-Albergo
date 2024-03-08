using BE_U2_W2_D5_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE_U2_W2_D5_Albergo.Controllers
{
    public class SignupController : Controller
    {

        // GET: Signup
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Signup
        [HttpPost]
        public ActionResult Signup(Utente nuovoUtente)
        {
            SqlConnection conn = Utility.GetConnection();
            try
            {
                conn.Open();

                string query = "INSERT INTO Utenti(Username, Password) " + "VALUES (@Username, @Password)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("Username", nuovoUtente.Username);
                cmd.Parameters.AddWithValue("Password", nuovoUtente.Password);

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                Response.Write($"Si è verificato un errore: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Login", "Login");
        }
    }
}
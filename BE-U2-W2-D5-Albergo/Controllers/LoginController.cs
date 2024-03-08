using BE_U2_W2_D5_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BE_U2_W2_D5_Albergo.Controllers
{
    public class LoginController : Controller
    {


        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Utente utente)
        {
            SqlConnection conn = Utility.GetConnection();
            try
            {
                conn.Open();

                string query = "SELECT * FROM Utenti WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("Username", utente.Username);
                cmd.Parameters.AddWithValue("Password", utente.Password);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    FormsAuthentication.SetAuthCookie(utente.Username, false);
                    return RedirectToAction("Benvenuto");
                }
                else
                {
                    conn.Close();
                    ViewBag.AuthError = "Username o Password errati";
                    return View();
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

            return View();
        }

        public ActionResult Benvenuto()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
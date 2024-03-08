﻿using BE_U2_W2_D5_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE_U2_W2_D5_Albergo.Controllers
{
    public class CameraController : Controller
    {
        // GET: Camera
        public ActionResult Index()
        {
            SqlConnection conn = Utility.GetConnection();
            List<Camera> listaCamere = new List<Camera>();

            try
            {
                conn.Open();

                string query = "SELECT * FROM Camere";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Camera camera = new Camera
                    {
                        IDCamera = Convert.ToInt32(reader["IDCamera"]),
                        NumeroCamera = Convert.ToInt32(reader["NumeroCamera"]),
                        Descrizione = reader["Descrizione"].ToString(),
                        Tipologia = reader["Tipologia"].ToString()
                    };

                    listaCamere.Add(camera);
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
            return View(listaCamere);
        }

        // GET: Camera/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Camera/Create
        [HttpPost]
        public ActionResult Create(Camera camera)
        {
            SqlConnection conn = Utility.GetConnection();

            try
            {
                conn.Open();

                string query = "INSERT INTO Camere (NumeroCamera, Descrizione, Tipologia) " +
                               "VALUES (@NumeroCamera, @Descrizione, @Tipologia)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NumeroCamera", camera.NumeroCamera);
                cmd.Parameters.AddWithValue("@Descrizione", camera.Descrizione);
                cmd.Parameters.AddWithValue("@Tipologia", camera.Tipologia);

                cmd.ExecuteNonQuery();

                TempData["msgSuccess"] = "Camera creata con successo!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Utilizza un logger o Debug.WriteLine per registrare l'errore
                Debug.WriteLine($"Si è verificato un errore: {ex.Message}");
                return View(camera);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using CitasMV.Models;

namespace CitasMV.Controllers
{
    public class EntidadesController : Controller
    {
        private readonly string? _connectionString;

        public EntidadesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQL");
        }

        // Traer las ciudades de los departamentos
        /*
        private List<Ciudad> ObtenerCiudades()
        {
            var ciudades = new List<Ciudad>();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();

                string query = @"SELECT ConseCodigo, CodigoCiudad, NombreCiudad 
                                 FROM Datos_de_las_ciudades
                                 ORDER BY NombreCiudad";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ciudades.Add(new Ciudad
                        {
                            ConseCodigo = reader.GetInt32(0),
                            CodigoCiudad = reader.GetString(1),
                            NombreCiudad = reader.GetString(2)
                        });
                    }
                }
            }

            return ciudades;
        }*/
        [HttpGet]
        public JsonResult ObtenerCiudadesPorDepartamento(string codigoDpto)
        {
            var ciudades = new List<Ciudad>();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();

                string query = @"SELECT ConseCodigo, CodigoCiudad, NombreCiudad 
                                FROM Datos_de_las_ciudades
                                WHERE CodigoDpto = @codigoDpto
                                ORDER BY NombreCiudad";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@codigoDpto", codigoDpto);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ciudades.Add(new Ciudad
                            {
                                ConseCodigo = reader.GetInt32(0),
                                CodigoCiudad = reader.GetString(1),
                                NombreCiudad = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return Json(ciudades);
        }

        // Traer los Departamentos de la base de datos
        private List<Departamento> ObtenerDptos()
        {
            var departamentos = new List<Departamento>();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();

                string query = @"SELECT CodigoDpto, CodigoPais, NombreDpto
                                 FROM Datos_de_los_Dpto_o_Estados
                                 ORDER BY NombreDpto";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departamentos.Add(new Departamento
                        {
                            CodigoDpto = reader.GetString(0),
                            CodigoPais = reader.GetString(1), // ✔ correcto porque es nvarchar
                            NombreDpto = reader.GetString(2)
                        });
                    }
                }
            }

            return departamentos;
        }

        public IActionResult EmpresasTerceros()
        {
            //ViewBag.Ciudades = ObtenerCiudades();
            ViewBag.Departamentos = ObtenerDptos();
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            //ViewBag.Ciudades = ObtenerCiudades();
            ViewBag.Departamentos = ObtenerDptos();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(string NombreEntrada, int CiudadId)
        {
            return View();
        }
    }
}


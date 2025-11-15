using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Para usar SelectListItem
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace CitasMV.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly string _connectionString;

        public UsuariosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQL");
        }

        // =========================================================
        // GET: Muestra el formulario para crear nuevo usuario
        // =========================================================
        [HttpGet]
        public IActionResult Crear()
        {
            // Lista para guardar las categorías de permisos
            List<SelectListItem> nivelesPermiso = new List<SelectListItem>();

            // Conexión a la base de datos
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();

                string query = "SELECT NivelPermiso, NombreNivel FROM Datos_niveles_de_permisos";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nivelesPermiso.Add(new SelectListItem
                        {
                            Value = reader["NivelPermiso"].ToString(),
                            Text = reader["NombreNivel"].ToString()
                        });
                    }
                }
            }

            // Enviamos la lista a la vista
            ViewBag.NivelesPermiso = nivelesPermiso;

            return View();
        }

        // =========================================================
        // POST: Procesa los datos enviados del formulario
        // =========================================================
        [HttpPost]
        public IActionResult Crear(
            string NombreEntrada,
            int SecretoUsa,
            string IdentificUsa,
            string NombreUsa,
            string Apellido1Usa,
            string Apellido2Usa,
            string CargoUsar,
            string DepenUsar,
            int NivelPermiso)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    // =========================================================
                    // OBTENER EL ÚLTIMO CÓDIGO DE USUARIO
                    // =========================================================
                    string getCodigoQuery = "SELECT ISNULL(MAX(CodigoUsuario), 0) FROM Datos_usuarios_de_los_aplicativos";
                    int nuevoCodigo = 1;

                    using (SqlCommand cmdCodigo = new SqlCommand(getCodigoQuery, conexion))
                    {
                        object result = cmdCodigo.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            nuevoCodigo = Convert.ToInt32(result) + 1;
                    }

                    // Darle formato de 3 dígitos: 001, 002, etc.
                    string codigoFormateado = nuevoCodigo.ToString("D3");

                    // =========================================================
                    // INSERTAR EL NUEVO USUARIO EN LA BASE DE DATOS
                    // =========================================================
                    string query = @"INSERT INTO Datos_usuarios_de_los_aplicativos
                                    (CodigoUsuario, SecretoUsa, NombreEntrada, IdentificUsa, NombreUsa, Apellido1Usa, 
                                    Apellido2Usa, CargoUsar, DepenUsar, Vigente, NivelPermiso)
                                    VALUES (@CodigoUsuario, @SecretoUsa, @NombreEntrada, @IdentificUsa, @NombreUsa,
                                            @Apellido1Usa, @Apellido2Usa, @CargoUsar, @DepenUsar, 1, @NivelPermiso)";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@CodigoUsuario", codigoFormateado);
                        cmd.Parameters.AddWithValue("@SecretoUsa", SecretoUsa);
                        cmd.Parameters.AddWithValue("@NombreEntrada", NombreEntrada);
                        cmd.Parameters.AddWithValue("@IdentificUsa", IdentificUsa ?? "");
                        cmd.Parameters.AddWithValue("@NombreUsa", NombreUsa ?? "");
                        cmd.Parameters.AddWithValue("@Apellido1Usa", Apellido1Usa ?? "");
                        cmd.Parameters.AddWithValue("@Apellido2Usa", Apellido2Usa ?? "");
                        cmd.Parameters.AddWithValue("@CargoUsar", CargoUsar ?? "");
                        cmd.Parameters.AddWithValue("@DepenUsar", DepenUsar ?? "");
                        cmd.Parameters.AddWithValue("@NivelPermiso", NivelPermiso);

                        cmd.ExecuteNonQuery();
                    }
                }

                ViewBag.Mensaje = "Usuario creado correctamente";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al crear usuario: " + ex.Message;
            }

            // =========================================================
            // 3️RECARGAR LA LISTA DE PERMISOS POR SI HAY ERROR
            // =========================================================
            List<SelectListItem> nivelesPermiso = new List<SelectListItem>();
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                conexion.Open();
                string query = "SELECT NivelPermiso, NombreNivel FROM Datos_niveles_de_permisos";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nivelesPermiso.Add(new SelectListItem
                        {
                            Value = reader["NivelPermiso"].ToString(),
                            Text = reader["NombreNivel"].ToString()
                        });
                    }
                }
            }
            ViewBag.NivelesPermiso = nivelesPermiso;

            return View();
        }
    }
}
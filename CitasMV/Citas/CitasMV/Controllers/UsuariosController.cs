using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CitasMV.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly string _connectionString;

        public UsuariosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQL");
        }

        // Muestra el formulario
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // Procesa los datos enviados desde el formulario
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

                    string query = @"INSERT INTO Datos_usuarios_de_los_aplicativos
                                    (SecretoUsa, NombreEntrada, IdentificUsa, NombreUsa, Apellido1Usa, 
                                    Apellido2Usa, CargoUsar, DepenUsar, Vigente, NivelPermiso)
                                    VALUES (@SecretoUsa, @NombreEntrada, @IdentificUsa, @NombreUsa,
                                    @Apellido1Usa, @Apellido2Usa, @CargoUsar, @DepenUsar, 1, 3)";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
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

                ViewBag.Mensaje = "Usuario creado correctamente.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al crear usuario: " + ex.Message;
            }

            return View();
        }
    }
}

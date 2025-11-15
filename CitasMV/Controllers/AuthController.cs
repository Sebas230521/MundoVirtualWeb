using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace CitasMV.Controllers
{
    public class AuthController : Controller
    {
        // Cadena de conexión obtenida desde appsettings.json
        private readonly string _connectionString;

        // Constructor: recibe la configuración y extrae la cadena de conexión
        public AuthController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQL");
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        // Valida las credenciales contra la base de datos
        [HttpPost]
        public IActionResult Login(string usuario, string contrasena)
        {
            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                ViewBag.Error = "Por favor ingrese usuario y contraseña.";
                return View();
            }

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    // Consulta parametrizada para evitar inyección SQL
                    string query = @"SELECT COUNT(*) 
                                     FROM Tabla_Prueba_Conexion_Sql 
                                     WHERE usuario = @usuario AND pass = @pass";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        // Asignación de parámetros
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@pass", contrasena);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            // Guardamos la sesión activa
                            HttpContext.Session.SetString("usuario", usuario);

                            // Redirige al Home una vez validado
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Error = "Usuario o contraseña incorrectos.";
                            return View();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Captura de errores de conexión o ejecución SQL
                ViewBag.Error = $"Error al intentar conectar con la base de datos: {ex.Message}";
                return View();
            }
            
        }

        // GET: /Auth/Logout
        // Cierra la sesión actual y redirige al login
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        //Enviar Solicitud al correo
        // GET: /Auth/SolicitudCuenta
        [HttpGet]
        public IActionResult SolicitudCuenta()
        {
            return View();
        }

        // POST: /Auth/EnviarSolicitud
        [HttpPost]
        public IActionResult EnviarSolicitud(string nombre, string correo, string mensaje)
        {
            try
            {
                string destinatario = "mundovirtual@gmail.com";
                string asunto = "Nueva solicitud de cuenta";
                string cuerpo = $"Nombre: {nombre}\nCorreo: {correo}\n\nMensaje:\n{mensaje}";

                using (var mail = new System.Net.Mail.MailMessage())
                {
                    mail.From = new System.Net.Mail.MailAddress("tucorreo@gmail.com"); //Poner el correo para las solicitudes.
                    mail.To.Add(destinatario);
                    mail.Subject = asunto;
                    mail.Body = cuerpo;

                    using (var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(
                            "tucorreo@gmail.com",
                            "tu_contraseña_de_aplicación"); //Usar la contraseña de aplicación
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                ViewBag.Mensaje = "✅ Solicitud enviada con éxito. Te contactaremos pronto.";
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "❌ Error al enviar la solicitud: " + ex.Message;
            }

            return View("SolicitudCuenta");
        }


    }
}

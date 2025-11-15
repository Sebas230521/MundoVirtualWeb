using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using CitasMV.Models;

namespace CitasMV.Controllers
{
    public class GestionTarifasController : Controller
    {
        private readonly string _connectionString;

        public GestionTarifasController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConexionSQL");
        }

        // ---------------------------------------------
        //  C A R G A R   F O R M U L A R I O
        // ---------------------------------------------
        public IActionResult GestionTarifas(int? id)
        {
            Tarifa tarifa = new Tarifa();
            return View(tarifa);
        }

        /*public IActionResult GestionTarifas()
        {
            return View();
        }*/


        // ---------------------------------------------
        //  O B T E N E R   U N A   T A R I F A
        // ---------------------------------------------
        private Tarifa ObtenerTarifa(int id)
        {
            Tarifa t = new Tarifa();

            using SqlConnection con = new(_connectionString);
            con.Open();

            string query = @"SELECT Codigo, Nombre, Descripcion, ManualTarifario, Signo,
                                    Porcentaje, RegistradoPor, FechaRegistro,
                                    ModificadoPor, FechaModificacion
                             FROM Datos_Tarifas
                             WHERE Codigo = @Codigo";

            using SqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@Codigo", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                t.Codigo = reader.GetInt32(0);
                t.Nombre = reader.GetString(1);
                t.Descripcion = reader.GetString(2);
                t.ManualTarifario = reader.GetString(3);
                t.Signo = reader.GetString(4);
                t.Porcentaje = reader.GetDecimal(5);
                t.RegistradoPor = reader.GetString(6);
                t.FechaRegistro = reader.GetDateTime(7);
                t.ModificadoPor = reader.GetString(8);
                t.FechaModificacion = reader.GetDateTime(9);
            }

            return t;
        }


        // ---------------------------------------------
        //  G U A R D A R   T A R I F A   (CREATE/UPDATE)
        // ---------------------------------------------
        [HttpPost]
        public IActionResult Guardar(Tarifa tarifa)
        {
            if (tarifa.Codigo == 0)
                CrearTarifa(tarifa);
            else
                ActualizarTarifa(tarifa);

            return RedirectToAction("Gestion", new { id = tarifa.Codigo });
        }

        private void CrearTarifa(Tarifa t)
        {
            using SqlConnection con = new(_connectionString);
            con.Open();

            string query = @"INSERT INTO Datos_Tarifas 
                            (Nombre, Descripcion, ManualTarifario, Signo, Porcentaje,
                             RegistradoPor, FechaRegistro)
                             VALUES
                            (@Nombre, @Descripcion, @ManualTarifario, @Signo, @Porcentaje,
                             @RegistradoPor, GETDATE())";

            using SqlCommand cmd = new(query, con);

            cmd.Parameters.AddWithValue("@Nombre", t.Nombre ?? "");
            cmd.Parameters.AddWithValue("@Descripcion", t.Descripcion ?? "");
            cmd.Parameters.AddWithValue("@ManualTarifario", t.ManualTarifario ?? "");
            cmd.Parameters.AddWithValue("@Signo", t.Signo ?? "");
            cmd.Parameters.AddWithValue("@Porcentaje", t.Porcentaje);
            cmd.Parameters.AddWithValue("@RegistradoPor", t.RegistradoPor ?? "");

            cmd.ExecuteNonQuery();
        }


        private void ActualizarTarifa(Tarifa t)
        {
            using SqlConnection con = new(_connectionString);
            con.Open();

            string query = @"UPDATE Datos_Tarifas SET
                                Nombre = @Nombre,
                                Descripcion = @Descripcion,
                                ManualTarifario = @ManualTarifario,
                                Signo = @Signo,
                                Porcentaje = @Porcentaje,
                                ModificadoPor = @ModificadoPor,
                                FechaModificacion = GETDATE()
                             WHERE Codigo = @Codigo";

            using SqlCommand cmd = new(query, con);

            cmd.Parameters.AddWithValue("@Codigo", t.Codigo);
            cmd.Parameters.AddWithValue("@Nombre", t.Nombre ?? "");
            cmd.Parameters.AddWithValue("@Descripcion", t.Descripcion ?? "");
            cmd.Parameters.AddWithValue("@ManualTarifario", t.ManualTarifario ?? "");
            cmd.Parameters.AddWithValue("@Signo", t.Signo ?? "");
            cmd.Parameters.AddWithValue("@Porcentaje", t.Porcentaje);
            cmd.Parameters.AddWithValue("@ModificadoPor", t.ModificadoPor ?? "");

            cmd.ExecuteNonQuery();
        }


        // ---------------------------------------------
        //  E L I M I N A R
        // ---------------------------------------------
        public IActionResult Eliminar(int id)
        {
            using SqlConnection con = new(_connectionString);
            con.Open();

            using SqlCommand cmd = new("DELETE FROM Datos_Tarifas WHERE Codigo = @Codigo", con);
            cmd.Parameters.AddWithValue("@Codigo", id);
            cmd.ExecuteNonQuery();

            return RedirectToAction("Gestion");
        }
    }

    internal class Tarifas
    {
    }
}


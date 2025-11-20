using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using CitasMV.Models;

namespace CitasMV.Controllers
{
    public class PacientesController : Controller
    {
        private readonly string _connectionString;

        public PacientesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQL")
                                ?? throw new ArgumentNullException("Connection string 'ConexionSQL' no encontrada.");
        }

        // GET: /Pacientes/Crear
        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.TipoIden = GetLookupList("Datos_documentos_indentificacion", "CodIdenti", "NomIdenti");
            ViewBag.Paises = GetLookupList("Datos_de_paises", "CodiPais", "NomPais");
            ViewBag.Departamentos = GetLookupList("Datos_de_los_Dpto_o_Estados", "CodigoDpto", "NombreDpto");
            ViewBag.Ciudades = new List<SelectListItem>();
            ViewBag.Barrios = new List<SelectListItem>();
            ViewBag.EstadosCiviles = GetLookupList("Datos_estado_civil", "CodEstado", "NomEstado");
            ViewBag.TiposUsuarios = GetLookupList("Datos_tipos_de_usuarios", "CodTipoUsuar", "NomTipo");
            ViewBag.Etnia = GetLookupList("Datos_grupos_etnicos", "CodiGrupo", "NomAten");
            ViewBag.Administradoras = GetLookupList("Datos_administradoras_de_planes", "CodInterno", "NomAdmin");

            ViewBag.Estratos = GetFixedListEstrato();
            ViewBag.Zonas = new List<SelectListItem>
            {
                new SelectListItem { Value = "U", Text = "Urbano" },
                new SelectListItem { Value = "R", Text = "Rural" }
            };

            var model = new PacienteModel
            {
                FechaNaci = DateTime.Today,
                Sexo = "F",
                EstadoCivil = "T",
                ZonaResiden = "U",
                TipoUsar = "2",
                GrupoEtni = "6"
            };

            return View(model);
        }


        // POST: /Pacientes/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(PacienteModel model)
        {
            if (!ModelState.IsValid)
            {
                RecargarListas(model);
                return View(model);
            }

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string insertSql = @"
INSERT INTO Datos_del_Paciente
(HistorPaci, TipoIden, NumIden, Nombre1, Nombre2, Apellido1, Apellido2, FechaNaci,
CodPaisNace, CodDptoNace, CodCiuNace, CodDpto, CodMuni, BarrioVive,
DirecResi, TelResi, ZonaResiden, EstadoCivil, TipoUsar, TipoAfiliado, NumAfilia,
EstraNum, GrupoEtni, CodAdmin, Ocupacion, Observaciones,
Discapacitado, VictimaArmado, LGBTI, CodiAdmin, CodiRegis, FecRegis)
VALUES
(@HistorPaci, @TipoIden, @NumIden, @Nombre1, @Nombre2, @Apellido1, @Apellido2, @FechaNaci,
@CodPaisNace, @CodDptoNace, @CodCiuNace, @CodDpto, @CodMuni, @BarrioVive,
@DirecResi, @TelResi, @ZonaResiden, @EstadoCivil, @TipoUsar, @TipoAfiliado, @NumAfilia,
@EstraNum, @GrupoEtni, @CodAdmin, @Ocupacion, @Observaciones,
@Discapacitado, @VictimaArmado, @LGBTI, @CodiAdmin, @CodiRegis, @FecRegis)";

                    using (var cmd = new SqlCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@HistorPaci", model.HistorPaci ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoIden", model.TipoIden ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NumIden", model.NumIden ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Nombre1", model.Nombre1 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Nombre2", model.Nombre2 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido1", model.Apellido1 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Apellido2", model.Apellido2 ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaNaci", model.FechaNaci);
                        cmd.Parameters.AddWithValue("@CodPaisNace", model.CodPaisNace ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CodDptoNace", model.CodDptoNace ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CodCiuNace", model.CodCiuNace ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@CodDpto", model.CodDpto ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CodMuni", model.CodMuni ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@BarrioVive", model.BarrioVive ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@DirecResi", model.DirecResi ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TelResi", model.TelResi ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ZonaResiden", model.ZonaResiden ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EstadoCivil", model.EstadoCivil ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoUsar", model.TipoUsar ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoAfiliado", model.TipoAfiliado ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NumAfilia", model.NumAfilia ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@EstraNum", model.EstraNum ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@GrupoEtni", model.GrupoEtni ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CodAdmin", model.CodAdmin ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ocupacion", model.Ocupacion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Observaciones", model.Observaciones ?? (object)DBNull.Value);

                        cmd.Parameters.AddWithValue("@Discapacitado", model.Discapacitado);
                        cmd.Parameters.AddWithValue("@VictimaArmado", model.VictimaArmado);
                        cmd.Parameters.AddWithValue("@LGBTI", model.LGBTI);

                        cmd.Parameters.AddWithValue("@CodiAdmin", model.CodiAdmin ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CodiRegis", model.CodiRegis ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FecRegis", model.FecRegis);

                        cmd.ExecuteNonQuery();
                    }
                }

                TempData["Success"] = $"Paciente guardado correctamente (Historia: {model.HistorPaci}).";
                return RedirectToAction("Crear");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error al guardar el paciente: " + ex.Message);
                RecargarListas(model);
                return View(model);
            }
        }


        // ======================================================
        // AJAX
        // ======================================================

        [HttpGet]
        public IActionResult GetDepartamentos(string pais)
        {
            var list = new List<SelectListItem>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                string q = "SELECT CodigoDpto, NombreDpto FROM Datos_de_los_Dpto_o_Estados WHERE CodigoPais = @pais ORDER BY NombreDpto";
                using var cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@pais", pais ?? "");

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = r["CodigoDpto"].ToString(),
                        Text = r["NombreDpto"].ToString()
                    });
                }
            }
            catch { }

            return Json(list);
        }


        [HttpGet]
        public IActionResult GetCiudades(string dpto)
        {
            var list = new List<SelectListItem>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                string q = "SELECT CodigoCiudad, NombreCiudad FROM Datos_de_las_ciudades WHERE CodigoDpto = @dpto ORDER BY NombreCiudad";
                using var cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@dpto", dpto ?? "");

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = r["CodigoCiudad"].ToString(),
                        Text = r["NombreCiudad"].ToString()
                    });
                }
            }
            catch { }

            return Json(list);
        }


        [HttpGet]
        public IActionResult GetBarrios(string ciudad)
        {
            var list = new List<SelectListItem>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                string q = "SELECT CodBarrio, NomBarrio FROM Datos_de_los_barrios_o_veredas WHERE CodCiudad = @ciudad ORDER BY NomBarrio";
                using var cmd = new SqlCommand(q, conn);

                // Corrección importante
                cmd.Parameters.AddWithValue("@ciudad", ciudad ?? "");

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = r["CodBarrio"].ToString(),
                        Text = r["NomBarrio"].ToString()
                    });
                }
            }
            catch { }

            return Json(list);
        }


        [HttpGet]
        public IActionResult BuscarPaciente(string historPaci)
        {
            if (string.IsNullOrWhiteSpace(historPaci))
                return Json(null);

            PacienteModel paciente = null;

            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                string sql = @"SELECT TOP 1 * FROM Datos_del_Paciente WHERE HistorPaci = @HistorPaci";
                using var cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@HistorPaci", historPaci);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    paciente = new PacienteModel
                    {
                        HistorPaci = reader["HistorPaci"].ToString(),
                        TipoIden = reader["TipoIden"].ToString(),
                        NumIden = reader["NumIden"].ToString(),
                        Nombre1 = reader["Nombre1"].ToString(),
                        Nombre2 = reader["Nombre2"].ToString(),
                        Apellido1 = reader["Apellido1"].ToString(),
                        Apellido2 = reader["Apellido2"].ToString(),
                        FechaNaci = Convert.ToDateTime(reader["FechaNaci"]),
                        CodPaisNace = reader["CodPaisNace"].ToString(),
                        CodDptoNace = reader["CodDptoNace"].ToString(),
                        CodCiuNace = reader["CodCiuNace"].ToString(),
                        CodDpto = reader["CodDpto"].ToString(),
                        CodMuni = reader["CodMuni"].ToString(),
                        BarrioVive = reader["BarrioVive"].ToString(),
                        DirecResi = reader["DirecResi"].ToString(),
                        TelResi = reader["TelResi"].ToString(),
                        ZonaResiden = reader["ZonaResiden"].ToString(),
                        EstadoCivil = reader["EstadoCivil"].ToString(),
                        TipoUsar = reader["TipoUsar"].ToString(),
                        TipoAfiliado = reader["TipoAfiliado"].ToString(),
                        NumAfilia = reader["NumAfilia"].ToString(),
                        EstraNum = reader["EstraNum"].ToString(),
                        GrupoEtni = reader["GrupoEtni"].ToString(),
                        CodAdmin = reader["CodAdmin"].ToString(),
                        Ocupacion = reader["Ocupacion"].ToString(),
                        Observaciones = reader["Observaciones"].ToString(),
                        Discapacitado = Convert.ToBoolean(reader["Discapacitado"]),
                        VictimaArmado = Convert.ToBoolean(reader["VictimaArmado"]),
                        LGBTI = Convert.ToBoolean(reader["LGBTI"])
                    };
                }
            }
            catch
            {
                return Json(null);
            }

            return Json(paciente);
        }


        // ======================================================
        // Helpers
        // ======================================================

        private void RecargarListas(PacienteModel model)
        {
            ViewBag.TipoIden = GetLookupList("Datos_documentos_indentificacion", "CodIdenti", "NomIdenti");
            ViewBag.Paises = GetLookupList("Datos_de_paises", "CodiPais", "NomPais");
            ViewBag.Departamentos = GetLookupList("Datos_de_los_Dpto_o_Estados", "CodigoDpto", "NombreDpto");
            ViewBag.Ciudades = GetLookupListFilteredCiudades(model.CodDpto);
            ViewBag.Barrios = GetLookupListFilteredBarrios(model.CodMuni);
            ViewBag.EstadosCiviles = GetLookupList("Datos_estado_civil", "CodEstado", "NomEstado");
            ViewBag.TiposUsuarios = GetLookupList("Datos_tipos_de_usuarios", "CodTipoUsuar", "NomTipo");
            ViewBag.Etnia = GetLookupList("Datos_grupos_etnicos", "CodiGrupo", "NomAten");
            ViewBag.Administradoras = GetLookupList("Datos_administradoras_de_planes", "CodInterno", "NomAdmin");
            ViewBag.Estratos = GetFixedListEstrato();

            ViewBag.Zonas = new List<SelectListItem>
            {
                new SelectListItem { Value = "U", Text = "Urbano" },
                new SelectListItem { Value = "R", Text = "Rural" }
            };
        }


        private List<SelectListItem> GetLookupList(string tableName, string valueField, string textField)
        {
            var list = new List<SelectListItem>();
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                string q = $"SELECT {valueField}, {textField} FROM {tableName} ORDER BY {textField}";
                using var cmd = new SqlCommand(q, conn);

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = r[valueField].ToString(),
                        Text = r[textField].ToString()
                    });
                }
            }
            catch { }

            return list;
        }

        private List<SelectListItem> GetLookupListFilteredCiudades(string codigoDpto)
        {
            var list = new List<SelectListItem>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                string q = "SELECT CodigoCiudad, NombreCiudad FROM Datos_de_las_ciudades WHERE CodigoDpto = @dpto ORDER BY NombreCiudad";
                using var cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@dpto", codigoDpto ?? "");

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = r["CodigoCiudad"].ToString(),
                        Text = r["NombreCiudad"].ToString()
                    });
                }
            }
            catch { }

            return list;
        }

        private List<SelectListItem> GetLookupListFilteredBarrios(string codigoCiudad)
        {
            var list = new List<SelectListItem>();

            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                string q = "SELECT CodBarrio, NomBarrio FROM Datos_de_los_barrios_o_veredas WHERE CodCiudad = @ciudad ORDER BY NomBarrio";
                using var cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@ciudad", codigoCiudad ?? "");

                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new SelectListItem
                    {
                        Value = r["CodBarrio"].ToString(),
                        Text = r["NomBarrio"].ToString()
                    });
                }
            }
            catch { }

            return list;
        }

        private List<SelectListItem> GetFixedListEstrato()
        {
            var list = new List<SelectListItem>();
            for (int i = 0; i <= 6; i++)
                list.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });

            return list;
        }
    }
}

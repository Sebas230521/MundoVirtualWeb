using System;

namespace CitasMV.Models
{
    public class EmpresaTercero
    {
        /*public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string DV { get; set; }

        public string Convenio { get; set; }
        public string TipoAdmin { get; set; }
        public string SGSSS { get; set; }
        public string Mtarifario { get; set; }
        public string Tarifas { get; set; }
        public string CUPS { get; set; }

        public string Regimen { get; set; }
        public string Contable { get; set; }

        public string Contacto { get; set; }
        public string Direccion { get; set; }
        public string Telefonos { get; set; }
        public string Celular { get; set; }
        public string Fax { get; set; }
        public string Opto { get; set; }
        public string Ciudad { get; set; }

        public bool Habilitada { get; set; }
        public string Nivel1 { get; set; }
        public string Nivel2 { get; set; }
        public string Nivel3 { get; set; }

        public string Actividades { get; set; }
        */

    }

    public class Departamento
    {
        public string? CodigoDpto { get; set; }
        public string? CodigoPais { get; set; }   // ← corregido
        public string? NombreDpto { get; set; }
    }
    public class Ciudad
    {
        public int ConseCodigo { get; set; }
        public string CodigoCiudad { get; set; } = string.Empty;
        public string NombreCiudad { get; set; } = string.Empty;
    }
}

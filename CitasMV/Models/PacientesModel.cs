using System;
using System.ComponentModel.DataAnnotations;

namespace CitasMV.Models
{
    // Modelo simplificado para el formulario de creación de pacientes.
    // Incluye los campos que usaremos en la vista y en el INSERT.
    public class PacienteModel
    {
        // PK que digita el usuario (nvarchar(15) en la BD)
        [Required(ErrorMessage = "La historia clínica es obligatoria.")]
        [Display(Name = "Historia Paciente")]
        public string? HistorPaci { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es obligatorio.")]
        [Display(Name = "Tipo Identificación")]
        public string? TipoIden { get; set; } = "CC";

        [Required(ErrorMessage = "Número de identificación obligatorio.")]
        [Display(Name = "Número Identificación")]
        public string? NumIden { get; set; }

        [Required(ErrorMessage = "Primer nombre obligatorio.")]
        [Display(Name = "Primer Nombre")]
        public string? Nombre1 { get; set; }

        [Display(Name = "Segundo Nombre")]
        public string? Nombre2 { get; set; } // opcional

        [Required(ErrorMessage = "Primer apellido obligatorio.")]
        [Display(Name = "Primer Apellido")]
        public string? Apellido1 { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string? Apellido2 { get; set; } // opcional

        [Required(ErrorMessage = "Fecha de nacimiento obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNaci { get; set; } = DateTime.Today;

        // Lugar de nacimiento
        [Display(Name = "País Nacimiento")]
        public string? CodPaisNace { get; set; } = "001";

        [Display(Name = "Departamento Nacimiento")]
        public string? CodDptoNace { get; set; }

        [Display(Name = "Ciudad Nacimiento")]
        public string? CodCiuNace { get; set; }

        // Residencia actual
        [Display(Name = "Departamento Residencia")]
        public string? CodDpto { get; set; }

        [Display(Name = "Ciudad Residencia (CódigoCiudad)")]
        public string? CodMuni { get; set; } // será CodigoCiudad

        [Display(Name = "Barrio")]
        public string? BarrioVive { get; set; } // CodBarrio

        [Display(Name = "Dirección Residencia")]
        public string? DirecResi { get; set; }

        [Display(Name = "Teléfono Residencia")]
        public string? TelResi { get; set; }

        [Display(Name = "Zona Residencia")]
        public string? ZonaResiden { get; set; } = "U"; // U / R

        // Demográficos / socioeconómicos
        [Display(Name = "Estado Civil")]
        public string? EstadoCivil { get; set; } = "T";

        [Display(Name = "Tipo Usuario")]
        public string? TipoUsar { get; set; } = "2";

        [Display(Name = "Tipo Afiliado")]
        public string? TipoAfiliado { get; set; }

        [Display(Name = "Número Afiliación")]
        public string? NumAfilia { get; set; }

        [Display(Name = "Estrato")]
        public string? EstraNum { get; set; } // 0..6

        [Display(Name = "Etnia")]
        public string? GrupoEtni { get; set; } = "6";

        // EPS / Administradora
        [Display(Name = "Administradora (EPS)")]
        public string? CodAdmin { get; set; } // CodInterno de Datos_administradoras_de_planes

        // Booleanos
        [Display(Name = "Discapacitado")]
        public bool Discapacitado { get; set; } = false;

        [Display(Name = "Víctima conflicto armado")]
        public bool VictimaArmado { get; set; } = false;

        [Display(Name = "Población LGBTI")]
        public bool LGBTI { get; set; } = false;

        [Display(Name = "Ocupación")]
        public string? Ocupacion { get; set; }

        [Display(Name = "Observaciones")]
        public string? Observaciones { get; set; } // opcional

        [Display(Name = "Sexo")]
        public string? Sexo { get; set; } // opcional


        // Auditoría mínima
        public string? CodiRegis { get; set; } = "001";
        public DateTime FecRegis { get; set; } = DateTime.Now;

        // Valores de administración
        public string? CodiAdmin { get; set; } = "0001";

    }
}

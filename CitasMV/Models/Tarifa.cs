namespace CitasMV.Models
{
    public class Tarifa
    {
        public int Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? ManualTarifario { get; set; }
        public string? Signo { get; set; }
        public decimal Porcentaje { get; set; }
        public string? RegistradoPor { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string? ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}

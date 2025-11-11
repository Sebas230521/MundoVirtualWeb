namespace CitasMV.Models
{
    public class Usuario
    {
        public int SecretoUsa { get; set; }
        public string? NombreEntrada { get; set; }
        public string? CodigoUsa { get; set; }
        public string? IdentificUsa { get; set; }
        public string? NombreUsa { get; set; }
        public string? Apellido1Usa { get; set; }
        public string? Apellido2Usa { get; set; }
        public int? Sucursal { get; set; }
        public string? CargoUsa { get; set; }
        public int? NivelPermiso { get; set; }
        public bool Vigente { get; set; }
    }
}

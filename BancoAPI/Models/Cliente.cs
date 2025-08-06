namespace BancoAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public decimal Ingresos { get; set; }

    //Relación 1:n, un cliente puede tener muchas cuentas
        public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();
    }
}
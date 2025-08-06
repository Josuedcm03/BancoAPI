namespace BancoAPI.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public decimal Saldo { get; set; } // saldo de la cuenta inicial

        // Relación n:1, muchas cuentas pueden pertenecer a un cliente
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        
        public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
    }
}
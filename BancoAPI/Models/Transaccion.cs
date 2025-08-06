namespace BancoAPI.Models
{

    public enum TipoTransaccion
    {
        Deposito,
        Retiro
    }

    public class Transaccion
    {
        public int Id { get; set; }
        public TipoTransaccion Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal SaldoDespues { get; set; }
        public int CuentaId { get; set; } // cuenta asociada a la transacción
        public Cuenta? Cuenta { get; set; } // referencia a la cuenta asociada
    }
}
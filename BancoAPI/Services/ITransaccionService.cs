using BancoAPI.Models;

namespace BancoAPI.Services;
public interface ITransaccionService
{
    Task<IEnumerable<Transaccion>> ObtenerPorCuenta(int cuentaId);
    Task<Transaccion?> Crear(Transaccion transaccion);
}

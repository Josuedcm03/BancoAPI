using BancoAPI.Models;


namespace BancoAPI.Services;

public interface ICuentaService
{
    Task<IEnumerable<Cuenta>> ObtenerTodos();
    Task<Cuenta?> ObtenerPorId(int id);
    Task<Cuenta> Crear(Cuenta cuenta);
    Task<Cuenta?> Actualizar(int id, Cuenta cuenta);
    Task<bool> Eliminar(int id);
}
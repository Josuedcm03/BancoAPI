using BancoAPI.Models;

namespace BancoAPI.Services;

public interface IClienteService
{
    Task<IEnumerable<Cliente>> ObtenerTodos();
    Task<Cliente?> ObtenerPorId(int id);
    Task<Cliente> Crear(Cliente cliente);
    Task<Cliente?> Actualizar(int id, Cliente cliente);
    Task<bool> Eliminar(int id);
}

using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Services;

public class ClienteService : IClienteService
{
    private readonly BancoDbContext _context;

    public ClienteService(BancoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> ObtenerTodos()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> ObtenerPorId(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente> Crear(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente?> Actualizar(int id, Cliente cliente)
    {
        var existente = await _context.Clientes.FindAsync(id);
        if (existente == null) return null;

        existente.Nombre = cliente.Nombre;
        existente.FechaNacimiento = cliente.FechaNacimiento;
        existente.Sexo = cliente.Sexo;
        existente.Ingresos = cliente.Ingresos;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> Eliminar(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }
}

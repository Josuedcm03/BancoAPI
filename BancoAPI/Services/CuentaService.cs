using BancoAPI.Models;
using BancoAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Services;

public class CuentaService : ICuentaService
{
    private readonly BancoDbContext _context;

    public CuentaService(BancoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cuenta>> ObtenerTodos()
    {
        return await _context.Cuentas
            .Include(c => c.Cliente) // Incluye info del cliente
            .Include(c => c.Transacciones) // Incluye transacciones
            .ToListAsync();
    }

    public async Task<Cuenta?> ObtenerPorId(int id)
    {
        return await _context.Cuentas
            .Include(c => c.Cliente)
            .Include(c => c.Transacciones)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<decimal?> ConsultarSaldo(int cuentaId)
    {
        var cuenta = await _context.Cuentas.FindAsync(cuentaId);
        return cuenta?.Saldo;
    }

    public async Task<Cuenta> Crear(Cuenta cuenta)
    {
        _context.Cuentas.Add(cuenta);
        await _context.SaveChangesAsync();
        return cuenta;
    }

    public async Task<Cuenta?> Actualizar(int id, Cuenta cuenta)
    {
        var existente = await _context.Cuentas.FindAsync(id);
        if (existente == null) return null;

        existente.NumeroCuenta = cuenta.NumeroCuenta;
        existente.Saldo = cuenta.Saldo;
        existente.ClienteId = cuenta.ClienteId;

        await _context.SaveChangesAsync();
        return existente;
    }

    public async Task<bool> Eliminar(int id)
    {
        var cuenta = await _context.Cuentas.FindAsync(id);
        if (cuenta == null) return false;

        _context.Cuentas.Remove(cuenta);
        await _context.SaveChangesAsync();
        return true;
    }
}
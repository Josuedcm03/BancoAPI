using BancoAPI.Data;
using BancoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Services;


public class TransaccionService : ITransaccionService
{
    private readonly BancoDbContext _context;

    public TransaccionService(BancoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaccion>> ObtenerPorCuenta(int cuentaId)
    {
        return await _context.Transacciones
            .Where(t => t.CuentaId == cuentaId)
            .OrderByDescending(t => t.Fecha)
            .ToListAsync();
    }

    public async Task<Transaccion?> Crear(Transaccion transaccion)
    {
        var cuenta = await _context.Cuentas.FindAsync(transaccion.CuentaId);
        if (cuenta == null) return null;

        decimal saldoActual = cuenta.Saldo;

        // Obtener último saldo registrado (si hay transacciones previas)
        var ultimaTransaccion = await _context.Transacciones
            .Where(t => t.CuentaId == cuenta.Id)
            .OrderByDescending(t => t.Fecha)
            .FirstOrDefaultAsync();

        if (ultimaTransaccion != null)
            saldoActual = ultimaTransaccion.SaldoDespues;

        if (transaccion.Tipo == TipoTransaccion.Retiro)
        {
            if (transaccion.Monto > saldoActual)
                throw new InvalidOperationException("Fondos insuficientes para el retiro.");

            transaccion.SaldoDespues = saldoActual - transaccion.Monto;
        }
        else if (transaccion.Tipo == TipoTransaccion.Deposito)
        {
            transaccion.SaldoDespues = saldoActual + transaccion.Monto;
        }

        // Actualizar el saldo de la cuenta, esto no lo tenía y el valor no se actualizaba en la cuenta solo en el registro de la transacción
        cuenta.Saldo = transaccion.SaldoDespues;

        _context.Transacciones.Add(transaccion);
        await _context.SaveChangesAsync();
        return transaccion;
    }
}
using Microsoft.EntityFrameworkCore;
using BancoAPI.Models;

namespace BancoAPI.Data;

// Esta clase representa el contexto principal de la base de datos
public class BancoDbContext : DbContext
{
    // Constructor que recibe las opciones de configuración del contexto
    public BancoDbContext(DbContextOptions<BancoDbContext> options)
        : base(options)
    {
    }

    // Estas propiedades representan las tablas en la base de datos
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Cuenta> Cuentas => Set<Cuenta>();
    public DbSet<Transaccion> Transacciones => Set<Transaccion>();
}

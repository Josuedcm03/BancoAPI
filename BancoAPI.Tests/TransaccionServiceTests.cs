using Xunit;
using BancoAPI.Models;
using BancoAPI.Services;
using BancoAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

public class TransaccionServiceTests
{
    [Fact] // test 1
    public async Task Deposito_DeberiaAumentarSaldoYRegistrarTransaccion()
    {
        // Base de datos en memoria
        // crear la cuenta con un saldo inicial 
        // Instanciar el servicio de transacciones
        var options = new DbContextOptionsBuilder<BancoDbContext>()
            .UseInMemoryDatabase(databaseName: "DepositoTest").Options;
        using var context = new BancoDbContext(options);
        var cuenta = new Cuenta { Id = 1, NumeroCuenta = "123", Saldo = 1000, ClienteId = 4 };
        context.Cuentas.Add(cuenta);
        context.SaveChanges();
        var service = new TransaccionService(context);
        var transaccion = new Transaccion { Tipo = TipoTransaccion.Deposito, Monto = 500, CuentaId = 1 };

        // Ejecución del método
        var resultado = await service.Crear(transaccion);

        // Comprobación de los cambios actualizados del saldo
        Assert.Equal(1500, resultado.SaldoDespues);
        Assert.Equal(1500, cuenta.Saldo);
    }

    [Fact] // test 2
    public async Task Retiro_DeberiaDisminuirSaldoYRegistrarTransaccion()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BancoDbContext>()
            .UseInMemoryDatabase(databaseName: "RetiroTest").Options;
        using var context = new BancoDbContext(options);
        var cuenta = new Cuenta { Id = 1, NumeroCuenta = "123", Saldo = 1000, ClienteId = 4 };
        context.Cuentas.Add(cuenta);
        context.SaveChanges();
        var service = new TransaccionService(context);
        var transaccion = new Transaccion { Tipo = TipoTransaccion.Retiro, Monto = 200, CuentaId = 1 };

        // Act
        var resultado = await service.Crear(transaccion);

        // Assert
        Assert.Equal(800, resultado.SaldoDespues);
        Assert.Equal(800, cuenta.Saldo);
    }

    [Fact] // test 3
    public async Task Retiro_DeberiaLanzarExcepcionSiFondosInsuficientes()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BancoDbContext>()
            .UseInMemoryDatabase(databaseName: "RetiroFondosInsuficientesTest").Options;
        using var context = new BancoDbContext(options);
        var cuenta = new Cuenta { Id = 1, NumeroCuenta = "123", Saldo = 100, ClienteId = 4 };
        context.Cuentas.Add(cuenta);
        context.SaveChanges();
        var service = new TransaccionService(context);
        var transaccion = new Transaccion { Tipo = TipoTransaccion.Retiro, Monto = 200, CuentaId = 1 };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => service.Crear(transaccion));
    }

    [Fact] // test 4
    public async Task ObtenerPorCuenta_DeberiaRetornarHistorialDeTransacciones()
    {
        // Se configuran los datos necesatios para la prueba
        var options = new DbContextOptionsBuilder<BancoDbContext>()
            .UseInMemoryDatabase(databaseName: "HistorialTest").Options;
        using var context = new BancoDbContext(options);
        var cuenta = new Cuenta { Id = 1, NumeroCuenta = "123", Saldo = 1000, ClienteId = 4 };
        context.Cuentas.Add(cuenta);
        context.Transacciones.Add(new Transaccion { Tipo = TipoTransaccion.Deposito, Monto = 500, CuentaId = 1, Fecha = DateTime.Now });
        context.Transacciones.Add(new Transaccion { Tipo = TipoTransaccion.Retiro, Monto = 200, CuentaId = 1, Fecha = DateTime.Now });
        context.SaveChanges();
        var service = new TransaccionService(context);

        // Se llama al método del servicio
        var historial = await service.ObtenerPorCuenta(1);

        // Verificacion del resultado
        Assert.Equal(2, historial.Count());
    }

    [Fact] // test 5
    public void AplicarInteres_DeberiaActualizarSaldo()
    {
        // Se configuran los datos necesarios para la prueba
        var cuenta = new Cuenta { Saldo = 1000 };
        var tasaInteres = 0.05m; // 5%

        // Se llama al método del servicio
        cuenta.Saldo += cuenta.Saldo * tasaInteres;

        // Verificacion del resultado
        Assert.Equal(1050, cuenta.Saldo);
    }
}

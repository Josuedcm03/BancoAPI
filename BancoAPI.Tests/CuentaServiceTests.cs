using Xunit;
using Moq;
using BancoAPI.Models;
using BancoAPI.Services;
using BancoAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class CuentaServiceTests
{
    [Fact] // test 1
    public async Task CrearCuenta_DeberiaCrearCuentaCorrectamente()
    {
        // Datos necearios para la prueba
        var options = new DbContextOptionsBuilder<BancoDbContext>()
            .UseInMemoryDatabase(databaseName: "CrearCuentaTest").Options;
        using var context = new BancoDbContext(options);
        var service = new CuentaService(context);
        var cuenta = new Cuenta { NumeroCuenta = "1234567890", Saldo = 1000, ClienteId = 4 };

        // Ejecutar el método a probar
        var resultado = await service.Crear(cuenta);

        // Verifica que el resultado no sea nulo y que los datos sean correctos
        Assert.NotNull(resultado);
        Assert.Equal("1234567890", resultado.NumeroCuenta);
        Assert.Equal(1000, resultado.Saldo);
    }

    [Fact] // test 2
    public async Task ConsultarSaldo_DeberiaRetornarSaldoActual()
    {
        // Se configura la base de datos enb memoria y se crea el contexto, 
        // agregando una cuenta con saldo específico y se guardan los datos, creando el servicio 
        var options = new DbContextOptionsBuilder<BancoDbContext>()
            .UseInMemoryDatabase(databaseName: "ConsultarSaldoTest").Options;
        using var context = new BancoDbContext(options);
        var cuenta = new Cuenta { Id = 1, NumeroCuenta = "1234567890", Saldo = 1500, ClienteId = 4 };
        context.Cuentas.Add(cuenta);
        context.SaveChanges();
        var service = new CuentaService(context);

        // Llamar al metodo mediante el id de la cuenta
        var saldo = await service.ConsultarSaldo(1);

        // Verificacion del resultado
        Assert.Equal(1500, saldo);
    }
}

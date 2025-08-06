using BancoAPI.Models;
using BancoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransaccionController : ControllerBase
{
    private readonly ITransaccionService _transaccionService;

    public TransaccionController(ITransaccionService transaccionService)
    {
        _transaccionService = transaccionService;
    }

    // GET: api/transaccion/cuenta/5
    [HttpGet("cuenta/{cuentaId}")]
    public async Task<ActionResult<IEnumerable<Transaccion>>> GetPorCuenta(int cuentaId)
    {
        var transacciones = await _transaccionService.ObtenerPorCuenta(cuentaId);
        return Ok(transacciones);
    }

    // POST: api/transaccion
    [HttpPost]
    public async Task<ActionResult<Transaccion>> CrearTransaccion(Transaccion transaccion)
    {
        try
        {
            var creada = await _transaccionService.Crear(transaccion);
            if (creada == null)
                return NotFound("Cuenta no encontrada");

            return CreatedAtAction(nameof(GetPorCuenta), new { cuentaId = creada.CuentaId }, creada);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

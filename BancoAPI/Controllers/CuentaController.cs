using BancoAPI.Models;
using BancoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentaController : ControllerBase
{
    private readonly ICuentaService _cuentaService;

    public CuentaController(ICuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }

    // GET: api/cuenta
    // Devuelve todas las cuentas registradas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuentas()
    {
        var cuentas = await _cuentaService.ObtenerTodos();
        return Ok(cuentas);
    }

    // GET: api/cuenta/{id}
    // Devuelve una cuenta por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Cuenta>> GetCuenta(int id)
    {
        var cuenta = await _cuentaService.ObtenerPorId(id);
        if (cuenta == null) return NotFound();
        return Ok(cuenta);
    }

    // POST: api/cuenta
    // Crea una nueva cuenta asociada a un cliente existente
    [HttpPost]
    public async Task<ActionResult<Cuenta>> CrearCuenta(Cuenta cuenta)
    {
        var creada = await _cuentaService.Crear(cuenta);
        return CreatedAtAction(nameof(GetCuenta), new { id = creada.Id }, creada);
    }

    // PUT: api/cuenta/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<Cuenta>> ActualizarCuenta(int id, Cuenta cuenta)
    {
        var actualizada = await _cuentaService.Actualizar(id, cuenta);
        if (actualizada == null) return NotFound();
        return Ok(actualizada);
    }
    
    // DELETE: api/cuenta/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarCuenta(int id)
    {
        var eliminada = await _cuentaService.Eliminar(id);
        if (!eliminada) return NotFound();
        return NoContent();
    }

}
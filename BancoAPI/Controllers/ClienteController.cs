using BancoAPI.Models;
using BancoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    // GET: api/cliente
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        var clientes = await _clienteService.ObtenerTodos();
        return Ok(clientes);
    }

    // GET: api/cliente/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {
        var cliente = await _clienteService.ObtenerPorId(id);
        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    // POST: api/cliente
    [HttpPost]
    public async Task<ActionResult<Cliente>> CrearCliente(Cliente cliente)
    {
        var creado = await _clienteService.Crear(cliente);
        return CreatedAtAction(nameof(GetCliente), new { id = creado.Id }, creado);
    }

    // PUT: api/cliente/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<Cliente>> ActualizarCliente(int id, Cliente cliente)
    {
        var actualizado = await _clienteService.Actualizar(id, cliente);
        if (actualizado == null)
            return NotFound();

        return Ok(actualizado);
    }

    // DELETE: api/cliente/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarCliente(int id)
    {
        var eliminado = await _clienteService.Eliminar(id);
        if (!eliminado)
            return NotFound();

        return NoContent();
    }
}

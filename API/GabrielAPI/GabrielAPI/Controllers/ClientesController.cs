using GabrielAPI.Contexts;
using GabrielAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GabrielAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly MainContext ctx;
        
        public ClientesController(MainContext context)
        {
            ctx = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ctx.Clientes.ToList());
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = ctx.Clientes.Where(c => c.Id == id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Dados inválidos ou nulos.");

            bool existe = ctx.Clientes.Any(c => c.Email == cliente.Email || c.Nome == cliente.Nome);

            if (existe)
            {
                return Conflict("Nome ou email de usuário já registrados.");
            }

            ctx.Clientes.Add(cliente);
            ctx.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente clienteNovo)
        {
            if (clienteNovo == null || id != clienteNovo.Id)
                return BadRequest("Dados inválidos.");

            var clienteExistente = ctx.Clientes.FirstOrDefault(c => c.Id == id);

            if (clienteExistente == null)
                return NotFound("Usuário não encontrado.");
            
            clienteExistente.Nome = clienteNovo.Nome;
            clienteExistente.Email = clienteNovo.Email;
            clienteExistente.Pedidos = clienteNovo.Pedidos;

            ctx.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Cliente clienteNovo)
        {
            var clienteExistente = ctx.Clientes.FirstOrDefault(c => c.Id == id);

            if (clienteExistente == null)
                return NotFound("Usuário não encontrado.");

            if (!string.IsNullOrEmpty(clienteNovo.Nome))
                clienteExistente.Nome = clienteNovo.Nome;

            if (!string.IsNullOrEmpty(clienteNovo.Email))
                clienteExistente.Email = clienteNovo.Email;

            if (clienteNovo.Pedidos != null)
                clienteExistente.Pedidos = clienteNovo.Pedidos;

            ctx.SaveChanges();

            return NoContent();
        }
    }
}

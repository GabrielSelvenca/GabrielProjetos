using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WS_Tower_api.Contexts;
using WS_Tower_api.Models;

namespace WS_Tower_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class Assets : ControllerBase
    {
        MainContext ctx = new MainContext();

        [HttpGet("usuarios")]
        public IActionResult GetUsers()
        {
            return Ok(ctx.Usuarios.ToList());
        }

        [HttpGet("usuarios/{id}")]
        public IActionResult GetUsersByID(int id)
        {
            var user = ctx.Usuarios.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] Usuario user)
        {
            var userEncontrado = ctx.Usuarios.FirstOrDefault(u => u.Email == user.Email && u.Senha == u.Senha);

            if (userEncontrado == null)
                return Unauthorized("Email ou senha errados.");

            return Ok(userEncontrado);
        }

        [HttpGet("relatos")]
        public IActionResult GetRelatos()
        {
            return Ok(ctx.Relatos.ToList());
        }

        [HttpGet("relatos/{id}")]
        public IActionResult GetRelatosByID(int id)
        {
            var relatoEncontrado = ctx.Relatos.Where(r => r.Id == id);

            if (relatoEncontrado == null)
                return NotFound("ID de relato inválido.");

            return Ok(relatoEncontrado);
        }

        [HttpPut("relatar")]
        public async Task<IActionResult> CriarRelato([FromBody] Relato relato)
        {
            //if (relato.Relato1 == null || imagem == null)
            //    return BadRequest("Valores incompletos.");

            var nomeDoArquivo = $"{Guid.NewGuid()}.jpg";
            var caminho = Path.Combine($"wwroot/uploads/{nomeDoArquivo}");

            using var stream = new FileStream(caminho, FileMode.Create);
            //await imagem.CopyToAsync(stream);

            var novoRelato = new Relato
            {
                Relato1 = relato.Relato1,
                Imagem = nomeDoArquivo,
                Latitude = relato.Latitude,
                Longitude = relato.Longitude,
                Usuarioid = relato.Usuarioid == null ? null : relato.Usuarioid
            };

            ctx.Relatos.Add(novoRelato);
            ctx.SaveChanges();
            return Ok(novoRelato);
        }

        [HttpDelete("relato/{id}")]
        public IActionResult DeleteRelato(int id)
        {
            var relatoExite = ctx.Relatos.FirstOrDefault(r => r.Id == id);
            if (relatoExite == null)
                return NotFound("Relato não existe");

            ctx.Relatos.Remove(relatoExite);
            ctx.SaveChanges();
            return Ok();
        }
    }
}

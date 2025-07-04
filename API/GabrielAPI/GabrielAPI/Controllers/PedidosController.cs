using GabrielAPI.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GabrielAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PedidosController : ControllerBase
    {
        private readonly MainContext ctx;

        public PedidosController(MainContext context)
        {
            ctx = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ctx.Pedidos.ToList());
        }
    }
}

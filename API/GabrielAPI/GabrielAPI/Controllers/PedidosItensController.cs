using GabrielAPI.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GabrielAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PedidosItensController : ControllerBase
    {
        private readonly MainContext ctx;

        public PedidosItensController(MainContext context)
        {
            ctx = context;
        }
         
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ctx.PedidoItens.ToList());
        }
    }
}

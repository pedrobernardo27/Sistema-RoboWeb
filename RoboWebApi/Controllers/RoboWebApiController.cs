using Microsoft.AspNetCore.Mvc;
using RoboWebApiService.Interface;

namespace RoboWebApi.Controllers
{
    public class RoboWebApiController : Controller
    {
        private readonly IRoboWebApiService _roboWebApiServices;

        public RoboWebApiController(IRoboWebApiService roboWebApiServices)
        {
            _roboWebApiServices = roboWebApiServices;
        }

        [HttpGet("BuscarCursoUdemy")]
        public async ValueTask<IActionResult> Get()
        {
            var cursos = await _roboWebApiServices.BuscarUdemyGoogle();
            return cursos.Any() ? Ok(cursos) : BadRequest();
        }       
    }
}

using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediReserva.Controllers  
{
    [ApiController]
    [Route("api/[controller]")]   

    public class ConsultorioController : ControllerBase
    {
        private readonly IConsultorioService _consultorioService;

        public ConsultorioController(IConsultorioService consultorioService)
        {
            _consultorioService = consultorioService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Consultorio>>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var consultorios = await _consultorioService.GetAllAsync();
            return Ok(new ApiResponse<List<Consultorio>>(consultorios, true, "La consultorios fueron consultados con exito"));
        }
    }
}

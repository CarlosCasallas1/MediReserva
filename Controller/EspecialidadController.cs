using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediReserva.Controllers  
{
    [ApiController]
    [Route("api/[controller]")]   

    public class EspecialidadController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadService;

        public EspecialidadController(IEspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Especialidad>>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var especialidades = await _especialidadService.GetAllAsync();
            return Ok(new ApiResponse<List<Especialidad>>(especialidades, true, "La especialidades fueron consultadas con exito"));
        }
    }
}

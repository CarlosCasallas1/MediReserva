using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediReserva.Controllers  
{
    [ApiController]
    [Route("api/[controller]")]   

    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<Medico>>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            var medicos = await _medicoService.GetAllAsync();
            return Ok(new ApiResponse<List<Medico>>(medicos, true, "Los medicos fueron consultadas con exito"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Medico>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            var medico = await _medicoService.GetByIdAsync(id);

            if (medico == null)
                return NotFound(new ApiResponse<Medico>(null, false, "El medico no ha sido encontrado"));

            return Ok(new ApiResponse<Medico>(medico, true, "El medico fue encontrado"));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Medico>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] Medico medico)
        {
            var medicoCreado = await _medicoService.CreateIdAsync(medico);

            if (medicoCreado == null)
            {
                return BadRequest(new ApiResponse<Medico>(
                    null,
                    false,
                    "El consultorio no se encuentra disponible"
                ));
            }


            return CreatedAtAction(
                nameof(GetById),
                new { id = medicoCreado.Id },
                new ApiResponse<Medico>(medicoCreado, true, "El medico fue creado")
            );
        }

        // Actualiza un médico y valida id y disponibilidad del consultorio
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Medico>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(int id, [FromBody] Medico medico)
        {
            if (id != medico.Id)
                return BadRequest(new ApiResponse<Medico>(null, false, "El id no coincide"));
            var actualizado = await _medicoService.UpdateAsync(medico);

            if (!actualizado)
                return BadRequest(new ApiResponse<Medico>(null, false, "No se pudo actualizar: médico no encontrado o consultorio ocupado"));
            return Ok(new ApiResponse<Medico>(medico, true, "Médico actualizado con éxito"));
        }


        [ProducesResponseType(typeof(ApiResponse<Medico>), 200)]
        [ProducesResponseType(500)]
        [HttpPut("inactivar/{id}")]
        public async Task<IActionResult> Inactivar(int id)
        {
            var resultado = await _medicoService.InactivarAsync(id);

            if (!resultado.Exitoso)
                return BadRequest(new { mensaje = resultado.Mensaje });

            return Ok(new { mensaje = resultado.Mensaje });
        }




    }
}

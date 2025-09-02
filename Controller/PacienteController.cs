using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MediReserva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pacientes = await _pacienteService.GetAllAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await _pacienteService.GetByIdAsync(id);
            if (paciente == null)
                return NotFound("Paciente no encontrado.");

            return Ok(paciente);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            var created = await _pacienteService.CreateIdAsync(paciente);

            if (created == null)
                return BadRequest("El documento ya está registrado para otro paciente.");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Paciente paciente)
        {
            if (id != paciente.Id)
                return BadRequest("El id no coincide con el paciente.");

            var updated = await _pacienteService.UpdateAsync(paciente);

            if (!updated)
                return NotFound("Paciente no encontrado.");

            return Ok("Paciente actualizado correctamente.");
        }

        [HttpPut("inactivar/{id}")]
        public async Task<IActionResult> Inactivar(int id)
        {
            var resultado = await _pacienteService.InactivarAsync(id);

            if (!resultado.Exitoso)
                return BadRequest(new { mensaje = resultado.Mensaje });

            return Ok(new { mensaje = resultado.Mensaje });
        }


    }
}

using MediReserva.Data;
using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediReserva.Services.Implementation
{
    public class PacienteService : IPacienteService
    {
        private readonly ApplicationDbContext _context;

        public PacienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Paciente>> GetAllAsync()
        {
            // Incluye las citas del paciente
            return await _context.Pacientes
                .Include(p => p.Cita)   // Trae las citas relacionadas
                .ToListAsync();
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _context.Pacientes
                .Include(p => p.Cita)   // Trae las citas relacionadas
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paciente?> CreateIdAsync(Paciente paciente)
        {
            var existe = await _context.Pacientes
                .AnyAsync(p => p.Documento == paciente.Documento);

            if (existe)
            {
                // devolvemos null → el controller decide qué mensaje mostrar
                return null;
            }

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return paciente;
        }

        public async Task<bool> UpdateAsync(Paciente paciente)
        {
            var pacienteExistente = await _context.Pacientes.FindAsync(paciente.Id);
            if (pacienteExistente == null) return false;

            // Validar documento duplicado con otro paciente
            var documentoDuplicado = await _context.Pacientes
                .AnyAsync(p => p.Documento == paciente.Documento && p.Id != paciente.Id);

            if (documentoDuplicado) return false; // El controller muestra el mensaje

            pacienteExistente.Nombre = paciente.Nombre;
            pacienteExistente.Apellido = paciente.Apellido;
            pacienteExistente.Documento = paciente.Documento;
            pacienteExistente.Email = paciente.Email;
            pacienteExistente.Telefono = paciente.Telefono;
            pacienteExistente.FechaNacimiento = paciente.FechaNacimiento;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<(bool Exitoso, string Mensaje)> InactivarAsync(int id)
        {
            var paciente = await _context.Pacientes
                .Include(p => p.Cita)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (paciente == null)
                return (false, "El paciente no existe.");

            if (paciente.Estado == false)
                return (false, "El paciente ya se encuentra en estado inactivo.");

            // Cambiar el estado del paciente a inactivo
            paciente.Estado = false;

            // Opcional: si quieres cancelar sus citas
            if (paciente.Cita.Any())
            {
                foreach (var cita in paciente.Cita)
                {
                    cita.Estado = "Cancelada";
                }
            }

            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();

            return (true, "Paciente inactivo.");
        }


    }
}

    using MediReserva.Data;
    using MediReserva.Models;
    using MediReserva.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    namespace MediReserva.Services.Implementation
    {
        public class MedicoService : IMedicoService
        {
            private readonly ApplicationDbContext _context;

            public MedicoService(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<Medico>> GetAllAsync()
            {
                return await _context.Medicos
                    .Include(m => m.Especialidad)
                    .Include(m => m.Consultorio)
                    .ToListAsync();
            }

            public async Task<Medico?> GetByIdAsync(int id)
            {
                return await _context.Medicos
                    .Include(m => m.Especialidad)
                    .Include(m => m.Consultorio)
                    .FirstOrDefaultAsync(m => m.Id == id);
            }
            // Service
            public async Task<Medico?> CreateIdAsync(Medico medico)
            {
                var consultorio = medico.ConsultorioId != null
                    ? await _context.Consultorios.FindAsync(medico.ConsultorioId)
                    : null;

                if (consultorio == null || !consultorio.Estado)
                    return null;

                consultorio.Estado = false;
                _context.Medicos.Add(medico);
                await _context.SaveChangesAsync();

                return medico;
            }

            //Modificación del método PUT (UpdateMedico) 
            // Actualiza un médico y gestiona la liberación/asignación de consultorios
            public async Task<bool> UpdateAsync(Medico medico)
            {
                var medicoExistente = await _context.Medicos
                    .Include(m => m.Consultorio)
                    .FirstOrDefaultAsync(m => m.Id == medico.Id);
                if (medicoExistente == null) return false;

                var consultorioAnterior = medicoExistente.Consultorio;

                if (medico.ConsultorioId != medicoExistente.ConsultorioId)
                {
                    if (consultorioAnterior != null) consultorioAnterior.Estado = true;

                    if (medico.ConsultorioId != null)
                    {
                        var nuevoConsultorio = await _context.Consultorios.FindAsync(medico.ConsultorioId);
                        if (nuevoConsultorio == null || !nuevoConsultorio.Estado) return false;
                        nuevoConsultorio.Estado = false;
                    }

                    medicoExistente.ConsultorioId = medico.ConsultorioId;
                }

                medicoExistente.Nombre = medico.Nombre;
                medicoExistente.EspecialidadId = medico.EspecialidadId;
                medicoExistente.Telefono = medico.Telefono;
                medicoExistente.Email = medico.Email;
                // Agregar aquí otros campos a actualizar

                await _context.SaveChangesAsync();
                return true;
            }




        // Se modificó el método DELETE para liberar el consultorio asignado al eliminar un médico
        public async Task<(bool Exitoso, string Mensaje)> InactivarAsync(int id)
        {
            var medico = await _context.Medicos
                .Include(m => m.Consultorio)
                .Include(m => m.Cita) // suponiendo que las citas se llaman Citas
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medico == null)
                return (false, "El médico no existe.");

            if (medico.Estado == false)
                return (false, "El médico ya se encuentra en estado inactivo.");

            // Cambiar el estado del médico a inactivo
            medico.Estado = false;

            // Liberar consultorio si tiene
            if (medico.Consultorio != null)
                medico.Consultorio.Estado = true;

            // Cancelar citas si tiene
            if (medico.Cita != null && medico.Cita.Any())
            {
                foreach (var cita in medico.Cita)
                {
                    cita.Estado = "Cancelada";
                }
            }

            _context.Medicos.Update(medico);
            await _context.SaveChangesAsync();

            return (true, "Médico inactivo.");
        }
    }
}
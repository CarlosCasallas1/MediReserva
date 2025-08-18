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


        public async Task<bool> UpdateAsync(Medico medico)
        {
            if (!_context.Medicos.Any(m => m.Id == medico.Id))
                return false;

            _context.Medicos.Update(medico);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null)
                return false;

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}




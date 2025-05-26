using MediReserva.Data;
using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediReserva.Services.Implementation
{
    public class EspecialidadService:IEspecialidadService
    {
        private readonly ApplicationDbContext _context;

        public EspecialidadService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Especialidad>> GetAllAsync()
        {
            return await _context.Especialidads.ToListAsync();
        }

    }
}




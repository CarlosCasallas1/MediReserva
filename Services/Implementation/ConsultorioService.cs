
using MediReserva.Data;
using MediReserva.Models;
using MediReserva.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediReserva.Services.Implementation
{
    public class ConsultorioService : IConsultorioService
    {
        private readonly ApplicationDbContext _context;

        public ConsultorioService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Consultorio>> GetAllAsync()
        {
            return await _context.Consultorios.ToListAsync();
        }

    }
}





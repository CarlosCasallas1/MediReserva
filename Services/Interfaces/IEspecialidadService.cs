using MediReserva.Models;

namespace MediReserva.Services.Interfaces
{
    public interface IEspecialidadService
    { 
     Task<List<Especialidad>> GetAllAsync();
    }
}

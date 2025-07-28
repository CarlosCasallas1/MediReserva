using MediReserva.Models;


namespace MediReserva.Services.Interfaces
{
    public interface IConsultorioService
    {
        Task<List<Consultorio>> GetAllAsync();
    }
}


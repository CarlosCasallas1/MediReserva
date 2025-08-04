using MediReserva.Models;

namespace MediReserva.Services.Interfaces
{
    public interface IMedicoService
    {
        //<summary>
        //retorna todos los medicos incluyendo sus relaciones 
        //</summary>
        Task<List<Medico>> GetAllAsync();

        //<summary>
        //retorna un medico por su id
        //</summary>
        Task<Medico?> GetByIdAsync();

        //<summary>
        //crear un nuevo medico
        //</summary>
        Task<Medico?> CreateAsync(Medico medico);

        //<summary>
        //actualizar medico existente
        //</summary>
        Task<bool> UpdateAsync(Medico medico);

        //<summary>
        //elimina medico por su id
        //</summary>
        Task<bool> DeleteAsync(int id);

    }
}


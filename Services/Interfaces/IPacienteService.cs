using MediReserva.Models;

namespace MediReserva.Services.Interfaces
{
    public interface IPacienteService
    {
        //<summary>
        // retorna todos los pacientes incluyendo sus relaciones 
        //</summary>
        Task<List<Paciente>> GetAllAsync();

        //<summary>
        // retorna un paciente por su id
        //</summary>
        Task<Paciente?> GetByIdAsync(int id);

        //<summary>
        // crear un nuevo paciente
        //</summary>
        Task<Paciente?> CreateIdAsync(Paciente paciente);

        //<summary>
        // actualizar paciente existente
        //</summary>
        Task<bool> UpdateAsync(Paciente paciente);

        //<summary>
        // inactivar paciente por su id (soft delete)
        //</summary>
        Task<(bool Exitoso, string Mensaje)> InactivarAsync(int id);

    }
}

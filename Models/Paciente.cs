using System.ComponentModel.DataAnnotations;

namespace MediReserva.Models
{
    public partial class Paciente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre no puede estar vacío y máximo 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El apellido no puede estar vacío y máximo 100 caracteres.")]
        public string Apellido { get; set; } = null!;

        [Required(ErrorMessage = "El documento es obligatorio.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El documento debe tener entre 5 y 20 caracteres.")]
        public string Documento { get; set; } = null!;

        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string? Email { get; set; }

        [StringLength(15, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 15 dígitos.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El teléfono solo puede contener números.")]
        public string? Telefono { get; set; }

        [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no es válida.")]
        public DateOnly? FechaNacimiento { get; set; }

        [Required]
        public bool Estado { get; set; } = true;
        public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();
    }
}

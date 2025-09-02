using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MediReserva.Models;

public partial class Medico
{
    public int Id { get; set; }


    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre no puede estar vacío y máximo 100 caracteres.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El Email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido.")] 
    public string? Email { get; set; }


    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(15, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 15 dígitos.")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "El teléfono solo puede contener números.")]
    public string? Telefono { get; set; }


    public int EspecialidadId { get; set; }

    public int? ConsultorioId { get; set; }

    [Required]
    public bool Estado { get; set; } = true;

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual Especialidad? Especialidad { get; set; }

    public virtual Consultorio? Consultorio { get; set; }

}

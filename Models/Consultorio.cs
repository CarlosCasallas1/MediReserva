using System;
using System.Collections.Generic;

namespace MediReserva.Models;

public partial class Consultorio
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;
    public bool Estado { get; set; }

    public virtual ICollection<Medico> Medicos { get; set; } = new List<Medico>();


}

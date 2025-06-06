﻿using System;
using System.Collections.Generic;

namespace MediReserva.Models;

public partial class Especialidad
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Medico> Medicos { get; set; } = new List<Medico>();
}

using System;
using System.Collections.Generic;

namespace APIBlazor.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? Rol { get; set; }

    public DateTime FechaRegistro { get; set; }

}

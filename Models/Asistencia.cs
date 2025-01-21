using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APIBlazor.Models;

public partial class Asistencia
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public DateTime Fecha { get; set; }

    public bool? Presente { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CompartirDatos
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Es necesario llenar el campo {0}.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Es necesario llenar el campo {0}.")]
        public string Correo { get; set; } = null!;

        [Required(ErrorMessage = "Es necesario llenar el campo {0}, debe tener una longitud mínima de 8 caracteres.")]
        [Range(8, int.MaxValue)]
        public string Contrasena { get; set; } = null!;

        public string? Rol { get; set; }

        public DateTime FechaRegistro { get; set; }

        public Asistencia? asistencia { get; set; }
    }
}

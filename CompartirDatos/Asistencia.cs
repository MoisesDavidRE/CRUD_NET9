using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompartirDatos
{
    public class Asistencia
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public DateTime Fecha { get; set; }

        public bool? Presente { get; set; }
    }
}

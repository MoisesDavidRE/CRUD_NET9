using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompartirDatos
{
    public class RespuestaAPI <T>
    {
        public bool ok { get; set; }
        public T? respuesta { get; set; }
        public string? mensaje { get; set; }
    }
}

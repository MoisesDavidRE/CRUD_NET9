using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIBlazor.Models;
using CompartirDatos;
using Microsoft.EntityFrameworkCore;

namespace APIBlazor.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly AsistenciasDbContext _context;
        public AsistenciaController(AsistenciasDbContext context)
        {
           _context = context;
        }

        [HttpGet]
        [Route("ListaAsistencias")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new RespuestaAPI<List<APIBlazor.Models.Asistencia>>();
            var listaAsistencia = new List<APIBlazor.Models.Asistencia>();

            try
            {
                var asistencias = await _context.Asistencias.ToListAsync();

                foreach (var item in asistencias)
                {
                    listaAsistencia.Add(new APIBlazor.Models.Asistencia
                    {
                        Id = item.Id,
                        UsuarioId = item.UsuarioId,
                        Fecha = item.Fecha,
                        Presente = item.Presente
                    });
                }

                responseApi.ok = true;
                responseApi.respuesta = listaAsistencia;
            }
            catch (Exception ex)
            {
                responseApi.ok = false;
                responseApi.mensaje = ex.Message;
            }
            return Ok(responseApi);
        }


        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new RespuestaAPI<APIBlazor.Models.Asistencia>();
            var asistencia = new APIBlazor.Models.Asistencia();

            try
            {
                var dbAsistencia = await _context.Asistencias.FirstOrDefaultAsync(x => x.Id == id);

                if (dbAsistencia != null)
                {
                    asistencia.Id = dbAsistencia.Id;
                    asistencia.Fecha = dbAsistencia.Fecha;
                    asistencia.Presente = dbAsistencia.Presente;


                    responseApi.ok = true;
                    responseApi.respuesta = asistencia;
                }
                else
                {
                    responseApi.ok = false;
                    responseApi.mensaje = "No encontrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.ok = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpPost]
        [Route("GuardarAsistencia")]
        public async Task<IActionResult> Guardar(APIBlazor.Models.Asistencia asistencia)
        {
            var responseApi = new RespuestaAPI<int>();

            try
            {
                
                asistencia.Fecha = DateTime.Now;

                _context.Asistencias.Add(asistencia);
                await _context.SaveChangesAsync();

                if (asistencia.Id != 0)
                {
                    responseApi.ok = true;
                    responseApi.respuesta = asistencia.Id;
                }
                else
                {
                    responseApi.ok = false;
                    responseApi.mensaje = "No guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.ok = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut]
        [Route("EditarAsistencia/{id}")]
        public async Task<IActionResult> Editar(APIBlazor.Models.Asistencia asistencia, int id)
        {
            var responseApi = new RespuestaAPI<int>();

            try
            {
                var dbAsistencia = await _context.Asistencias.FirstOrDefaultAsync(a => a.Id == id);

                if (dbAsistencia != null)
                {
                    dbAsistencia.UsuarioId = asistencia.UsuarioId; 
                    dbAsistencia.Presente = asistencia.Presente; 


                    _context.Asistencias.Update(dbAsistencia);
                    await _context.SaveChangesAsync();

                    responseApi.ok = true;
                    responseApi.respuesta = dbAsistencia.Id;
                }
                else
                {
                    responseApi.ok = false;
                    responseApi.mensaje = "Asistencia no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.ok = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }
    }


}


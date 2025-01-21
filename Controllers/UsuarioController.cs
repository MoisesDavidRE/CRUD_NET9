using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIBlazor.Models;
using CompartirDatos;
using Microsoft.EntityFrameworkCore;

namespace APIBlazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AsistenciasDbContext _context;
        public UsuarioController(AsistenciasDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListaUsuarios")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new RespuestaAPI<List<APIBlazor.Models.Usuario>>();
            var listaUsuario = new List<APIBlazor.Models.Usuario>();

            try
            {
                foreach (var item in await _context.Usuarios.ToListAsync())
                {
                    listaUsuario.Add(new APIBlazor.Models.Usuario
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Correo = item.Correo,
                        Contrasena = item.Contrasena,
                        Rol = item.Rol,
                        FechaRegistro = item.FechaRegistro
                    });
                }

                responseApi.ok = true;
                responseApi.respuesta = listaUsuario;

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
            var responseApi = new RespuestaAPI<APIBlazor.Models.Usuario>();
            var usuario = new APIBlazor.Models.Usuario();

            try
            {
                var dbUsuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                if (dbUsuario != null)
                {
                    usuario.Id= dbUsuario.Id;
                    usuario.Nombre = dbUsuario.Nombre;
                    usuario.Rol = dbUsuario.Rol;
                    usuario.FechaRegistro = dbUsuario.FechaRegistro;
                    usuario.Correo = dbUsuario.Correo;


                    responseApi.ok = true;
                    responseApi.respuesta = usuario;
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
        [Route("GuardarUsuario")]
        public async Task<IActionResult> Guardar(APIBlazor.Models.Usuario usr)
        {
            var responseApi = new RespuestaAPI<int>();

            try
            {
                var dbUsuario = new APIBlazor.Models.Usuario
                {
                    Id = usr.Id,
                    Nombre = usr.Nombre,
                    Correo = usr.Correo,
                    Contrasena = "123456789",
                    Rol = usr.Rol,
                    FechaRegistro = DateTime.Now
                };

                _context.Usuarios.Add(dbUsuario);
                await _context.SaveChangesAsync();

                if (dbUsuario.Id != 0)
                {
                    responseApi.ok = true;
                    responseApi.respuesta = dbUsuario.Id;
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
        [Route("EditarUsuario/{id}")]
        public async Task<IActionResult> Editar(APIBlazor.Models.Usuario usr, int id)
        {
            var responseApi = new RespuestaAPI<int>();

            try
            {

                var dbUsuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (dbUsuario != null)
                {

                    dbUsuario.Nombre = usr.Nombre;
                    dbUsuario.Correo = usr.Correo;
                    dbUsuario.Contrasena = usr.Contrasena;
                    dbUsuario.Rol = usr.Rol;
                    dbUsuario.FechaRegistro = usr.FechaRegistro;


                    _context.Usuarios.Update(dbUsuario);
                    await _context.SaveChangesAsync();


                    responseApi.ok = true;
                    responseApi.respuesta = dbUsuario.Id;


                }
                else
                {
                    responseApi.ok = false;
                    responseApi.mensaje = "Usuario no econtrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.ok = false;
                responseApi.mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpDelete]
        [Route("EliminarUsuario/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new RespuestaAPI<int>();

            try
            {

                var dbUsuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

                if (dbUsuario != null)
                {
                    _context.Usuarios.Remove(dbUsuario);
                    await _context.SaveChangesAsync();


                    responseApi.ok = true;
                }
                else
                {
                    responseApi.ok = false;
                    responseApi.mensaje = "Usuario no econtrado";
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

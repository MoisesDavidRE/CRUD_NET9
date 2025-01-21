using CompartirDatos;
using System.Net.Http.Json;

namespace Blazor_CRUD.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http) {
            _http = http;
        }

        public async Task<List<Usuario>> Lista()
        {
            var result = await _http.GetFromJsonAsync<RespuestaAPI<List<Usuario>>>("api/Usuario/ListaUsuarios");

            if (result!.ok)
                return result.respuesta!;
            else
                throw new Exception(result.mensaje);
        }
        public async Task<Usuario> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<RespuestaAPI<Usuario>>($"api/Usuario/Buscar/{id}");

            if (result != null && result.ok)
            {
                return result.respuesta;
            }
            else
            {
                Console.WriteLine($"Error en Buscar: {result?.mensaje ?? "Error desconocido"}");
                throw new Exception(result?.mensaje ?? "Error desconocido");
            }
        }
        public async Task<int> Guardar(Usuario usr)
        {
            var result = await _http.PostAsJsonAsync("api/Usuario/GuardarUsuario",usr);
            var response = await result.Content.ReadFromJsonAsync<RespuestaAPI<int>>();
            if (response!.ok)
                return response.respuesta!;
            else
                throw new Exception(response.mensaje);
        }

        public async Task<int> Editar(Usuario usr)
        {
            var result = await _http.PutAsJsonAsync($"api/Usuario/EditarUsuario/{usr.Id}", usr);
            var response = await result.Content.ReadFromJsonAsync<RespuestaAPI<int>>();
            if (response!.ok)
                return response.respuesta!;
            else
                throw new Exception(response.mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Usuario/EliminarUsuario/{id}");
            var response = await result.Content.ReadFromJsonAsync<RespuestaAPI<int>>();
            if (response!.ok)
                return response.ok!;
            else
                throw new Exception(response.mensaje);
        }

    }
}

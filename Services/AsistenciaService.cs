using CompartirDatos;
using System.Net.Http.Json;

namespace Blazor_CRUD.Services
{
    public class AsistenciaService : IAsistenciasService
    {
        private readonly HttpClient _http;

        public AsistenciaService(HttpClient http) {
            _http = http;
        }

        public async Task<List<Asistencia>> Lista()
        {
            var result = await _http.GetFromJsonAsync<RespuestaAPI<List<Asistencia>>>("api/Asistencia/ListaAsistencias");

            if (result!.ok)
                return result.respuesta!;
            else
                throw new Exception(result.mensaje);
        }
        public async Task<Asistencia> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<RespuestaAPI<Asistencia>>($"api/Asistencia/Buscar/{id}");

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

        public async Task<int> Guardar(Asistencia asistencia)
        {
            var result = await _http.PostAsJsonAsync("api/Asistencia/GuardarAsistencia", asistencia);
            var response = await result.Content.ReadFromJsonAsync<RespuestaAPI<int>>();
            if (response!.ok)
                return response.respuesta!;
            else
                throw new Exception(response.mensaje);
        }

        public async Task<int> Editar(Asistencia asistencia)
        {
            var result = await _http.PutAsJsonAsync($"api/Asistencia/EditarAsistencia/{asistencia.Id}", asistencia);
            var response = await result.Content.ReadFromJsonAsync<RespuestaAPI<int>>();
            if (response!.ok)
                return response.respuesta!;
            else
                throw new Exception(response.mensaje);
        }

    }
}

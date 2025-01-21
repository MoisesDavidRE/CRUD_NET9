using CompartirDatos;
namespace Blazor_CRUD.Services
{
    public interface IAsistenciasService
    {
        Task<List<Asistencia>> Lista();
        Task<Asistencia> Buscar(int id);
        Task<int> Guardar(Asistencia asistencia);
        Task<int> Editar(Asistencia asistencia);
    }
}

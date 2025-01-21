using CompartirDatos;
namespace Blazor_CRUD.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> Lista();
        Task<Usuario> Buscar (int id);
        Task<int> Guardar(Usuario usr);
        Task<int> Editar(Usuario usr);
        Task<bool> Eliminar(int id);
    }
}

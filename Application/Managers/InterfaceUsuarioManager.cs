using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface InterfaceUsuarioManager
    {
        public Task<IEnumerable<Usuario>> GetAllUsuarios();
        public Task<Usuario> GetUsuario(int idUsuario);
        public Task<bool> InsertUsuario(Usuario usuario);
        public Task<bool> UpdateUsuario(Usuario usuario);
        public Task<bool> DeleteUsuario(Usuario usuario);
    }
}

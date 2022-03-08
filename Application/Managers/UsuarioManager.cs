using Domain.Models;
using Infrastructure.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class UsuarioManager : InterfaceUsuarioManager
    {
        private readonly InterfaceUsuarioRepository _usuarioRepo;

        public UsuarioManager(InterfaceUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public async Task<bool> DeleteUsuario(Usuario usuario)
        {
            return await _usuarioRepo.DeleteUsuario(usuario);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            return await _usuarioRepo.GetAllUsuarios();
        }

        public async Task<Usuario> GetUsuario(int idUsuario)
        {
            return await _usuarioRepo.GetUsuario(idUsuario);
        }

        public async Task<bool> InsertUsuario(Usuario usuario)
        {
            return await _usuarioRepo.InsertUsuario(usuario);
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            return await _usuarioRepo.UpdateUsuario(usuario);
        }
    }
}

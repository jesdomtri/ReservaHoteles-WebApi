using Dapper;
using Infrastructure.DbContext;
using Npgsql;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : InterfaceUsuarioRepository
    {
        private PostgreConfiguration _connectionString;

        public UsuarioRepository(PostgreConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        private NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteUsuario(Usuario usuario)
        {
            var db = dbConnection();
            var sql = "DELETE FROM usuario_jesus  WHERE id_usuario = @id_usuario";
            var result = await db.ExecuteAsync(sql, new
            {
                id_usuario = usuario.UsuarioID
            });
            return result > 0;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            var db = dbConnection();
            var sql = "SELECT * FROM usuario_jesus ORDER BY id_usuario";
            return await db.QueryAsync<Usuario>(sql, new { });
        }

        public async Task<Usuario> GetUsuario(int idUsuario)
        {
            var db = dbConnection();
            var sql = "SELECT * FROM usuario_jesus WHERE id_usuario = @Id";
            return await db.QueryFirstOrDefaultAsync<Usuario>(sql, new
            {
                Id = idUsuario
            });
        }

        public async Task<bool> InsertUsuario(Usuario usuario)
        {
            var db = dbConnection();
            var sql = $@"INSERT INTO usuario_jesus(nombre, apellidos, mail, direccion)
                 VALUES (@nombre, @apellidos, @mail, @direccion)";
            var result = await db.ExecuteAsync(sql, new
            {
                usuario.Nombre,
                usuario.Apellidos,
                usuario.Mail,
                usuario.Direccion
            });
            return result > 0;
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            var db = dbConnection();
            var sql = $@"UPDATE usuario_jesus SET id_usuario = @id_usuario, nombre = @nombre, 
                        apellidos = @apellidos, mail = @mail, direccion = @direccion WHERE id_usuario = @id_usuario";
            var result = await db.ExecuteAsync(sql, new
            {
                usuario.UsuarioID,
                usuario.Nombre,
                usuario.Apellidos,
                usuario.Mail,
                usuario.Direccion
            });
            return result > 0;
        }
    }
}

using Dapper;
using Npgsql;
using Infrastructure.DbContext;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReservaRepository : InterfaceReservaRepository
    {
        private PostgreConfiguration _connectionString;

        public ReservaRepository(PostgreConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        private NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteReserva(Reserva Reserva)
        {
            var db = dbConnection();
            var sql = "DELETE FROM reserva_jesus WHERE id =@id";
            var result = await db.ExecuteAsync(sql, new
            {
                id = Reserva.ReservaID
            });
            return result > 0;
        }

        public async Task<IEnumerable<Reserva>> GetAllReservas()
        {
            var db = dbConnection();
            var sql = "SELECT * FROM reserva_jesus ORDER BY id";
            return await db.QueryAsync<Reserva>(sql, new { });
        }

        public async Task<Reserva> GetReserva(int idReserva)
        {
            var db = dbConnection();
            var sql = "SELECT * FROM reserva_jesus WHERE id=@Id";
            return await db.QueryFirstOrDefaultAsync<Reserva>(sql, new
            {
                Id = idReserva
            });
        }

        public async Task<bool> InsertReserva(Reserva Reserva)
        {
            var db = dbConnection();
            var sql = $@"INSERT INTO reserva_jesus(checkin, checkout, fecha_reserva, estado, id_hotel, id_usuario) 
                        VALUES (@checkin, @checkout, @fecha_reserva, @estado, @id_hotel, @id_usuario)";
            var result = await db.ExecuteAsync(sql, new
            {
                Reserva.CheckIn,
                Reserva.CheckOut,
                Reserva.FechaReserva,
                Reserva.Estado,
                Reserva.HotelID,
                Reserva.UsuarioID
            });
            return result > 0;
        }

        public async Task<bool> UpdateReserva(Reserva Reserva)
        {
            var db = dbConnection();
            var sql = $@"UPDATE reserva_jesus SET id=@id, checkin=@checkin, checkout=@checkout, fecha_reserva=@fecha_reserva,
                        estado=@estado, id_hotel=@id_hotel, id_usuario=@id_usuario WHERE id=@id";
            var result = await db.ExecuteAsync(sql, new
            {
                Reserva.ReservaID,
                Reserva.CheckIn,
                Reserva.CheckOut,
                Reserva.FechaReserva,
                Reserva.Estado,
                Reserva.HotelID,
                Reserva.UsuarioID
            });
            return result > 0;
        }

        public async Task<IEnumerable<Reserva>> GetAllReservasActivasByUsuario(int idUsuario)
        {
            var db = dbConnection();
            var sql = "SELECT * FROM reserva_jesus WHERE estado=0 AND id_usuario=@id_usuario ORDER BY id";
            return await db.QueryAsync<Reserva>(sql, new
            {
                id_usuario = idUsuario
            });
        }

        public async Task<IEnumerable<Reserva>> GetAllReservasByHotelAndEmail(int idHotel, string email)
        {
            var db = dbConnection();
            var sql = $@"SELECT r.*, u.mail FROM reserva_jesus r INNER JOIN usuario_jesus u 
                        ON r.id_usuario = u.id_usuario WHERE r.id_hotel=@id_hotel AND u.mail=@mail 
                        ORDER BY r.id";
            return await db.QueryAsync<Reserva>(sql, new
            {
                id_hotel = idHotel,
                mail = email
            });
        }
    }
}

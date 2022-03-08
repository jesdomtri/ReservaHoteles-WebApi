using Dapper;
using Infrastructure.DbContext;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class HotelRepository : InterfaceHotelRepository
    {
        private PostgreConfiguration _connectionString;

        public HotelRepository(PostgreConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        private NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }
        public async Task<bool> DeleteHotel(Hotel hotel)
        {
            var db = dbConnection();
            var sql = "DELETE FROM hotel_jesus WHERE id_hotel =@id_hotel";
            var result = await db.ExecuteAsync(sql, new
            {
                id_hotel = hotel.HotelID
            });
            return result > 0;
        }

        public async Task<IEnumerable<Hotel>> GetAllHoteles()
        {
            var db = dbConnection();
            var sql = "SELECT * FROM hotel_jesus ORDER BY id_hotel";
            return await db.QueryAsync<Hotel>(sql, new { });
        }

        public async Task<Hotel> GetHotel(int idHotel)
        {
            var db = dbConnection();
            var sql = "SELECT * FROM hotel_jesus WHERE id_hotel=@Id";
            return await db.QueryFirstOrDefaultAsync<Hotel>(sql, new
            {
                Id = idHotel
            });
        }

        public async Task<bool> InsertHotel(Hotel hotel)
        {
            var db = dbConnection();
            var sql = $@"INSERT INTO hotel_jesus(nombre, pais, latitud, longitud, descripcion, activo) 
                        VALUES ( @nombre, @pais, @latitud, @longitud, @descripcion, @activo)";
            var result = await db.ExecuteAsync(sql, new
            {
                hotel.Nombre,
                hotel.Pais,
                hotel.Latitud,
                hotel.Longitud,
                hotel.Descripcion,
                hotel.Activo
            });
            return result > 0;
        }

        public async Task<bool> UpdateHotel(Hotel hotel)
        {
            var db = dbConnection();
            var sql = $@"UPDATE hotel_jesus SET id_hotel=@id_hotel, nombre=@nombre, pais=@pais, latitud=@latitud, 
                        longitud=@longitud, descripcion=@descripcion, activo=@activo WHERE id_hotel =@id_hotel";
            var result = await db.ExecuteAsync(sql, new
            {
                hotel.HotelID,
                hotel.Nombre,
                hotel.Pais,
                hotel.Latitud,
                hotel.Longitud,
                hotel.Descripcion,
                hotel.Activo
            });
            return result > 0;
        }
    }
}

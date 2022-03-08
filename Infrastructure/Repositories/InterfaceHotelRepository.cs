using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface InterfaceHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllHoteles();
        Task<Hotel> GetHotel(int idHotel);
        Task<bool> InsertHotel(Hotel hotel);
        Task<bool> UpdateHotel(Hotel hotel);
        Task<bool> DeleteHotel(Hotel hotel);
    }
}

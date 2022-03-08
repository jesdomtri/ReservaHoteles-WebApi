using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface InterfaceHotelManager
    {
        public Task<IEnumerable<Hotel>> GetAllHoteles();
        public Task<Hotel> GetHotel(int idHotel);
        public Task<bool> InsertHotel(Hotel hotel);
        public Task<bool> UpdateHotel(Hotel hotel);
        public Task<bool> DeleteHotel(Hotel hotel);
    }
}

using Domain.Models;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class HotelManager : InterfaceHotelManager
    {
        private readonly InterfaceHotelRepository _hotelRepository;

        public HotelManager(InterfaceHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<bool> DeleteHotel(Hotel hotel)
        {
            return await _hotelRepository.DeleteHotel(hotel);
        }

        public async Task<IEnumerable<Hotel>> GetAllHoteles()
        {
            return await _hotelRepository.GetAllHoteles();
        }

        public async Task<Hotel> GetHotel(int idHotel)
        {
            return await _hotelRepository.GetHotel(idHotel);
        }

        public async Task<bool> InsertHotel(Hotel hotel)
        {
            return await _hotelRepository.InsertHotel(hotel);
        }

        public async Task<bool> UpdateHotel(Hotel hotel)
        {
            return await _hotelRepository.UpdateHotel(hotel);
        }
    }
}

using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public interface InterfaceReservaManager
    {
        public Task<IEnumerable<Reserva>> GetAllReservas();
        public Task<Reserva> GetReserva(int idReserva);
        public Task<bool> InsertReserva(Reserva Reserva);
        public Task<bool> UpdateReserva(Reserva Reserva);
        public Task<bool> DeleteReserva(Reserva Reserva);
        public Task<Reserva> CancelarReserva(int idReserva);
        public Task<IEnumerable<Reserva>> GetAllReservasActivasByUsuario(int idUsuario);
        public Task<IEnumerable<Reserva>> GetAllReservasByHotelAndEmail(int idHotel, string email);
    }
}

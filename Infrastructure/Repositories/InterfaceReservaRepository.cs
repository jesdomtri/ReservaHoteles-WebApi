using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface InterfaceReservaRepository
    {
        Task<IEnumerable<Reserva>> GetAllReservas();
        Task<Reserva> GetReserva(int idReserva);
        Task<bool> InsertReserva(Reserva Reserva);
        Task<bool> UpdateReserva(Reserva Reserva);
        Task<bool> DeleteReserva(Reserva Reserva);
        Task<IEnumerable<Reserva>> GetAllReservasActivasByUsuario(int idUsuario);
        Task<IEnumerable<Reserva>> GetAllReservasByHotelAndEmail(int idHotel, string email);
    }
}

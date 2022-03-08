using Domain.Models;
using Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class ReservaManager : InterfaceReservaManager
    {
        private readonly InterfaceReservaRepository _reservaRepo;

        public ReservaManager(InterfaceReservaRepository reservaRepo)
        {
            _reservaRepo = reservaRepo;
        }

        public async Task<Reserva> CancelarReserva(int idReserva)
        {
            var reserva = await _reservaRepo.GetReserva(idReserva);
            reserva.Cancel();
            await _reservaRepo.UpdateReserva(reserva);
            return reserva;
        }

        public async Task<bool> DeleteReserva(Reserva Reserva)
        {
            return await _reservaRepo.DeleteReserva(Reserva);
        }

        public async Task<IEnumerable<Reserva>> GetAllReservas()
        {
            return await _reservaRepo.GetAllReservas();
        }

        public async Task<IEnumerable<Reserva>> GetAllReservasActivasByUsuario(int idUsuario)
        {
            return await _reservaRepo.GetAllReservasActivasByUsuario(idUsuario);
        }

        public async Task<IEnumerable<Reserva>> GetAllReservasByHotelAndEmail(int idHotel, string email)
        {
            return await _reservaRepo.GetAllReservasByHotelAndEmail(idHotel, email);
        }

        public async Task<Reserva> GetReserva(int idReserva)
        {
            return await _reservaRepo.GetReserva(idReserva);
        }

        public async Task<bool> InsertReserva(Reserva Reserva)
        {
            return await _reservaRepo.InsertReserva(Reserva);
        }

        public async Task<bool> UpdateReserva(Reserva Reserva)
        {
            return await _reservaRepo.UpdateReserva(Reserva);
        }
    }
}

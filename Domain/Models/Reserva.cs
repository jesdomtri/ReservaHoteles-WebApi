using System;

namespace Domain.Models
{
    public enum EstadoReserva { Reservado, Cancelado }
    public class Reserva
    {
        public int ReservaID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime FechaReserva { get; set; }
        public EstadoReserva Estado { get; set; }
        public int HotelID { get; set; }
        public int UsuarioID { get; set; }
        public void Cancel()
        {
            Estado = EstadoReserva.Cancelado;
        }
    }
}

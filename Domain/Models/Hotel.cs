namespace Domain.Models
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}

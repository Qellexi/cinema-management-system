namespace CinemaApp.Models
{
    public class Seat
    {
        public int Id { get; set; }

        public int Row { get; set; }
        public int Number { get; set; }

        public bool IsBooked { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
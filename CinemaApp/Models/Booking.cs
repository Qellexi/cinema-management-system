using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<Seat> Seats { get; set; } = new();
    }
}
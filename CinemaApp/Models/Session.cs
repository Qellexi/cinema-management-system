using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Час початку")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Ціна квитка")]
        public decimal TicketPrice { get; set; }
        public string Format { get; set; }
        // Зв'язок з фільмом
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
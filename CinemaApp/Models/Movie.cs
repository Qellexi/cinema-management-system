using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва обов'язкова")]
        [Display(Name = "Назва фільму")]
        public string Title { get; set; }

        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        [Display(Name = "Режисер")]
        public string Director { get; set; }

        [Display(Name = "Постер (URL)")]
        public string ImageUrl { get; set; } // Посилання на картинку
        
        [Display(Name = "Трейлер (URL)")]
        public string TrailerUrl { get; set; }
        
        // Зв'язок: один фільм може мати багато сеансів
        public List<Session> Sessions { get; set; } = new List<Session>();
        
        [Display(Name = "Дата релізу")]
        public DateTime ReleaseDate { get; set; }

    }
}
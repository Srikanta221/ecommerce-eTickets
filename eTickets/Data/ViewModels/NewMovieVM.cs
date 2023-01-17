using eTickets.Data.Base;
using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieVM 
    {

        public int Id { get; set; }

        [Display(Name ="Name")]
        [Required(ErrorMessage ="Required")]
        public string? Name { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        public string? Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Required")]
        public double Price { get; set; }

        [Display(Name = "Movie Poster URL")]
        [Required(ErrorMessage = "Required")]
        public string? ImageURL { get; set; }

        [Display(Name = "Movie Start Date")]
        [Required(ErrorMessage = "Required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Movie End Date")]
        [Required(ErrorMessage = "Required")]
        public DateTime EndDate { get; set; }


        [Display(Name = "Select a Category")]
        [Required(ErrorMessage = "Required")]
        public MovieCategory MovieCategory { get; set; }


        [Display(Name = "Select Actor(s)")]
        [Required(ErrorMessage = "Required")]
        public List<int>? ActorIds { get; set; }

        [Display(Name = "Select a Cinema")]
        [Required(ErrorMessage = "Required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a Producer")]
        [Required(ErrorMessage = "Required")]
        public int ProducerId { get; set; }
 
    }
}

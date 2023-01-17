using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name= "Cinema Logo")]
        [Required(ErrorMessage ="Required")]
        public string? Logo { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]

        public string? Name { get; set; }
        
        [Display(Name= "Description")]
        [Required(ErrorMessage = "Required")]

        public string? Description { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}

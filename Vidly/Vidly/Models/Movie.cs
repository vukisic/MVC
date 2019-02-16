using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name="Release Date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ReleaseDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateAdded { get; set; }

        [Required]
        [Display(Name="Number in Stock")]
        [Range(1,20,ErrorMessage ="Number must be between 1 and 20!")]
        public int NumberInStock { get; set; }

        //[Required]
        public Genre Genre { get; set; }

        [Display(Name="Genre")]
        [Required]
        public int GenreId { get; set; }

        public int NumberAvailable { get; set; }

    }
}
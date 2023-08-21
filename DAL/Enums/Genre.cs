using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.Enums
{
    public enum Genre
    {
        [Display(Name = "Жанр 1")]
        Genre1,
        [Display(Name = "Жанр 2")]
        Genre2,
        [Display(Name = "Жанр 3")]
        Genre3,
        [Display(Name = "Жанр 4")]
        Genre4
    }
}

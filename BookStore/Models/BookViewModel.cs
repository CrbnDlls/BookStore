using System.ComponentModel.DataAnnotations;
using BookStore.Enums;
using BookStore.Helpers;
using DAL.Enums;

namespace BookStore.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Назва - обов'язкове поле для заповнення")]
        [Display(Name = "Назва")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Кількість сторінок - обов'язкове поле для заповнення")]
        [Display(Name = "Кількість сторінок")]
        public uint PageQuantity { get; set; }
        [Required(ErrorMessage = "Жанр - обов'язкове поле для заповнення")]
        [Display(Name = "Жанр")]
        public Genre Genre { get; set; }
        [Display(Name = "Автор")]
        [Required(ErrorMessage = "Автор - обов'язкове поле для заповнення")]
        public int AuthorId { get; set; }
        [Display(Name = "Автор")]
        public virtual AuthorViewModel? Author { get; set; }
        public string JsonGenre => EnumHelper.GetDisplayValue(Genre);
        public RecordStatus RecordStatus { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Прізвище - обов'язкове поле для заповнення")]
        [Display(Name = "Прізвище")]
        public string FamilyName { get; set; }
        [Required(ErrorMessage = "Ім'я - обов'язкове поле для заповнення")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        [Display(Name = "По батькові")]
        public string? FathersName { get; set; }
        [Required(ErrorMessage = "Дата народження - обов'язкове поле для заповнення")]
        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Список книг")]
        public virtual ICollection<BookViewModel>? Books { get; set; }
        [Display(Name = "Книжок в базі")]
        public int BooksQuantity => Books?.Count ?? 0;
        [Display(Name = "ПІБ")]
        public string FullName => $"{FamilyName} {Name.Substring(0,1)}.{(string.IsNullOrEmpty(FathersName) ? "" : FathersName.Substring(0,1) + ".")}";
    }
}

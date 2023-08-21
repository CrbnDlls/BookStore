using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class Author
    {
        public int Id { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        [Display(Name = "По батькові")]
        public string? FathersName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Book>? Books { get; set; } 
    }
}

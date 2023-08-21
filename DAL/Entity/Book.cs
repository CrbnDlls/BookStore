using DAL.Enums;

namespace DAL.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public uint PageQuantity { get; set; }
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public virtual Author? Author { get; set; }
    }
}

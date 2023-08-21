using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Task<IEnumerable<Book>> SelectWithAuthors();

        public Task<Book> GetBookWithAuthor(int id);
    }
}

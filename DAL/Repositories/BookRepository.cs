using DAL.Entity;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class BookRepository : IBookRepository
    {
        public async Task<Book> Create(Book entity)
        {
            using (var db = new ApplicationContext())
            {
                EntityEntry<Book> entityEntry = await db.Books.AddAsync(entity);
                await db.SaveChangesAsync();
                return entityEntry.Entity;
            }

        }

        public async Task<bool> Delete(int id)
        {
            using (var db = new ApplicationContext())
            {
                Book book = await GetById(id);
                if (book != null)
                {
                    await Task.Run(() => db.Books.Remove(book));

                    await db.SaveChangesAsync();

                    return true;
                }
                return false;
            }
        }

        public async Task<Book> GetBookWithAuthor(int id)
        {
            using (var db = new ApplicationContext())
            {
                return await db.Books.Include(x => x.Author).FirstOrDefaultAsync();
            }
        }

        public async Task<Book> GetById(int id)
        {
            using (var db = new ApplicationContext())
            {
                return await db.Books.FindAsync(id);
            }
        }

        public async Task<IEnumerable<Book>> Select()
        {
            using (var db = new ApplicationContext())
            {
                return await db.Books.ToListAsync();
            }
        }

        public async Task<IEnumerable<Book>> SelectWithAuthors()
        {
            using (var db = new ApplicationContext())
            {
                return await db.Books.Include(x=> x.Author).ToListAsync();
            }
        }

        public async Task<Book> Update(Book entity)
        {
            using (var db = new ApplicationContext())
            {
                EntityEntry<Book> entityEntry = await Task.Run(() => db.Books.Update(entity));

                await db.SaveChangesAsync();

                return entityEntry.Entity;
            }
        }
    }
}

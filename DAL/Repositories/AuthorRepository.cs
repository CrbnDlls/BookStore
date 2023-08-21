using DAL.Entity;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public async Task<Author> Create(Author entity)
        {
            using (var db = new ApplicationContext())
            {
                EntityEntry<Author> entityEntry = await db.Authors.AddAsync(entity);
                await db.SaveChangesAsync();
                return entityEntry.Entity;
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            using (var db = new ApplicationContext())
            {
                Author author = await GetById(id);
                if (author != null)
                {
                    await Task.Run(() => db.Authors.Remove(author));

                    await db.SaveChangesAsync();

                    return true;
                }
                return false;
            }
        }

        public async Task<Author> GetAuthorWithBooksById(int id)
        {
            using (var db = new ApplicationContext())
            {
                return await db.Authors.Include(x => x.Books).FirstOrDefaultAsync(x=> x.Id == id);
            }
        }

        public async Task<Author> GetById(int id)
        {
            using (var db = new ApplicationContext())
            {
                return await db.Authors.FindAsync(id);
            }
        }

        public async Task<IEnumerable<Author>> Select()
        {
            using (var db = new ApplicationContext())
            {
                return await db.Authors.ToListAsync();
            }
        }

        public async Task<IEnumerable<Author>> SelectWithBooks()
        {
            using (var db = new ApplicationContext())
            {
                return await db.Authors.Include(x=>x.Books).ToListAsync();
            }
        }

        public async Task<Author> Update(Author entity)
        {
            using (var db = new ApplicationContext())
            {
                EntityEntry<Author> entityEntry = await Task.Run(() => db.Authors.Update(entity));

                await db.SaveChangesAsync();

                return entityEntry.Entity;
            }
        }
    }
}

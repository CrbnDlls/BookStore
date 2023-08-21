using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DAL
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-GLAT3MS\\SQLEXPRESS;Initial Catalog=BookStoreDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Books");

                entity.Property(e => e.Id).HasColumnName("ID").IsRequired();
                entity.Property(e => e.PageQuantity).HasColumnType("numeric(18, 0)").IsRequired();
                entity.Property(e => e.AuthorId).HasColumnName("AuthorID").IsRequired();
                entity.Property(e => e.Genre).HasColumnType("numeric(18, 0)").IsRequired();

                entity.HasOne(d => d.Author).WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Book_Author")
                    .IsRequired();

            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Authors");

                entity.Property(e => e.Id).HasColumnName("ID").IsRequired();
                entity.Property(e => e.FamilyName).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property(e => e.FathersName).HasMaxLength(50);
                entity.Property(e => e.BirthDate).HasColumnType("datetime2").IsRequired();

            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

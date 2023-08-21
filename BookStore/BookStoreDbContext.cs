using System;
using System.Collections.Generic;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore;

public partial class BookStoreDataBaseContext : DbContext
{
    public BookStoreDataBaseContext()
    {
        Database.EnsureCreated();
    }

    public BookStoreDataBaseContext(DbContextOptions<BookStoreDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthorViewModel> Authors { get; set; }

    public virtual DbSet<BookViewModel> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-GLAT3MS\\SQLEXPRESS;Initial Catalog=BookStoreDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookViewModel>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Books");

            entity.Property(e => e.Id).HasColumnName("ID").IsRequired();
            entity.Property(e => e.PageQuantity).HasColumnType("numeric(18, 0)").IsRequired();
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID").IsRequired();
            entity.Property(e => e.Genre).HasColumnType("numeric(18, 0)").IsRequired();
            entity.Ignore(e => e.JsonGenre);
            entity.Ignore(e => e.RecordStatus);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Book_Author")
                .IsRequired();

        });

        modelBuilder.Entity<AuthorViewModel>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Authors");

            entity.Property(e => e.Id).HasColumnName("ID").IsRequired();
            entity.Property(e => e.FamilyName).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.FathersName).HasMaxLength(50);
            entity.Property(e => e.BirthDate).HasColumnType("datetime2").IsRequired();
            entity.Ignore(e => e.BooksQuantity);
            entity.Ignore(e => e.FullName);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


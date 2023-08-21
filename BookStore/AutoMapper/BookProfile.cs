using AutoMapper;
using BookStore.Models;
using DAL.Entity;

namespace BookStore.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();
        }
    }
}

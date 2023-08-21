using AutoMapper;
using BookStore.Models;
using DAL.Entity;

namespace BookStore.AutoMapper
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorViewModel>();
            CreateMap<AuthorViewModel, Author>();
        }
        
    }
}

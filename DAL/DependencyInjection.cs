using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection service)
        {
            service.AddScoped<IBookRepository, BookRepository>();
            service.AddScoped<IAuthorRepository, AuthorRepository>();


            return service;
        }
    }
}
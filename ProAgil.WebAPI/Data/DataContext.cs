using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI.Model;

namespace ProAgil.WebAPI.Data
{
    public class DataContext : DbContext
    {
        //propriedade
        public DbSet<Evento> Eventos { get; set; }

        //constrututor
        public DataContext(DbContextOptions<DataContext> options) 
        : base(options) { }

        
    }
}
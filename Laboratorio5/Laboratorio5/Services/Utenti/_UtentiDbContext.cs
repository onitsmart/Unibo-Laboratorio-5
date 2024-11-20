using Laboratorio5.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio5.Services.Utenti
{
    public class UtentiDbContext : DbContext
    {
        public UtentiDbContext()
        {
        }

        public UtentiDbContext(DbContextOptions<UtentiDbContext> options) : base(options)
        {
            // SOLO PER ESERCITAZIONE. E' UN DATABASE IN MEMORY
            DataGenerator.InitializeUtenti(this);
        }

        public DbSet<Utente> Utenti { get; set; }
    }
}

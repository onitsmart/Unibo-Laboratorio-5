using Laboratorio6.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Laboratorio6.Services.Utenti
{
    public class UtentiDbContext : DbContext
    {
        public UtentiDbContext()
        {
        }

        public UtentiDbContext(DbContextOptions<UtentiDbContext> options) : base(options)
        {
            // ES2 SOLO PER ESERCITAZIONE. E' UN DATABASE IN MEMORY
            DataGenerator.InitializeUtenti(this);
        }

        public DbSet<Utente> Utenti { get; set; }
    }
}

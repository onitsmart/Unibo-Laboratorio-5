namespace Laboratorio6.Services.Utenti
{
    public partial class UtentiService
    {
        UtentiDbContext _dbContext;

        public UtentiService(UtentiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

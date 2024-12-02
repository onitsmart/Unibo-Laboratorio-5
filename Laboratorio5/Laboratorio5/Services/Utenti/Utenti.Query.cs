using Laboratorio5.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio5.Services.Utenti
{
    public class UtenteInfoDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
    }

    public class ValidaCredenzialiPerLoginQuery
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UtenteInfoQuery
    {
        public Guid Id { get; set; }
    }

    public class UtenteDaSfidareDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

    public class UtentiDaSfidareQuery
    {
        public Guid IdUtenteCorrente { get; set; }
        public string Filtro { get; set; }
    }

    public partial class UtentiService
    {
        // Ritorna i dati dell'utente trovato oppure genera un'eccezione se i dati forniti 
        // nella query non corrispondono a nessun utente presente nel db
        public async Task<UtenteInfoDTO> Query(ValidaCredenzialiPerLoginQuery qry)
        {
            var utente = await _dbContext.Utenti
                .Where(x => x.Email == qry.Email)
                .FirstOrDefaultAsync();

            if (utente == null || utente.TryPassword(qry.Password) == false)
                throw new LoginException("Email o password errate");

            return new UtenteInfoDTO
            {
                Id = utente.Id,
                Email = utente.Email,
                FirstName = utente.FirstName,
                LastName = utente.LastName,
                NickName = utente.NickName
            };
        }

        public async Task<UtenteInfoDTO> Query(UtenteInfoQuery qry)
        {
            return await _dbContext.Utenti
                .Where(x => x.Id == qry.Id)
                .Select(x => new UtenteInfoDTO
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    NickName = x.NickName
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UtenteDaSfidareDTO>> Query(UtentiDaSfidareQuery qry)
        {
            var queryable = _dbContext.Utenti
                .Where(x => x.Id != qry.IdUtenteCorrente);

            if (string.IsNullOrWhiteSpace(qry.Filtro) == false)
                queryable = queryable.Where(x => x.Email.Contains(qry.Filtro));

            return await queryable
                .Select(x => new UtenteDaSfidareDTO
                {
                    Id = x.Id,
                    Email = x.Email
                }).ToArrayAsync();
        }
    }
}

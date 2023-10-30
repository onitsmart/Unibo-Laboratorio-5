using System;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Laboratorio6.Services.Utenti
{
    public class Utente
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }

        public bool TryPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            var sha256 = SHA256.Create();
            var testPassword = System.Convert.ToBase64String(sha256.ComputeHash(Encoding.ASCII.GetBytes(password)));

            return this.Password == testPassword;
        }
    }
}

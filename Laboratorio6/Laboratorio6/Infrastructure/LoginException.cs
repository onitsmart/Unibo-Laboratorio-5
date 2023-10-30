using System;

namespace Laboratorio6.Infrastructure
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message) { }
    }
}

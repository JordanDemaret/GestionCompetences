using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Common.Auth
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string? encodedHash);
    }
}

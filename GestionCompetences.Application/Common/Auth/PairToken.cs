using System;
using System.Collections.Generic;
using System.Text;

namespace GestionCompetences.Application.Common.Auth
{
    public sealed record PairToken(string Token, string RefreshToken);
}

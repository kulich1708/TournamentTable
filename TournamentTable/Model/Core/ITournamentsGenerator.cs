using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ITournamentsGenerator<TTournament, TTeam> where TTournament : TournamentTable<TTeam> where TTeam : Team
    {
        TTournament GenerateTournament(string name);
        void GenerateTournaments();
    }
}

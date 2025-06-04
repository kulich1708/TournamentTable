using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface IMatchGenerator<TTeam> where TTeam : Team
    {
        MatchDop<TTeam> GenerateMatch(int tournamentId, TTeam team1, TTeam team2);
    }
}

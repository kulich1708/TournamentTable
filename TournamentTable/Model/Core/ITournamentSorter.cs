using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ITournamentSorter<TTeam> where TTeam : Team
    {
        List<TTeam> Sort(int tournamentId, List<TTeam> teams);
    }
}

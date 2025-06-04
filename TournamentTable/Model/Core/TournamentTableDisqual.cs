using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public abstract partial class TournamentTable<TTeam>
    {
        public void Disqual(TTeam team) => team.Disqual(Id);
    }
}

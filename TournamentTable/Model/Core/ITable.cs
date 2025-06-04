using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ITable<TTeam>
    {
        public void Match(TTeam team1, TTeam team2);
        public void SortDefault();
        public void SortByScore();
    }
}

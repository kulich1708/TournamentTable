using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ICreateTeam<TTeam> where TTeam : Team
    {
        TTeam Create(string name);
    }
    public class CreateFootballTeam : ICreateTeam<FootballTeam>
    {
        public FootballTeam Create(string name) { return new FootballTeam(name); }
    }
    public class CreateBasketballTeam : ICreateTeam<BasketballTeam>
    {
        public BasketballTeam Create(string name) { return new BasketballTeam(name); }
    }
    public class CreateVoleyballTeam : ICreateTeam<VoleyballTeam>
    {
        public VoleyballTeam Create(string name) { return new VoleyballTeam(name); }
    }
}

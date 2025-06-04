using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ICreateTournament<TTournament, TTeam> where TTournament : TournamentTable<TTeam> where TTeam : Team
    {
        TTournament Create(string name, TTeam[] teams, int id = 0);

    }
    public class CreateFootballTournament : ICreateTournament<FootballTournamentTable, FootballTeam>
    {
        public FootballTournamentTable Create(string name, FootballTeam[] teams, int id = 0)
        { return new FootballTournamentTable(name, teams, id); }
    }
    public class CreateBasketballTournament : ICreateTournament<BasketballTournamentTable, BasketballTeam>
    {
        public BasketballTournamentTable Create(string name, BasketballTeam[] teams, int id = 0)
        { return new BasketballTournamentTable(name, teams, id); }
    }
    public class CreateVoleyballTournament : ICreateTournament<VoleyballTournamentTable, VoleyballTeam>
    {
        public VoleyballTournamentTable Create(string name, VoleyballTeam[] teams, int id = 0)
        { return new VoleyballTournamentTable(name, teams, id); }
    }
}

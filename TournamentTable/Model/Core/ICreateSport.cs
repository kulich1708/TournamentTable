using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ICreateSport<TSport, TTournament, TTeam> 
        where TSport : Sport<TTournament, TTeam> 
        where TTournament : TournamentTable<TTeam> 
        where TTeam : Team
    {
        TSport Create(TTournament[] tournaments);

    }
    public class CreateFootball : ICreateSport<Football, FootballTournamentTable, FootballTeam>
    {
        public Football Create(FootballTournamentTable[] tournaments)
        { return new Football(tournaments); }
    }
    public class CreateBasketball : ICreateSport<Basketball, BasketballTournamentTable, BasketballTeam>
    {
        public Basketball Create(BasketballTournamentTable[] tournaments)
        { return new Basketball(tournaments); }
    }
    public class CreateVoleyball : ICreateSport<Voleyball, VoleyballTournamentTable, VoleyballTeam>
    {
        public Voleyball Create(VoleyballTournamentTable[] tournaments)
        { return new Voleyball(tournaments); }
    }
}

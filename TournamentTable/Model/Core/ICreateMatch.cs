using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ICreateMatch<TMatch, TTeam> where TMatch : Match where TTeam : Team
    {
        TMatch Create(TTeam team1, TTeam team2, int score1, int score2, int tournamentId);
    }
    public class CreateFootballMatch : ICreateMatch<FootballMatch, FootballTeam>
    {
        public FootballMatch Create(FootballTeam team1, FootballTeam team2, int score1, int score2, int tournamentId)
        { return new FootballMatch(team1, team2, score1, score2, tournamentId); }
    }
    public class CreateBasketballMatch : ICreateMatch<BasketballMatch, BasketballTeam>
    {
        public BasketballMatch Create(BasketballTeam team1, BasketballTeam team2, int score1, int score2, int tournamentId)
        { return new BasketballMatch(team1, team2, score1, score2, tournamentId); }
    }
    public class CreateVoleyballMatch : ICreateMatch<VoleyballMatch, VoleyballTeam>
    {
        public VoleyballMatch Create(VoleyballTeam team1, VoleyballTeam team2, int score1, int score2, int tournamentId)
        { return new VoleyballMatch(team1, team2, score1, score2, tournamentId); }
    }
}

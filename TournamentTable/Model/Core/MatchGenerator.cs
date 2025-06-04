using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public abstract class MatchGenerator<TTeam> : IMatchGenerator<TTeam> where TTeam : Team
    {
        protected static readonly Random _random = new Random();
        public abstract MatchDop<TTeam> GenerateMatch(int tournamentId, TTeam team1, TTeam team2);
    }
    public class FootballMatchGenerator : MatchGenerator<FootballTeam>
    {
        public override MatchDop<FootballTeam> GenerateMatch(int tournamentId, FootballTeam team1, FootballTeam team2)
        {
            int score1 = _random.Next(0, 5);
            int score2 = _random.Next(0, 5);

            return new FootballMatch(team1, team2, score1, score2, tournamentId);
        }
    }
    public class BasketballMatchGenerator : MatchGenerator<BasketballTeam>
    {
        public override MatchDop<BasketballTeam> GenerateMatch(int tournamentId, BasketballTeam team1, BasketballTeam team2)
        {
            int score1 = _random.Next(80, 141);
            int score2 = _random.Next(80, 141);
            while (score1 == score2)
            {
                score1 += _random.Next(3, 11);
                score2 += _random.Next(3, 11);
            }

            return new BasketballMatch(team1, team2, score1, score2, tournamentId);
        }
    }
    public class VoleyballMatchGenerator : MatchGenerator<VoleyballTeam>
    {
        public override MatchDop<VoleyballTeam> GenerateMatch(int tournamentId, VoleyballTeam team1, VoleyballTeam team2)
        {
            int isWin = _random.Next(0, 2);
            int score1 = 0;
            int score2 = 0;
            if (isWin == 0)
            {
                score1 = 3;
                score2 = _random.Next(0, 3);
            }
            else
            {
                score1 = _random.Next(0, 3);
                score2 = 3;
            }

            return new VoleyballMatch(team1, team2, score1, score2, tournamentId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public interface ITournamentStatsCreator
    {
        ITournamentStats Create();
    }
    public class FootballTournamentStatsCreator : ITournamentStatsCreator
    {
        public ITournamentStats Create() => new FootballTournamentStats();
    }
    public class BasketballTournamentStatsCreator : ITournamentStatsCreator
    {
        public ITournamentStats Create() => new BasketballTournamentStats();
    }
    public class VoleyballTournamentStatsCreator : ITournamentStatsCreator
    {
        public ITournamentStats Create() => new VoleyballTournamentStats();
    }
    public interface ITournamentStats
    {
        List<Match> Matches { get; }
        int Points { get; }
        int Place { get; }
        string EqualityId { get; }
        int EqualityPoints { get; }
        bool Disqualification { get; }
        void AddMatch(Match match);
        ScoringContext GetScoringContext();
        void SetPoints(int points);
        void SetPlace(int place);
        void SetEqualityId(string equalityId);
        void AddEqualityPoints(int equalityPoints);
        void Disqual();
    }
    public abstract class TournamentStats : ITournamentStats
    {
        public int Points { get; private set; }
        public int Place { get; private set; }
        public string EqualityId { get; private set; }
        public int EqualityPoints { get; private set; }
        public bool Disqualification { get; private set; }
        public List<Match> Matches { get; private set; } = new();
        public void AddMatch(Match match) => Matches.Add(match);
        public void SetPoints(int points) => Points = points;
        public void SetPlace(int place) => Place = place;
        public void SetEqualityId(string equalityId) => EqualityId = equalityId;
        public void AddEqualityPoints(int equalityPoints) => EqualityPoints += equalityPoints;
        public void Disqual() => Disqualification = true;
        public abstract ScoringContext GetScoringContext();

    }
    public class FootballTournamentStats : TournamentStats
    {
        public int Wins { get; private set; }
        public int Draws { get; private set; }
        public int Losses { get; private set; }

        public void AddWin() => Wins++;
        public void AddDraw() => Draws++;
        public void AddLoss() => Losses++;

        public int GetWins() => Wins;
        public int GetDraws() => Draws;
        public int GetLosses() => Losses;
        public override ScoringContext GetScoringContext()
        {
            return new ScoringContext
            {
                Wins = Wins,
                Draws = Draws,
                Losses = Losses
            };
        }
    }
    public class BasketballTournamentStats : TournamentStats
    {
        public int Wins { get; private set; }
        public int Draws { get; private set; }
        public int Losses { get; private set; }

        public void AddWin() => Wins++;
        public void AddLoss() => Losses++;

        public int GetWins() => Wins;
        public int GetLosses() => Losses;
        public override ScoringContext GetScoringContext()
        {
            return new ScoringContext
            {
                Wins = Wins,
                Losses = Losses
            };
        }
    }
    public class VoleyballTournamentStats : TournamentStats
    {
        public int Wins3 { get; private set; }
        public int Wins2 { get; private set; }
        public int Losses1 { get; private set; }
        public int Losses { get; private set; }

        public void AddWin3() => Wins3++;
        public void AddWin2() => Wins2++;
        public void AddLoss1() => Losses1++;
        public void AddLoss() => Losses++;



        public int GetWin3() => Wins3;
        public int GetWin2() => Wins2;
        public int GetLoss1() => Losses1;
        public int GetLoss() => Losses;
        public override ScoringContext GetScoringContext()
        {
            return new ScoringContext
            {
                Wins3 = Wins3,
                Wins2 = Wins2,
                Losses1 = Losses1,
                Losses = Losses
            };
        }
    }
    public class ScoringContext
    {
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int Wins3 { get; set; }
        public int Wins2 { get; set; }
        public int Losses1 { get; set; }
    }
    public interface IScoringStrategy
    {
        int Calculate(ScoringContext context);
    }
    public class FootballScoringStrategy : IScoringStrategy
    {
        public int Calculate(ScoringContext context)
        {
            return context.Wins * FootballRules.ScoringTable[MatchResult.Win] +
                   context.Draws * FootballRules.ScoringTable[MatchResult.Draw];
        }
    }
    public class BasketballScoringStrategy : IScoringStrategy
    {
        public int Calculate(ScoringContext context)
        {
            return context.Wins - context.Losses;
        }
    }
    public class VoleyballScoringStrategy : IScoringStrategy
    {
        public int Calculate(ScoringContext context)
        {
            return context.Wins3 * VoleyballRules.ScoringTable[MatchResult.Win3] +
                   context.Wins2 * VoleyballRules.ScoringTable[MatchResult.Win2] +
                   context.Losses1 * VoleyballRules.ScoringTable[MatchResult.Lose1];
        }
    }
}

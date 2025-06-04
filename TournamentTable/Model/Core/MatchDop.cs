using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Model.Core
{
    public abstract class Match
    {
        protected static int count = 0;
        public int ID { get; protected set; }
        public int Team1ID { get; protected set; }
        public int Team2ID { get; protected set; }
        public int Score1 { get; protected set; }
        public int Score2 { get; protected set; }
        public int Points1 { get; protected set; }
        public int Points2 { get; protected set; }
        public int TournamentId { get; protected set; }
        public Match(int ID1, int ID2, int score1, int score2, int tournamentId)
        {
            count++;
            ID = count;
            Team1ID = ID1;
            Team2ID = ID2;
            Score1 = score1;
            Score2 = score2;
            Score2 = score2;
            TournamentId = tournamentId;
        }
        public void SetId(int id) => ID = id;
    }
    public abstract class MatchDop<TTeam> : Match where TTeam : Team
    {

        public MatchDop(TTeam team1, TTeam team2, int score1, int score2, int tournamentId) : base(team1.Id, team2.Id, score1, score2, tournamentId)
        {
            UpdateTeamsStat(team1, team2);
            team1.AddMatch(tournamentId, this);
            team2.AddMatch(tournamentId, this);
        }
        public abstract void UpdateTeamsStat(TTeam team1, TTeam team2);
    }
    public class FootballMatch : MatchDop<FootballTeam>
    {
        public FootballMatch(FootballTeam team1, FootballTeam team2, int score1, int score2, int tournamentId)
            : base(team1, team2, score1, score2, tournamentId) { }
        public override void UpdateTeamsStat(FootballTeam team1, FootballTeam team2)
        {
            if (team1.GetOrCreateStats(TournamentId) is FootballTournamentStats stats1
                && team2.GetOrCreateStats(TournamentId) is FootballTournamentStats stats2)
            {
                var scoring = FootballRules.ScoringTable;
                if (Score1 > Score2)
                {
                    stats1.AddWin();
                    stats2.AddLoss();
                    Points1 = scoring[MatchResult.Win];
                    Points2 = scoring[MatchResult.Lose];
                }
                else if (Score1 < Score2)
                {
                    stats1.AddLoss();
                    stats2.AddWin();
                    Points1 = scoring[MatchResult.Lose];
                    Points2 = scoring[MatchResult.Win];
                }
                else
                {
                    stats1.AddDraw();
                    stats2.AddDraw();
                    Points1 = scoring[MatchResult.Draw];
                    Points2 = scoring[MatchResult.Draw];
                }
                team1.CalculatePoints(TournamentId);
                team2.CalculatePoints(TournamentId);
            }
        }
    }

    public class BasketballMatch : MatchDop<BasketballTeam>
    {
        public BasketballMatch(BasketballTeam team1, BasketballTeam team2, int score1, int score2, int tournamentId)
            : base(team1, team2, score1, score2, tournamentId) { }
        public override void UpdateTeamsStat(BasketballTeam team1, BasketballTeam team2)
        {
            if (team1.GetOrCreateStats(TournamentId) is BasketballTournamentStats stats1
                && team2.GetOrCreateStats(TournamentId) is BasketballTournamentStats stats2)
            {
                if (Score1 > Score2)
                {
                    stats1.AddWin();
                    stats2.AddLoss();
                    Points1 = 1;
                    Points2 = 0;
                }
                else if (Score1 < Score2)
                {
                    stats1.AddLoss();
                    stats2.AddWin();
                    Points1 = 0;
                    Points2 = 1;
                }
                team1.CalculatePoints(TournamentId);
                team2.CalculatePoints(TournamentId);
            }
        }
    }
    public class VoleyballMatch : MatchDop<VoleyballTeam>
    {
        public VoleyballMatch(VoleyballTeam team1, VoleyballTeam team2, int score1, int score2, int tournamentId)
            : base(team1, team2, score1, score2, tournamentId) { }
        public override void UpdateTeamsStat(VoleyballTeam team1, VoleyballTeam team2)
        {
            if (team1.GetOrCreateStats(TournamentId) is VoleyballTournamentStats stats1
                && team2.GetOrCreateStats(TournamentId) is VoleyballTournamentStats stats2)
            {
                var scoring = VoleyballRules.ScoringTable;
                if (Score1 == 3)
                {
                    if (Score2 < 2)
                    {
                        stats1.AddWin3();
                        stats2.AddLoss();
                        Points1 = scoring[MatchResult.Win3];
                        Points2 = scoring[MatchResult.Lose];
                    } 
                    else
                    {
                        stats1.AddWin2();
                        stats2.AddLoss1();
                        Points1 = scoring[MatchResult.Win2];
                        Points2 = scoring[MatchResult.Lose1];
                    }
                }
                else if (Score2 == 3)
                {
                    if (Score1 < 2)
                    {
                        stats1.AddLoss();
                        stats2.AddWin3();
                        Points1 = scoring[MatchResult.Lose];
                        Points2 = scoring[MatchResult.Win3];
                    }
                    else
                    {
                        stats1.AddLoss1();
                        stats2.AddWin2();
                        Points1 = scoring[MatchResult.Lose1];
                        Points2 = scoring[MatchResult.Win2];
                    }
                }
                team1.CalculatePoints(TournamentId);
                team2.CalculatePoints(TournamentId);
            }
        }
    }
}

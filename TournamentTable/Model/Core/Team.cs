using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.Core
{
    public abstract class Team
    {
        private static int count = 0;
        protected Dictionary<int, ITournamentStats> _tournamentsData = new Dictionary<int, ITournamentStats>();
        public Dictionary<int, ITournamentStats> TournamentsData => _tournamentsData;
        protected IScoringStrategy _scoringStrategy;
        protected ITournamentStatsCreator _tournamentStatsCreator;
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public Team(string name, IScoringStrategy scoringStrategy, ITournamentStatsCreator tournamentStatsCreator)
        {
            Name = name;
            count++;
            Id = count;
            _scoringStrategy = scoringStrategy;
            _tournamentStatsCreator = tournamentStatsCreator;
        }
        public void SetId(int id) => Id = id;
        public ITournamentStats GetOrCreateStats(int tournamentId)
        {
            if (_tournamentsData.TryGetValue(tournamentId, out var stats))
                return stats;

            stats = _tournamentStatsCreator.Create();
            _tournamentsData[tournamentId] = stats;
            return stats;
        }
        public virtual void CalculatePoints(int tournamentId)
        {
            var stats = GetOrCreateStats(tournamentId);
            var context = stats.GetScoringContext();
            int points = _scoringStrategy.Calculate(context);
            stats.SetPoints(points);
        }
        public void AddMatch(int tournamentId, Match match)
            => GetOrCreateStats(tournamentId).AddMatch(match);
        public void SetPlace(int tournamentId, int place)
            => GetOrCreateStats(tournamentId).SetPlace(place);
        public void SetEqualityId(int tournamentId, string id)
            => GetOrCreateStats(tournamentId).SetEqualityId(id);
        public void Disqual(int tournamentId)
            => GetOrCreateStats(tournamentId).Disqual();
        public void AddEqualityPoints(int tournamentId, int equalityPoints)
            => GetOrCreateStats(tournamentId).AddEqualityPoints(equalityPoints);
        public bool GetDisqual(int tournamentId)
            => GetOrCreateStats(tournamentId).Disqualification;
        public string GetEqualityId(int tournamentId)
            => GetOrCreateStats(tournamentId).EqualityId;
        public int GetEqualityPoints(int tournamentId)
            => GetOrCreateStats(tournamentId).EqualityPoints;
        public int GetPlace(int tournamentId)
            => GetOrCreateStats(tournamentId).Place;
    }
    public class FootballTeam : Team
    {
        public FootballTeam(string name)
            : base(name, new FootballScoringStrategy(), new FootballTournamentStatsCreator()) { }
    }
    public class BasketballTeam : Team
    {
        public BasketballTeam(string name)
            : base(name, new BasketballScoringStrategy(), new BasketballTournamentStatsCreator()) { }
    }
    public class VoleyballTeam : Team
    {
        public VoleyballTeam(string name)
            : base(name, new VoleyballScoringStrategy(), new VoleyballTournamentStatsCreator()) { }
    }

}

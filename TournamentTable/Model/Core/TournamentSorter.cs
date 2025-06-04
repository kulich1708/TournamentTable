using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public class FootballSorter : ITournamentSorter<FootballTeam>
    {
        public List<FootballTeam> Sort(int tournamentId, List<FootballTeam> teams)
        {
            teams = teams.OrderByDescending(t => t.GetOrCreateStats(tournamentId).Points)
                       .ThenByDescending(t => { if (t.GetOrCreateStats(tournamentId) is FootballTournamentStats stats) return stats.Wins; return 0; })
                       .ThenByDescending(t => { if (t.GetOrCreateStats(tournamentId) is FootballTournamentStats stats) return stats.Draws; return 0; })
                       .ToList();
            foreach (var t in teams)
                if (t.GetOrCreateStats(tournamentId) is FootballTournamentStats stats)
                    t.SetEqualityId(tournamentId, $"Points{stats.Points}Wins{stats.Wins}Draws{stats.Draws}");

            return teams;
        }
    }
    public class BasketballSorter : ITournamentSorter<BasketballTeam>
    {
        public List<BasketballTeam> Sort(int tournamentId, List<BasketballTeam> teams)
        {
            teams = teams.OrderByDescending(t => t.GetOrCreateStats(tournamentId).Points)
                       .ThenByDescending(t => { if (t.GetOrCreateStats(tournamentId) is BasketballTournamentStats stats) return stats.Wins; return 0; })
                       .ToList();
            foreach (var t in teams)
                if (t.GetOrCreateStats(tournamentId) is BasketballTournamentStats stats)
                    t.SetEqualityId(tournamentId, $"Points{stats.Points}Wins{stats.Wins}");

            return teams;
        }
    }
    public class VoleyballSorter : ITournamentSorter<VoleyballTeam>
    {
        public List<VoleyballTeam> Sort(int tournamentId, List<VoleyballTeam> teams)
        {
            teams = teams.OrderByDescending(t => t.GetOrCreateStats(tournamentId).Points)
                       .ThenByDescending(t => { if (t.GetOrCreateStats(tournamentId) is VoleyballTournamentStats stats) return stats.Wins3; return 0; })
                       .ThenByDescending(t => { if (t.GetOrCreateStats(tournamentId) is VoleyballTournamentStats stats) return stats.Wins2; return 0; })
                       .ThenByDescending(t => { if (t.GetOrCreateStats(tournamentId) is VoleyballTournamentStats stats) return stats.Losses1; return 0; })
                       .ToList();
            foreach (var t in teams)
                if (t.GetOrCreateStats(tournamentId) is VoleyballTournamentStats stats)
                    t.SetEqualityId(tournamentId, $"Points'{stats.Points}'Wins3'{stats.Wins3}''Wins2'{stats.Wins2}''Losses1'{stats.Losses1}'");

            return teams;
        }
    }
}

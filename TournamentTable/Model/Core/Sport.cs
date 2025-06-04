using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Model.Core
{

    public abstract class Sport<TTournament, TTeam> where TTournament : TournamentTable<TTeam> where TTeam : Team
    {
        public string sportName { get; protected set; }
        public List<MatchDop<TTeam>> Matches { get; protected set; } = new List<MatchDop<TTeam>>();
        public List<TTournament> Tournaments { get; protected set; } = new List<TTournament>();
        public void AddTournaments(TTournament[] tables) => Tournaments.AddRange(tables);
        public void AddMatches(MatchDop<TTeam>[] matches) => Matches.AddRange(matches);
        public void AddMatches(TTournament[] tournaments)
        {
            foreach (var t in tournaments)
                AddMatches(t.Matches);
        }
        public Sport(TTournament[] tournaments)
        {
            AddTournaments(tournaments);
            AddMatches(tournaments);
        }
    }
    public class Football : Sport<FootballTournamentTable, FootballTeam>
    {
        public Football(FootballTournamentTable[] tournaments)
            : base(tournaments)
        { sportName = "Football"; }
    }
    public class Basketball : Sport<BasketballTournamentTable, BasketballTeam>
    {
        public Basketball(BasketballTournamentTable[] tournaments)
            : base(tournaments)
        { sportName = "Basketball"; }
    }
    public class Voleyball : Sport<VoleyballTournamentTable, VoleyballTeam>
    {
        public Voleyball(VoleyballTournamentTable[] tournaments)
            : base(tournaments)
        { sportName = "Voleyball"; }
    }
}

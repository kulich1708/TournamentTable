using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public abstract partial class TournamentTable<TTeam> : ITable<TTeam> where TTeam : Team
    {
        private static int count = 0;
        protected List<MatchDop<TTeam>> _matches = new List<MatchDop<TTeam>>();
        protected List<TTeam> _teams = new List<TTeam>();
        protected IMatchGenerator<TTeam> _matchGenerator;
        protected ITournamentSorter<TTeam> _tournamentSorter;
        public MatchDop<TTeam>[] Matches => _matches.ToArray();
        public TTeam[] Teams => _teams.ToArray();
        public string Sport { get; protected set; }
        public string Name { get; protected set; }
        public int Season { get; protected set; }
        public int Id { get; private set; }
        public void AddMatch(MatchDop<TTeam> match) => _matches.Add(match);
        public void AddMatches(List<MatchDop<TTeam>> matches) => _matches.AddRange(matches);
        public void AddTeam(TTeam team) => _teams.Add(team);
        public void AddTeams(IEnumerable<TTeam> teams) => _teams.AddRange(teams);
        public void SetSeason(int season) => Season = season;
        public void PlayAllMatch()
        {
            for (int i = 0; i < _teams.Count; i++)
            {
                if (i == 9) Disqual(_teams[i]);
                if (_teams[i].GetDisqual(Id)) continue;
                for (int j = i + 1; j < _teams.Count; j++)
                {
                    if (_teams[j].GetDisqual(Id)) continue;
                    Match(_teams[i], _teams[j]);
                }
            }
        }
        public void Match(TTeam team1, TTeam team2)
        {
            var match = _matchGenerator.GenerateMatch(Id, team1, team2);
            AddMatch(match);
        }

        public TournamentTable(string name, TTeam[] teams, IMatchGenerator<TTeam> matchGenerator, ITournamentSorter<TTeam> tournamentSorter, int id = 0)
        {
            Id = id == 0 ? ++count : id;
            Name = name;
            _matchGenerator = matchGenerator;
            _tournamentSorter = tournamentSorter;
            AddTeams(teams);
            //SortDefault();
            SortByScore();
        }
        public void SortDefault()
        {
            _teams = _teams.OrderBy(x => x.Name).ToList();
            for (int i = 0; i < _teams.Count; i++)
                _teams[i].SetPlace(Id, i + 1);
        }
        public void SortByScore()
        {
            _teams = _tournamentSorter.Sort(Id, _teams);
            _teams = new TournamentPlacementService<TTeam>().SetPlaces(_teams, _matches, Id);
        }
    }
    public class FootballTournamentTable : TournamentTable<FootballTeam>
    {
        public FootballTournamentTable(string name, FootballTeam[] teams, int id = 0)
            : base(name, teams, new FootballMatchGenerator(), new FootballSorter(), id) { }
    }
    public class BasketballTournamentTable : TournamentTable<BasketballTeam>
    {
        public BasketballTournamentTable(string name, BasketballTeam[] teams, int id = 0)
            : base(name, teams, new BasketballMatchGenerator(), new BasketballSorter(), id) { }
    }
    public class VoleyballTournamentTable : TournamentTable<VoleyballTeam>
    {
        public VoleyballTournamentTable(string name, VoleyballTeam[] teams, int id = 0)
            : base(name, teams, new VoleyballMatchGenerator(), new VoleyballSorter(), id) { }
    }
}

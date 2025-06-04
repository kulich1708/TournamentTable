using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public abstract class TournamentGenerator<TTournament, TTeam> : ITournamentsGenerator<TTournament, TTeam>
        where TTournament : TournamentTable<TTeam> where TTeam : Team
    {
        protected string[] _tournamentNames;
        protected static readonly Random Random = new Random();
        protected TTournament[] _tournaments;
        protected TTeam[] _teams;
        protected ICreateTournament<TTournament, TTeam> _creatorTournament;
        public TTournament[] Tournaments => _tournaments;
        public TournamentGenerator(string[] tournamentNames, TTeam[] teams, ICreateTournament<TTournament, TTeam> creatorTournament)
        {
            _tournamentNames = tournamentNames;
            _teams = teams;
            _creatorTournament = creatorTournament;
            GenerateTournaments();
        }
        public TTournament GenerateTournament(string name)
        {
            var tournamentTeams = _teams.OrderBy(x => Random.Next()).Take(Random.Next(5, 21)).ToArray();
            var tournament = _creatorTournament.Create(name, tournamentTeams);
            tournament.PlayAllMatch();

            return tournament;
        }
        public void GenerateTournaments()
        {
            _tournamentNames = _tournamentNames.OrderBy(x => Random.Next()).ToArray();
            int numberOfTournaments = 3;
            var tournaments = new TTournament[numberOfTournaments];
            for (int i = 0; i < numberOfTournaments; i++)
            {
                var tournament = GenerateTournament(_tournamentNames[i]);
                tournament.SetSeason(2025 - i);
                tournaments[i] = tournament;
            }

            _tournaments = tournaments;
        }
    }
    public class FootballTournamentGenerator : TournamentGenerator<FootballTournamentTable, FootballTeam>
    {
        private static string[] _footballTournamentNames = new string[]
        {
            "Premier League",
            "La Liga",
            "Bundesliga",
            "Serie A",
            "Ligue 1"
        };
        public FootballTournamentGenerator(FootballTeam[] teams)
            : base(_footballTournamentNames, teams, new CreateFootballTournament()) { }

    }
    public class BasketballTournamentGenerator : TournamentGenerator<BasketballTournamentTable, BasketballTeam>
    {
        private static string[] _BasketballTournamentNames = new string[]
        {
            "NBA (National Basketball Association)",
    "EuroLeague",
    "Liga ACB (Испания)",
    "VTB United League",
    "CBA (Chinese Basketball Association)"
        };
        public BasketballTournamentGenerator(BasketballTeam[] teams)
            : base(_BasketballTournamentNames, teams, new CreateBasketballTournament()) { }

    }
    public class VoleyballTournamentGenerator : TournamentGenerator<VoleyballTournamentTable, VoleyballTeam>
    {
        private static string[] _voleyballTournamentNames = new string[]
        {
            "Суперлига России",
    "Итальянская Серия А1",
    "Польская ПлюсЛига",
    "Турецкая Суперлига",
    "Бразильская Суперлига"
        };
        public VoleyballTournamentGenerator(VoleyballTeam[] teams)
            : base(_voleyballTournamentNames, teams, new CreateVoleyballTournament()) { }

    }
}
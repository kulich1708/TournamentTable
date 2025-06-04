using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public abstract class TeamsGenerator<TTeam> where TTeam : Team
    {
        protected static readonly Random Random = new Random();
        protected ICreateTeam<TTeam> _creatorTeam;
        protected int _numberOfTeam;
        protected string[] _teamNames;
        protected TTeam[] _teams;

        public TTeam[] Teams => _teams;
        public TeamsGenerator(string[] teamNames, ICreateTeam<TTeam> creatorTeam)
        {
            _creatorTeam = creatorTeam;
            _numberOfTeam = Random.Next(20, 21);
            _teamNames = teamNames;
            GenerateTeams();
        }
        private void GenerateTeams()
        {
            _teamNames = _teamNames.OrderBy(x => Random.Next()).Take(_numberOfTeam).ToArray();
            var teams = new TTeam[_numberOfTeam];

            for (int i = 0; i < _numberOfTeam; i++)
                teams[i] = _creatorTeam.Create(_teamNames[i]);
            _teams = teams;
        }
    }
    public class FootballTeamsGenerator : TeamsGenerator<FootballTeam>
    {
        private static string[] footballTeamNames = new string[]
        {
                "Manchester City",
                "Liverpool",
                "Chelsea",
                "Arsenal",
                "Manchester United",
                "Real Madrid",
                "FC Barcelona",
                "Atlético Madrid",
                "Bayern Munich",
                "Borussia Dortmund",
                "RB Leipzig",
                "Juventus",
                "AC Milan",
                "Inter Milan",
                "Paris Saint-Germain",
                "Marseille",
                "Napoli",
                "Bayer Leverkusen",
                "Tottenham Hotspur",
                "Newcastle United"
        };
        public FootballTeamsGenerator() : base(footballTeamNames, new CreateFootballTeam()) { }
    }
    public class BasketballTeamsGenerator : TeamsGenerator<BasketballTeam>
    {
        private static string[] basketballTeamNames = new string[]
{
    "Los Angeles Lakers",
    "Boston Celtics",
    "Chicago Bulls",
    "Golden State Warriors",
    "San Antonio Spurs",
    "Miami Heat",
    "Houston Rockets",
    "Oklahoma City Thunder",
    "Toronto Raptors",
    "Dallas Mavericks",
    "Philadelphia 76ers",
    "Brooklyn Nets",
    "Milwaukee Bucks",
    "Denver Nuggets",
    "Atlanta Hawks",
    "Phoenix Suns",
    "CSKA Moscow",
    "Zenit Saint Petersburg",
    "UNICS Kazan",
    "Real Madrid"
};
        public BasketballTeamsGenerator() : base(basketballTeamNames, new CreateBasketballTeam()) { }
    }
    public class VoleyballTeamsGenerator : TeamsGenerator<VoleyballTeam>
    {
        private static string[] VoleyballTeamNames = new string[]
        {
                "Zenit Kazan",
    "Lokomotiv Novosibirsk",
    "Dynamo Moscow",
    "Belogorie Belgorod",
    "Kuzbass Kemerovo",
    "Trentino Volley",
    "Sir Safety Perugia",
    "Cucine Lube Civitanova",
    "PGE Skra Bełchatów",
    "ZAKSA Kędzierzyn-Koźle",
    "Asseco Resovia Rzeszów",
    "VC Dynamo-LO Saint Petersburg",
    "Sada Cruzeiro",
    "Funvic Taubaté",
    "Itas Trentino",
    "VakıfBank Istanbul",
    "Eczacıbaşı VitrA",
    "Fenerbahçe Opet",
    "Imoco Volley Conegliano",
    "Tauron MKS Dąbrowa Górnicza"
        };
        public VoleyballTeamsGenerator() : base(VoleyballTeamNames, new CreateVoleyballTeam()) { }
    }
}

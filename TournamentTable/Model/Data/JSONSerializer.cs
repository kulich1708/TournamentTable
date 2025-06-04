using Model.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Model.Data
{
    public abstract class JSONSerializer<TSport, TTournament, TTeam, TMatch> : FileSerializer 
        where TSport : Sport<TTournament, TTeam> 
        where TTournament : TournamentTable<TTeam> 
        where TTeam : Team where TMatch : MatchDop<TTeam>
    {
        private JsonSerializerSettings settings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.All,
            //Formatting = Formatting.Indented
        };
        protected ICreateTeam<TTeam> _creatorTeam;
        protected ICreateTournament<TTournament, TTeam> _creatorTournament;
        protected ICreateMatch<TMatch, TTeam> _creatorMatch;
        protected ICreateSport<TSport, TTournament, TTeam> _creatorSport;
        public override string Extension => "json";
        public JSONSerializer(
            string fileName,
            ICreateSport<TSport, TTournament, TTeam> creatorSport,
            ICreateTournament<TTournament, TTeam> creatorTournament, 
            ICreateTeam<TTeam> creatorTeam, 
            ICreateMatch<TMatch, TTeam> creatorMatch)
        {
            SelectFolder(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Tournament"));
            SelectFile(fileName);
            _creatorSport = creatorSport;
            _creatorTournament = creatorTournament;
            _creatorTeam = creatorTeam;
            _creatorMatch = creatorMatch;
        }
        
        public void SerializeObject<T>(T obj)
        {
            if (obj == null || string.IsNullOrEmpty(FilePath)) return;
            
            string dataJson = JsonConvert.SerializeObject(obj, settings);

            using (var writer = new StreamWriter(FilePath)) writer.Write(dataJson);
        }
        public JToken DeserializeData()
        {
            if (string.IsNullOrEmpty(FilePath) || !File.Exists(FilePath)) return null;
            string dataJson = string.Empty;
            using (var reader = new StreamReader(FilePath)) dataJson = reader.ReadToEnd();
            var tournamentData = JsonConvert.DeserializeObject<JToken>(dataJson, settings);

            return tournamentData;
        }
        protected TMatch DeserializeMatch(JObject matchJson, TTeam[] allTeams, Dictionary<string, TMatch> dictionaryAllMatches)
        {
            if (matchJson.ContainsKey("$ref"))
            {
                var key = matchJson["$ref"].ToObject<string>();
                if (dictionaryAllMatches.ContainsKey(key))
                    return dictionaryAllMatches[key];
                return null;
            }
            else
            {
                int Id = matchJson["ID"].ToObject<int>();
                int team1ID = matchJson["Team1ID"].ToObject<int>();
                int team2ID = matchJson["Team2ID"].ToObject<int>();
                int score1 = matchJson["Score1"].ToObject<int>();
                int score2 = matchJson["Score2"].ToObject<int>();
                int tournamentId = matchJson["TournamentId"].ToObject<int>();
                TTeam team1 = null;
                TTeam team2 = null;
                foreach (var team in allTeams)
                {
                    if (team.Id == team1ID) team1 = team;
                    else if (team.Id == team2ID) team2 = team;
                }
                if (team1 == null || team2 == null) return default;

                var match = _creatorMatch.Create(team1, team2, score1, score2, tournamentId);
                match.SetId(Id);
                dictionaryAllMatches.Add(matchJson["$id"].ToObject<string>(), match);
                return match;
            }
        }
        private TTournament DeserializeTournament(JToken tournamentJson, Dictionary<string, TTeam> dictionaryAllTeams, Dictionary<string, TMatch> dictionaryAllMatches, Dictionary<string, TTournament> dictionaryAllTournaments)
        {
            var teamsJson = tournamentJson["Teams"]["$values"];
            var matchesJson = tournamentJson["Matches"]["$values"];
            string name = tournamentJson["Name"].ToObject<string>();
            int season = tournamentJson["Season"].ToObject<int>();
            int tournamentId = tournamentJson["Id"].ToObject<int>();

            var teams = new List<TTeam>();
            foreach (JObject team in teamsJson)
            {
                teams.Add(DeserializeTeam(team, dictionaryAllTeams));
            }

            var matches = new List<TMatch>();
            foreach (JObject matchJSON in matchesJson)
            {
                var match = DeserializeMatch(matchJSON, teams.ToArray(), dictionaryAllMatches);
                if (match != null)
                    matches.Add(match);
            }

            var tournament = _creatorTournament.Create(name, teams.ToArray(), tournamentId);
            tournament.SetSeason(season);
            tournament.AddMatches(matches.Select(t => t as MatchDop<TTeam>).ToList());
            dictionaryAllTournaments.Add(tournamentId.ToString(), tournament);
            return tournament;

        }
        private TTeam DeserializeTeam(JObject teamJson, Dictionary<string, TTeam> dictionaryAllTeams)
        {
            if (teamJson.ContainsKey("$ref"))
            {
                return dictionaryAllTeams[teamJson["$ref"].ToObject<string>()];
            }
            else
            {
                int Id = teamJson["Id"].ToObject<int>();
                string name = teamJson["Name"].ToObject<string>();

                TTeam team = _creatorTeam.Create(name);
                team.SetId(Id);
                dictionaryAllTeams.Add(teamJson["$id"].ToObject<string>(), team);
                return team;
            }
        }

        public TSport DeserializeSport(JToken json)
        {
            var tournamentsJson = json["Tournaments"]["$values"];
            var tournaments = new List<TTournament>();
            var dictionaryAllTeams = new Dictionary<string, TTeam>();
            var dictionaryAllMatches = new Dictionary<string, TMatch>();
            var dictionaryAllTournaments = new Dictionary<string, TTournament>();

            var matches = new List<TMatch>();
            var matchesJson = json["Matches"]["$values"];
            foreach (var tournamentJson in tournamentsJson)
                tournaments.Add(DeserializeTournament(tournamentJson, dictionaryAllTeams, dictionaryAllMatches, dictionaryAllTournaments));
            foreach (JObject matchJson in matchesJson)
            {
                var match = DeserializeMatch(matchJson, dictionaryAllTeams.Values.ToArray(), dictionaryAllMatches);
                matches.Add(match);
                var key = match.TournamentId.ToString();
                dictionaryAllTournaments[key].AddMatch(match);
                dictionaryAllTournaments[key].SortDefault();
            }

            var sport = _creatorSport.Create(tournaments.ToArray());
            return sport;
        }
    }
    public class FootballJSONSerializer : JSONSerializer<Football, FootballTournamentTable, FootballTeam, FootballMatch>
    {
        public FootballJSONSerializer()
            : base("Football", new CreateFootball(), new CreateFootballTournament(), new CreateFootballTeam(), new CreateFootballMatch()) { }
    }
    public class BasketballJSONSerializer : JSONSerializer<Basketball, BasketballTournamentTable, BasketballTeam, BasketballMatch>
    {
        public BasketballJSONSerializer()
            : base("Basketball", new CreateBasketball(), new CreateBasketballTournament(), new CreateBasketballTeam(), new CreateBasketballMatch()) { }
    }
    public class VoleyballJSONSerializer : JSONSerializer<Voleyball, VoleyballTournamentTable, VoleyballTeam, VoleyballMatch>
    {
        public VoleyballJSONSerializer()
            : base("Voleyball", new CreateVoleyball(), new CreateVoleyballTournament(), new CreateVoleyballTeam(), new CreateVoleyballMatch()) { }
    }
    public class TestJSONSerializer : JSONSerializer<Football, FootballTournamentTable, FootballTeam, FootballMatch>
    {
        public TestJSONSerializer(string fileName) : base(fileName, new CreateFootball(), new CreateFootballTournament(), new CreateFootballTeam(), new CreateFootballMatch())
        {
        }
    }
}
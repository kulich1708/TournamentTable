using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core;
public class TournamentPlacementService<TTeam> where TTeam : Team
{
    public List<TTeam> SetPlaces(List<TTeam> teams, List<MatchDop<TTeam>> matches, int tournamentId)
    {
        for (int i = 0; i < teams.Count; i++)
        {
            // Если команда в списке первая или перед ней стоит команда с тем же айди равенства, то устанавливаем место по индексу в списке
            if (i == 0 || teams[i].GetEqualityId(tournamentId) != teams[i - 1].GetEqualityId(tournamentId))
                teams[i].SetPlace(tournamentId, i + 1);

            // Если команда не последняя и после неё идёт команда с тем же айди равенства, то у следующей команды устанавливаем место такое же как у текущей
            if (i < teams.Count - 1 && teams[i].GetEqualityId(tournamentId) == teams[i + 1].GetEqualityId(tournamentId))
                teams[i + 1].SetPlace(tournamentId, teams[i].GetPlace(tournamentId));
            /*
             В итоге в команды записывается их место по списку, но если у них равенство параметров
            то устанавливаем им одно более высокое место
             */
        }
        var EqualityIds = new Dictionary<string, List<TTeam>>(); // Словарь айди равенства - список команд
        foreach (var team in teams)
        {
            if (EqualityIds.ContainsKey(team.GetEqualityId(tournamentId)))
                EqualityIds[team.GetEqualityId(tournamentId)].Add(team);
            else EqualityIds.Add(team.GetEqualityId(tournamentId), new List<TTeam>() { team });
            // Создаём новую пару или добавляем команду в имеющуюся
        }
        //Tools.Print(EqualityIds.Count.ToString());
        foreach (var property in EqualityIds)
        {
            //Здесь работа только с командами, с одинаковым айди равенства

            var dictionaryTeams = new Dictionary<int, TTeam>(); // Словарь: айди команды - команда

            // Заполнили словарь
            foreach (var team in property.Value)
                dictionaryTeams.Add(team.Id, team);
            if (dictionaryTeams.Count == 1) continue;
            var equalityTeamMatches = FindMatchesByTeam(dictionaryTeams, matches); // Массив матчей, которые проходили при участии только этих команд

            // Подсчёт очков в этих матчах для каждой команды
            foreach (var match in equalityTeamMatches)
            {
                dictionaryTeams[match.Team1ID].AddEqualityPoints(tournamentId, match.Points1);
                dictionaryTeams[match.Team2ID].AddEqualityPoints(tournamentId, match.Points2);
            }
            var listTeams = dictionaryTeams.Values.ToList(); // Список команд
            listTeams = listTeams.OrderByDescending(t => t.GetEqualityPoints(tournamentId)).ToList(); // Сортируем команды по очкам, набранным в этих матчах

            // Правильно расставляем места. Перебор с 1, потому что у первой команды уже точно правильное место
            for (int i = 1; i < listTeams.Count; i++)
            {
                // Если у предыдущей команды столько же очков в этих матчах, то место одинаковое
                if (listTeams[i].GetEqualityPoints(tournamentId) == listTeams[i - 1].GetEqualityPoints(tournamentId))
                    listTeams[i].SetPlace(tournamentId, listTeams[i - 1].GetPlace(tournamentId));
                else
                    listTeams[i].SetPlace(tournamentId, listTeams[i].GetPlace(tournamentId) + i);
            }
        }
        teams = teams.OrderBy(t => t.GetPlace(tournamentId)).ThenBy(t => t.Name).ToList();

        return teams;
    }
    protected MatchDop<TTeam>[] FindMatchesByTeam(Dictionary<int, TTeam> teams, List<MatchDop<TTeam>> matches)
    {
        var targetMatches = new List<MatchDop<TTeam>>();
        var allMatches = matches.ToArray();
        var teamsId = teams.Keys;
        foreach (var match in allMatches)
            if (teams.ContainsKey(match.Team1ID) && teams.ContainsKey(match.Team2ID))
                targetMatches.Add(match);

        return targetMatches.ToArray();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public static class FootballRules
    {
        public static IReadOnlyDictionary<MatchResult, int> ScoringTable =>
            new Dictionary<MatchResult, int>
            {
                { MatchResult.Win, 3 },
                { MatchResult.Draw, 1 },
                { MatchResult.Lose, 0 }
            };
    }
    public static class VoleyballRules
    {
        public static IReadOnlyDictionary<MatchResult, int> ScoringTable =>
            new Dictionary<MatchResult, int>
            {
                { MatchResult.Win3, 3 },
                { MatchResult.Win2, 2 },
                { MatchResult.Lose1, 1 },
                { MatchResult.Lose, 0 }
            };
    }
}

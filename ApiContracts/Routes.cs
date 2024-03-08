namespace BettingApi.ApiContracts
{
    public class Routes
    {
           public const string BaseApiRoute = "api";

        public class MatchRoutes
        {
            public const string Match = nameof(Match);
            public const string Base = $"{BaseApiRoute}/{Match}";

            public const string Heartbeat = $"{Base}/Heartbeat";

            public const string CreateMatch = $"{Base}";

            public const string GetMatch = $"{Base}/{{id}}";

            public const string UpdateMatch = $"{Base}/{{id}}";
            
            public const string DeleteMatch = $"{Base}/{{id}}";

            public const string GetMatches = $"{Base}/all";
        }

        //Similarly for MatchOddRoutes
    }
}

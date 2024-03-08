namespace BettingApi.Models.DTO
{
    public class MatchListDto
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public DateOnly MatchDate { get; set; }

        public TimeOnly MatchTime { get; set; }

        public string TeamA { get; set; } = null!;

        public string TeamB { get; set; } = null!;

        public string SportString { get; set; } = null!;

        public int Sport { get; set; }
    }
}

namespace BettingApi.Models.DTO
{
    public class MatchOddDto
    {
        public int Id { get; set; }

        public string? Specifier { get; set; }

        public double? Odd { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BettingApi.Models.DTO
{
    public class CreateMatchRequestDto
    {
        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateOnly MatchDate { get; set; }

        [Required]
        public TimeOnly MatchTime { get; set; }

        [Required]
        public string TeamA { get; set; } = null!;

        [Required]
        public string TeamB { get; set; } = null!;

        [Required]
        public int Sport { get; set; }

        public (bool success, string? message) IsCreateMatchValid()
        {
            if (string.IsNullOrWhiteSpace(Description) || string.IsNullOrWhiteSpace(TeamA) || string.IsNullOrWhiteSpace(TeamB))
            {
                return (false, "Please provide appropriate values");
            }

            try
            {
                _ = (Enums.SportEnum)Sport; //Throws exception if int is not included in enum
            }
            catch
            {
                return (false, "Invalid");
            }

            return (true, null);
        }
    }
}

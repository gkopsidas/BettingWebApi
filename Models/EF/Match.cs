using System;
using System.Collections.Generic;

namespace BettingApi.Models.EF;

public partial class Match
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly MatchDate { get; set; }

    public TimeOnly MatchTime { get; set; }

    public string TeamA { get; set; } = null!;

    public string TeamB { get; set; } = null!;

    public int Sport { get; set; }

    public virtual ICollection<MatchOdd> MatchOdds { get; set; } = new List<MatchOdd>();
}

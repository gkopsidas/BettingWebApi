using System;
using System.Collections.Generic;

namespace BettingApi.Models.EF;

public partial class MatchOdd
{
    public int Id { get; set; }

    public int? MatchId { get; set; }

    public string? Specifier { get; set; }

    public double? Odd { get; set; }

    public virtual Match? Match { get; set; }
}

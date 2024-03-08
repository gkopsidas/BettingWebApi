using BettingApi.Models;
using BettingApi.Models.DTO;
using BettingApi.Models.EF;
using System.Collections.Generic;

namespace BettingApi.Mappings
{
    public static class MatchMappings
    {
        public static Match MapCreateMatchRequestDtoToMatch(CreateMatchRequestDto createMatchRequestDto)
        {
            ArgumentNullException.ThrowIfNull(createMatchRequestDto);

            Match match = new()
            {
                Description = createMatchRequestDto.Description,
                MatchDate = createMatchRequestDto.MatchDate,
                MatchTime = createMatchRequestDto.MatchTime,
                Sport = createMatchRequestDto.Sport,
                TeamA = createMatchRequestDto.TeamA,
                TeamB = createMatchRequestDto.TeamB
            };

            return match;
        }

        public static Match MapUpdateMatchRequestDtoToMatch(Match match, UpdateMatchRequestDto updateMatchRequestDto, int id)
        {
            ArgumentNullException.ThrowIfNull(updateMatchRequestDto);
            if (match.Id != id)
            {
                throw new Exception("Wrong entity mapping");
            }

            match.Id = id;
            match.Description = updateMatchRequestDto.Description;
            match.MatchDate = updateMatchRequestDto.MatchDate;
            match.MatchTime = updateMatchRequestDto.MatchTime;
            match.Sport = updateMatchRequestDto.Sport;
            match.TeamA = updateMatchRequestDto.TeamA;
            match.TeamB = updateMatchRequestDto.TeamB;

            return match;
        }

        public static MatchResponseDto MapMatchToMatchResponseDto(Match match)
        {
            ArgumentNullException.ThrowIfNull(match);

            MatchResponseDto matchResponseDto = new()
            {
                Id = match.Id,
                Description = match.Description,
                MatchDate = match.MatchDate,
                MatchTime = match.MatchTime,
                Sport = match.Sport,
                SportString = _ = ((Enums.SportEnum)match.Sport).ToString(),
                TeamA = match.TeamA,
                TeamB = match.TeamB,
                MatchOdds = match?.MatchOdds?.Select(x => new MatchOddDto
                {
                    Id = x.Id,
                    Odd = x.Odd,
                    Specifier = x.Specifier
                }).ToList()
            };

            return matchResponseDto;
        }

        public static List<MatchListDto> MapToGetMatchesResponse(List<Match> matches)
        {
            matches ??= new List<Match>();

            List<MatchListDto> response = new List<MatchListDto>();

            foreach (var match in matches)
            {
                MatchListDto matchListDto = new MatchListDto
                {
                    Id = match.Id,
                    Description = match.Description,
                    MatchDate = match.MatchDate,
                    MatchTime = match.MatchTime,
                    Sport = match.Sport,
                    SportString = _ = ((Enums.SportEnum)match.Sport).ToString(),
                    TeamA = match.TeamA,
                    TeamB = match.TeamB
                };
                response.Add(matchListDto);
            }

            return response;
        }
    }
}

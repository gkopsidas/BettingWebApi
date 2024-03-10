using BettingApi.Models.DTO;
using BettingApi.Models.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BettingApi.Services.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly BettingsDbContext _dbContext;
        public MatchRepository(BettingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Match?> CreateMatch(Match matchToCreate)
        {
            _ = await _dbContext.Matches.AddAsync(matchToCreate);
            _dbContext.SaveChanges();

            return matchToCreate;
        }

        public async Task<Match?> GetMatch(int id)
        {
            //Without join MatchOdds
            //var response = await _dbContext.Matches.FirstOrDefaultAsync(x => x.Id == id);
            
            var match = await _dbContext.Matches
                .Where(x => x.Id == id)
                .Select(p => new Match
                {
                    Id = p.Id,
                    Description = p.Description,
                    MatchDate = p.MatchDate,
                    MatchTime = p.MatchTime,
                    Sport = p.Sport,
                    TeamA = p.TeamA,
                    TeamB = p.TeamB,
                    MatchOdds = _dbContext.MatchOdds
                        .Where(x => x.MatchId == id)
                        .Select(x => new MatchOdd
                        {
                            Id = x.Id,
                            Odd = x.Odd,
                            MatchId = x.Id,
                            Specifier = x.Specifier
                        }).ToList()
                })
               .FirstOrDefaultAsync();

            //This does not gets Match with zero related Match Odds
            //var match = await _dbContext.Matches
            //   .Where(x => x.Id == id)
            //   .Join(_dbContext.MatchOdds,
            //         p => p.Id,
            //         e => e.MatchId,
            //         (p, e) => new
            //         {
            //             Id = p.Id,
            //             Description = p.Description,
            //             MatchDate = p.MatchDate,
            //             MatchTime = p.MatchTime,
            //             Sport = p.Sport,
            //             TeamA = p.TeamA,
            //             TeamB = p.TeamB,
            //             OddId = e.Id,
            //             Odd = e.Odd,
            //             Specifier = e.Specifier
            //         })
            //   .GroupBy(x => x.Id)
            //   .Select(p => new Match
            //   {
            //       Id = p.Key,
            //       Description = p.First().Description,
            //       MatchDate = p.First().MatchDate,
            //       MatchTime = p.First().MatchTime,
            //       Sport = p.First().Sport,
            //       TeamA = p.First().TeamA,
            //       TeamB = p.First().TeamB,
            //       MatchOdds = p.Select(x => new MatchOdd
            //       {
            //           Id = x.OddId,
            //           Odd = x.Odd,
            //           MatchId = x.Id,
            //           Specifier = x.Specifier
            //       }).ToList()
            //   })
            //   .FirstOrDefaultAsync();

            return match;
        }

        public Match? UpdateMatch(Match matchToUpdate)
        {
            _ = _dbContext.Matches.Update(matchToUpdate);
            _dbContext.SaveChanges();

            return matchToUpdate;
        }

        public bool DeleteMatch(Match match)
        {
            try
            {
                _ = _dbContext.Matches.Remove(match);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Match>> GetMatches(int skip = 0, int take = 20, string? descriptionContains = null, bool sortAscending = true)
        {
            List<Match> response;
            Expression<Func<Match, bool>> whereClause = x => true;
            if (!string.IsNullOrWhiteSpace(descriptionContains))
            {
                whereClause = x => x.Description != null && x.Description.Contains(descriptionContains);
            }

            if (sortAscending)
            {
                response = await _dbContext.Matches.Where(x => true)
                    .Where(whereClause)
                    .Skip(skip)
                    .Take(take)
                    .OrderBy(x => x.MatchDate)
                    .ToListAsync();
            }
            else
            {
                response = await _dbContext.Matches
                    .Where(whereClause)
                    .Skip(skip)
                    .Take(take)
                    .OrderByDescending(x => x.MatchDate)
                    .ToListAsync();
            }

            return response;
        }
    }
}

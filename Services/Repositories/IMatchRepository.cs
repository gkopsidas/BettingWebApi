using BettingApi.Models.EF;

namespace BettingApi.Services.Repositories
{
    public interface IMatchRepository
    {
        Task<Match?> CreateMatch(Match matchToCreate);
        Task<Match?> GetMatch(int id);
        Match? UpdateMatch(Match matchToCreate);
        bool DeleteMatch(Match match);
        Task<List<Match>> GetMatches(int skip = 0, int take = 20, string? descriptionContains = null, bool sortAscending = true);
    }
}

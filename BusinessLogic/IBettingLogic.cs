using BettingApi.Models.EF;

namespace BettingApi.BusinessLogic
{
    public interface IBettingLogic
    {
        #region MatchOperations
        Task<Match?> CreateMatch(Match matchToCreate);
        Match? UpdateMatch(Match matchToUpdate);
        Task<Match?> GetMatch(int id);
        bool DeleteMatch(Match match);
        Task<List<Match>> GetMatches(int skip, int take, string? descriptionContains, bool sortAscending);
        #endregion

        //Similar we implement MatchOdd CRUD operations

        //Task<MatchOdd?> CreateMatchOdd(MatchOdd matchOddToCreate);
        //Match UpdateMatchOdd(MatchOdd matchOddToUpdate);
        //Task<MatchOdd?> GetMatchOdd(int id);
        //Task<bool> DeleteMatchOdd(int id);
        //Task<List<MatchOdd>> GetMatchOdds(int skip, int take, string filterContains, bool sortAscending);
    }
}

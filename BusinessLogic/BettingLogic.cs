using BettingApi.Models.EF;
using BettingApi.Services;

namespace BettingApi.BusinessLogic
{
    public class BettingLogic : IBettingLogic
    {
        private readonly IUnitOfWork _unitOfWork;

        public BettingLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region MatchOperations
        public async Task<Match?> CreateMatch(Match matchToCreate) =>
            await _unitOfWork.MatchRepository.CreateMatch(matchToCreate);

        public Match? UpdateMatch(Match matchToUpdate) =>
            _unitOfWork.MatchRepository.UpdateMatch(matchToUpdate);

        public async Task<Match?> GetMatch(int id) =>
            await _unitOfWork.MatchRepository.GetMatch(id);

        public bool DeleteMatch(Match match) =>
            _unitOfWork.MatchRepository.DeleteMatch(match);

        public async Task<List<Match>> GetMatches(int skip, int take, string? descriptionContains, bool sortAscending) =>
            await _unitOfWork.MatchRepository.GetMatches(
                skip: skip,
                take: take,
                descriptionContains: descriptionContains,
                sortAscending: sortAscending);
        #endregion

        //Similarly we implement MatchOdd CRUD operations

        //public async Task<MatchOdd> CreateMatchOdd(MatchOdd matchOddToCreate) =>
        //    await _unitOfWork.MatchRepository.CreateMatchOdd(matchOddToCreate);

        //public async Task<MatchOdd> UpdateMatchOdd(MatchOdd matchOddToCreate) =>
        //    await _unitOfWork.MatchRepository.UpdatMatchOdd(matchOddToCreate);

        //public async Task<MatchOdd> GetMatchOdd(int id) =>
        //    await _unitOfWork.MatchRepository.GetMatchOdd(id);

        //public async Task<bool> DeleteMatchOdd(int id) =>
        //    await _unitOfWork.MatchRepository.DeleteMatchOdd(id);

        //public async Task<List<MatchOdd>> GetMatchOdds(List<int> ids) =>
        //    await _unitOfWork.MatchRepository.GetMatchOdds(ids);
    }
}

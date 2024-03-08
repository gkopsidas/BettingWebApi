using BettingApi.Services.Repositories;

namespace BettingApi.Services
{
    public interface IUnitOfWork
    {
        IMatchRepository MatchRepository { get; }
    }
}

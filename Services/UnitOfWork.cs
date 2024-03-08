using BettingApi.Models.EF;
using BettingApi.Services.Repositories;

namespace BettingApi.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private IMatchRepository? _matchRepository;
        private readonly BettingsDbContext _dbContext;

        public UnitOfWork(BettingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMatchRepository MatchRepository
        {
            get
            {
                if (_matchRepository == null)
                {
                    _matchRepository = new MatchRepository(_dbContext);
                }

                return _matchRepository;
            }
        }

        //Similarly we would use MatchOddRepository. In case we have many entities we may implement seperate services for some repositories.
    }
}

namespace TaskMgmt.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public Task<int> CommitAsync();
        public Task BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollbackAsync();
    }
}

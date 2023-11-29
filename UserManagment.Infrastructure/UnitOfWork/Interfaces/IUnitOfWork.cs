using UserManagment.Infrastructure.Repositories.Interfaces;

namespace UserManagment.Infrastructure.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        public IBaseRepository BaseRepository { get; }
    }
}

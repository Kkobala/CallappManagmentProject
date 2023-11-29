using UserManagment.Infrastructure.Repositories.Interfaces;
using UserManagment.Infrastructure.UnitOfWork.Interfaces;

namespace UserManagment.Infrastructure.UnitOfWork.Implementations
{
    public class UnitOfWork: IUnitOfWork
    {
        public IBaseRepository BaseRepository { get; }

        public UnitOfWork(IBaseRepository baseRepository)
        {
            BaseRepository = baseRepository;
        }
    }
}

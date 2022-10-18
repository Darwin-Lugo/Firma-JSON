using Firma.Core.Entitys;

namespace Firma.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Persona> UserRepository { get; }

        void Dispose();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
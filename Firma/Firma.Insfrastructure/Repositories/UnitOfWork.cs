#region References
using Firma.Core.Entitys;
using Firma.Core.Interfaces;
using Firma.Insfrastructure.Entity;
#nullable disable 
#endregion

namespace Firma.Insfrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PruebaContext _context;
        private readonly IRepository<Persona> _userRepository;
        public UnitOfWork(PruebaContext context) => _context = context;
        public IRepository<Persona> UserRepository => _userRepository ?? new Repository<Persona>(_context);

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public void SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}

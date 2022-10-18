#region references
using Firma.Core.Entitys;
using Firma.Core.Interfaces;
using Firma.Insfrastructure.Entity;
using Microsoft.EntityFrameworkCore;
#nullable disable 
#endregion

namespace Firma.Insfrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        #region InjectionDependency
        private readonly PruebaContext _context;
        protected readonly DbSet<T> _entities;
        public Repository(PruebaContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        #endregion

        public IQueryable<T> GetAlls() => _entities.AsEnumerable().AsQueryable();
        public async Task<T> GetById(int id) => await _entities.FindAsync(id);
        public async Task Add(T entity) => await _entities.AddAsync(entity);
        public void Update(T entity) => _entities.Update(entity);
        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
        }
    }
}

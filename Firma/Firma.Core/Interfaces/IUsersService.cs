using Firma.Core.CustomEntities;
using Firma.Core.Entitys;
using Firma.Core.QueryFilters;

namespace Firma.Core.Interfaces
{
    public interface IUsersService
    {
        PageList<Persona> GetAlls(Filter filter);
        Task<Persona> GetById(int id);
        Task Insert(Persona persona);
        Task<bool> Delete(int id);
    }
}
#region References
using Firma.Core.CustomEntities;
using Firma.Core.Entitys;
using Firma.Core.Interfaces;
using Firma.Core.QueryFilters;
using Firma.Core.Utils;
using Microsoft.Extensions.Options;
using System.Text.Json;
#endregion

namespace Firma.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public UsersService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public PageList<Persona> GetAlls(Filter filter)
        {
            filter.PageNumber = filter.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filter.PageNumber;
            filter.PageSize = filter.PageSize == 0 ? _paginationOptions.DefaultPageSize : filter.PageSize;
            var users = _unitOfWork.UserRepository.GetAlls();
            users = Filter(filter, users);
            var pageUsers = PageList<Persona>.Create(users, filter.PageNumber, filter.PageSize);
            return pageUsers;
        }

        public async Task Insert(Persona persona)
        {
            persona.Name = persona.Name;
            persona.Age = persona.Age;
            persona.DateCrea = DateTime.Now;
            var p = JsonSerializer.Serialize(persona);
            persona.Firma = Hash.Encritys(p);
            persona.StatusData = "Integro";
            await _unitOfWork.UserRepository.Add(persona);
            _unitOfWork.SaveChanges();
        }

        public async Task<Persona> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            return user;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if (user != null)
            {
                await _unitOfWork.UserRepository.Delete(user.Id);
                _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Filters
        private static IQueryable<Persona> Filter(Filter filter, IQueryable<Persona> users)
        {
            _ = (filter.Id != 0) ? users = users.Where(x => x.Id.Equals(filter.Id)) :
                (filter.Name != null) ? users = users.Where(x => x.Name.Equals(filter.Name)) :
                (filter.Age != 0) ? users = users.Where(x => x.Age.Equals(filter.Age)) :
                (filter.StatusData != null) ? users = users.Where(x => x.StatusData.Equals(filter.StatusData)) :
                default;

            return users;
        }
        #endregion
    }
}







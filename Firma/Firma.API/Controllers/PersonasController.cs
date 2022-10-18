#region References
using Firma.Insfrastructure.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Firma.API.Responses;
using System.Net;
using System.Diagnostics;
using Firma.Core.CustomEntities;
using Firma.Core.Interfaces;
using Firma.Core.QueryFilters;
using Firma.Core.Entitys;
using Newtonsoft.Json;
using AutoMapper;
using Firma.Core.DTOs;
#endregion

namespace Firma.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        public PersonasController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [ActionName(nameof(GetAllsUsers))]
        [HttpGet(Name = nameof(GetAllsUsers))]
        public IActionResult GetAllsUsers([FromQuery] Filter filters)
        {
            Stopwatch time = Time();
            var users = _usersService.GetAlls(filters);
            if (users.Any())
            {
                var metadata = new MetaData
                {
                    TotalCount = users.TotalCount,
                    PageSize = users.PageSize,
                    CurrentPage = users.CurrentPage,
                    TotalPages = users.TotalPages,
                    HasNextPage = users.HasNextPage,
                    HasPreviousPage = users.HasPreviousPage
                };
                time.Stop();
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                Response.Headers.Add("X-RequestTime", Convert.ToString(time.Elapsed));
                var response = new APIResponses<IQueryable<Persona>>(users.ToList().AsQueryable())
                {
                    Version = "V1",
                    Meta = metadata,
                    StatusCode = (int)HttpStatusCode.OK
                };
                return Ok(response);
            }
            else
            {
                time.Stop();
                return NoContent();
            }
        }

        [ActionName(nameof(GetById))]
        [HttpGet(template: "{id}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById(int id)
        {
            Stopwatch time = Time();
            var user = await _usersService.GetById(id);
            if(user != null)
            {
                time.Stop();
                Response.Headers.Add("X-RequestTime", Convert.ToString(time.Elapsed));
                var response = new APIResponses<Persona>(user)
                {
                    Version = "V1",
                    Meta = null,
                    StatusCode = (int)HttpStatusCode.OK
                };
                return Ok(response);
            }
            else
            {
                time.Stop();
                return NotFound();
            }
        }

        [ActionName(nameof(Post))]
        [HttpPost(template: "")]
        public async Task<IActionResult> Post([FromBody] PersonaDTO personaDto)
        {
            Stopwatch time = Time();
            var persona = _mapper.Map<Persona>(personaDto);
            await _usersService.Insert(persona);
            var personaOut = _mapper.Map<PersonaDTO>(personaDto);
            var response = new APIResponses<PersonaDTO>(personaOut)
            {
                Version = "V1",
                StatusCode = (int)HttpStatusCode.Created
            };
            Response.Headers.Add("X-RequestTime", Convert.ToString(time.Elapsed));
            time.Stop();
            return Created("Exitoso", response);
        }

       
        [ActionName(nameof(Delete))]
        [HttpDelete(template: "{id}", Name = nameof(Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            Stopwatch time = Time();
            var user = await _usersService.Delete(id);
            if (user == false)
            {
                return NotFound();
            }
            Response.Headers.Add("X-RequestTime", Convert.ToString(time.Elapsed));
            time.Stop();

            return NoContent();
        }

        #region Time
        private static Stopwatch Time()
        {
            var time = new Stopwatch();
            time.Start();
            return time;
        }
        #endregion
    }
}

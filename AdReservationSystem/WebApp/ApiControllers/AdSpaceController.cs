#pragma warning disable 1591
using AutoMapper;
using DAL;
using DAL.Contacts.App;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;
using Public.DTO.v1;


namespace WebApp.ApiControllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdSpaceController : ControllerBase
{
    private readonly AdSpaceMapper _adSpaceMapper;
    private readonly IAppUOW _uow;

    /// <summary>
    /// Constructor for AdSpaceController
    /// </summary>
    /// <param name="uow"></param>
    /// <param name="mapper"></param>
    public AdSpaceController(IAppUOW uow, IMapper mapper)
    {
        _uow = uow;
        _adSpaceMapper = new AdSpaceMapper(mapper);
    }

    // GET: api/AdSpace
    /// <summary>
    /// Gets all AdSpaces 
    /// </summary>
    /// <returns>List of all AdSpaces</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdSpace>>> GetAdSpaces()
    {
        var adSpaces = await _uow.AdSpaceRepository.AllAsync();
        var res = new List<AdSpaceMin>();
        foreach (var adSpace in adSpaces)
        {
            res.Add(_adSpaceMapper.Map(adSpace)!);
        }
        return Ok(res);
    }
    
    // GET: api/AdSpaces/5
    /// <summary>
    /// Gets an AdSpace by the ID 
    /// </summary>
    /// <param name="id">AdSpace id (Guid)</param>
    /// <returns>AdSpace</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AdSpace>> GetAdSpace(Guid id)
    {
        var adSpace = await _uow.AdSpaceRepository.FindAsync(id);
        
        if (adSpace == null)
        {
            return NotFound();
        }
        return Ok(adSpace);
    }
}
#pragma warning restore 1591
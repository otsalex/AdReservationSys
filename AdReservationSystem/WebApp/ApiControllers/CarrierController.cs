using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;
using Public.DTO.v1;

namespace WebApp.ApiControllers;

/// <summary>
/// API controller for Carriers
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CarrierController : ControllerBase
{
    private readonly CarrierMapper _carrierMapper;
    private readonly IAppBLL _bll;

    /// <summary>
    /// Constructs a new CarrierController instance
    /// </summary>
    /// <param name="bll">BLL instance for the controller</param>
    /// <param name="mapper">Data mapper instance for the controller</param>
    public CarrierController(IAppBLL bll, IMapper mapper)
    {
        _bll = bll;
        _carrierMapper = new CarrierMapper(mapper);
    }

    // GET: api/Carriers
    /// <summary>
    /// Gets all Carriers
    /// </summary>
    /// <returns>JWTResponse with jwt and refresh token</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.CarrierMin>>> GetCarriers()
    {
        var carriers = await _bll.CarrierService.AllAsync();
        return Ok(carriers);
    }
    
    // GET: api/Carriers/5
    /// <summary>
    /// Gets a Carrier by the ID 
    /// </summary>
    /// <param name="id">Carrier id (Guid)</param>
    /// <returns>Carrier</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CarrierWithAdSpaces>> GetCarrier(Guid id)
    {
        var carrier = await _bll.CarrierService.FindAsync(id);
        if (carrier == null) {
            return NotFound();
        }
        var res = _carrierMapper.MapWithAdSpaces(carrier);
        
        return Ok(res);
    }
    
    
}
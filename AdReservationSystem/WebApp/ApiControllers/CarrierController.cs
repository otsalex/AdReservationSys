﻿using AutoMapper;
using DAL.Contacts.App;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers;

/// <summary>
/// API controller for Carriers
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CarrierController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly CarrierMapper _carrierMapper;

    /// <summary>
    /// Constructs a new CarrierController instance
    /// </summary>
    /// <param name="uow">Unit of work instance for the controller</param>
    /// <param name="mapper">Data mapper instance for the controller</param>
    public CarrierController(IAppUOW uow, IMapper mapper)
    {
        _uow = uow;
        _carrierMapper = new CarrierMapper(mapper);
    }

    // GET: api/Carriers
    /// <summary>
    /// Gets all Carriers
    /// </summary>
    /// <returns>JWTResponse with jwt and refresh token</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Carrier>>> GetCarriers()
    {
        var carriers = await _uow.CarrierRepository.AllAsync();
        return Ok(carriers);
    }
    
    // GET: api/Carriers/5
    /// <summary>
    /// Gets a Carrier by the ID 
    /// </summary>
    /// <param name="id">Carrier id (Guid)</param>
    /// <returns>Carrier</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Carrier>> GetCarrier(Guid id)
    {
        var carrier = await _uow.CarrierRepository.FindAsync(id);
        if (carrier == null) {
            return NotFound();
        }
        var res = _carrierMapper.MapWithAdSpaces(carrier);
        
        return Ok(res);
    }
    
    
}
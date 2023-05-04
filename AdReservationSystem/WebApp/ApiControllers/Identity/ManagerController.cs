using Asp.Versioning;
using DAL.Contacts.App;
using Domain.App.Identity;
using Helpers.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.v1.Identity;

namespace WebApp.ApiControllers.Identity;

/// <summary>
/// API controller for managing account
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity/[controller]")]
public class ManagerController : ControllerBase
{

    private readonly UserManager<AppUser> _userManager;
    private readonly IAppUOW _uow;


    /// <summary>
    /// Constructs a new ManagerController instance
    /// </summary>
    /// <param name="uow">Unit of work instance for the controller</param>
    /// <param name="userManager">UserManager instance for the controller</param>
    public ManagerController(IAppUOW uow, UserManager<AppUser> userManager)
    {
        _uow = uow;
        _userManager = userManager;

    }
    // GET: manager/
    /// <summary>
    /// Gets user data
    /// </summary>
    /// <returns>User data</returns>
    [HttpGet("userData")]
    public async Task<ActionResult<Public.DTO.v1.Identity.Register>> GetUserData()
    {
        AppUser user;
        try
        {
            user = (await _uow.AppUserRepository.FindAsync(User.GetUserId()))!;
        }
        catch(Exception)
        {
            return NotFound();
        }
        
        var res = new Register
        {
            Email = user.Email!,
            Password = "",
            FirstName = user.FirstName,
            LastName = user.LastName
        };
        
        return res;
    }
    // PUT: manager/updateUser
    /// <summary>
    /// Updates user data
    /// </summary>
    /// <param name="data"></param>
    /// <returns>Ok</returns>
    [HttpPut("updateUser")]
    public async Task<ActionResult<Public.DTO.v1.Identity.Register>> UpdateUserData([FromBody] Register data)
    {

        AppUser user = (await _userManager.FindByIdAsync(User.GetUserId().ToString()))!;

        user.FirstName = data.FirstName;
        user.LastName = data.LastName;
        user.Email = data.Email;

        await _userManager.UpdateAsync(user);
        
        return Ok();
    }
}
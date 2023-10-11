using Core.DTOs;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructre.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly ILoginTimeRepository _loginTime ;

    public AccountController(ITokenService tokenService, ILoginTimeRepository loginTime, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
    {
         _tokenService = tokenService;
        _loginTime = loginTime;
        _userManager = userManager;
        _signInManager = signInManager;
    }

   
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Unauthorized("Passowrd or Email has been wrong");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized("Passowrd or Email has been wrong");

        await _loginTime.AddLoginTime(user);
        return await _tokenService.RefreshTokenAsync(loginDto.Email);

    }
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {

        var userExists = await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email);
        if (userExists) return BadRequest("Email already exists");
        var user = new AppUser
        {
            Email = registerDto.Email,
            UserName = registerDto.UserName,
            Address = registerDto.Address,
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) {

            var errorMessages = result.Errors.Select(error => error.Description).ToList();
            return Unauthorized(errorMessages);
        }
        return   await _tokenService.RefreshTokenAsync(registerDto.Email);
 
    }

}

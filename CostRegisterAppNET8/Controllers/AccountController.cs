using API.Data;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto)
    {
        if (await UserExistsAsync(registerDto.Username))
        {
            return BadRequest("Username is taken");
        }

        var user = mapper.Map<AppUser>(registerDto);
        user.UserName = registerDto.Username.ToLower();

        var currencyId = await unitOfWork.CurrencyRepository.GetCurrencyId(registerDto.Currency);

        if (currencyId == 0)
        {
            return BadRequest("Currency not found");
        }

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await unitOfWork.UserCurrencyRepository.AddUserCurrencyAsync(user.Id, currencyId);

        if (!await unitOfWork.CompleteAsync())
        {
            return BadRequest("Failed to add user currency");
        }

        var userDto = new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };

        return userDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(x =>
        x.NormalizedUserName == loginDto.Username.ToUpper());

        if (user is null || user.UserName is null)
        {
            return Unauthorized("Invalid username");
        }

        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!result)
        {
            return Unauthorized();
        }

        return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExistsAsync(string username)
    {
        return await userManager.Users.AnyAsync(x =>
            x.NormalizedUserName == username.ToUpper());
        // NOTE: For EF, operator '==' should be used instead of 'Equals'.
    }
}

using API.Data;
using API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
public class UsersController(UserManager<AppUser> userManager, IMapper mapper) : BaseApiController
{
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUserAsync(string username)
    {
        var user = await userManager.FindByNameAsync(username);

        if (user is null)
        {
            return NotFound();
        }

        var userToReturn = mapper.Map<MemberDto>(user);

        return Ok(userToReturn);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUserAsync(MemberDto memberDto)
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;

        if (username is null)
        {
            return BadRequest("No username was found in token.");
        }

        var user = await userManager.FindByNameAsync(username);

        if (user is null)
        {
            return BadRequest("Could not find user.");
        }

        mapper.Map(memberDto, user);

        await userManager.UpdateAsync(user);

        return Ok();
    }
}

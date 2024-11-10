using CostRegisterAppNET8.Data;

namespace CostRegisterAppNET8.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}

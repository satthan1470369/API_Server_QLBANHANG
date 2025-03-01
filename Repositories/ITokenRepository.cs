using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace API_Server_QLBANHANG.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}

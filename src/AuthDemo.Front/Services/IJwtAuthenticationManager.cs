using System.Collections.Generic;
using System.Security.Claims;

namespace AuthDemo.Front.Services
{
	public interface IJwtAuthenticationManager
    {
        string CreateToken(IEnumerable<Claim> claims);
    }
}

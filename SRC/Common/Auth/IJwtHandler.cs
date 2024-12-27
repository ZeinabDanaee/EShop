using Microsoft.IdentityModel.JsonWebTokens;

namespace Auth
{
    public interface IJwtHandler
    {
        JsonWebToken GenerateToken(Int64 userId);
        bool ValidateToken(string token);
    }


}

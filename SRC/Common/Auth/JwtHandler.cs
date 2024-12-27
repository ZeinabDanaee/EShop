using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSigningKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(IOptions<JwtOptions> options)
        {
          _options =options.Value;
             _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader(_signingCredentials);


            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _issuerSigningKey,
                ValidIssuer = _options.Issuer,
               // ValidAudience = false,//_options.ValidAudience,
                ValidateAudience = false,//_options.ValidateAudience,
              //  ValidateLifetime = _options.ValidateLifetime
            };
        }
        public JsonWebToken GenerateToken(long userId)
        {
            var nowUtc = DateTime.UtcNow; // زمان فعلی UTC
            var expires = nowUtc.AddMinutes(_options.ExpiryMinutes); // محاسبه زمان انقضاء با افزودن دقیقه‌ها
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime(); // زمان آغاز قرن به صورت UTC
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalMilliseconds); // زمان انقضاء بر حسب میلی‌ثانیه از آغاز قرن
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalMilliseconds); // زمان فعلی بر حسب میلی‌ثانیه از آغاز قرن


            // ساختن payload
            var payload = new JwtPayload {
                { "sub", userId.ToString() },
                { "unique_name", userId.ToString() }, 
                { "iat", now }, { "exp", exp }, 
                { "iss", _options.Issuer }, 
                { "aud", _options.Audience } 
            };

          var jwtToken = new JwtSecurityToken(_jwtHeader, payload); 
          
            var tokenString = _jwtSecurityTokenHandler.WriteToken(jwtToken);
            return new JsonWebToken()
            {
               Token=tokenString,                
               Expiry= expires
            };

        }

        public bool ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }


}

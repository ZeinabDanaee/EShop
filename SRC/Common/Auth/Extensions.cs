﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth
{
    public static class Extensions
    {
        public static void AddJwt(IServiceCollection  services, IConfiguration configuration  )
        {         
            var options = new JwtOptions();
            var section = configuration.GetSection("jwt");
            section.Bind(options);
            services.Configure<JwtOptions>(section);
            services.AddSingleton<IJwtHandler,JwtHandler>();
            services.AddAuthentication()
             .AddJwtBearer(c=>
             {
                 c.RequireHttpsMetadata = false;// اجازه استفاده از HTTP در محیط‌های توسعه
                 c.SaveToken = true;
                 c.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateAudience = false,
                     ValidIssuer = options.Issuer,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)) };


             })   ;

        }

    }
}

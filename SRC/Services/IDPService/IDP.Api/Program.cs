using Asp.Versioning;
using AutoMapper;
using IDP.Application.Handler.Command.User;
using IDP.Application.Helper;
using IDP.Domain.IRepository.Command;
using IDP.Domain.IRepository.Command.Base;
using IDP.Domain.IRepository.Query;
using IDP.Infra.Data;
using IDP.Infra.Repository.Command;
using IDP.Infra.Repository.Command.Base;
using IDP.Infra.Repository.Query;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region redisconfig
builder.Services.AddStackExchangeRedisCache(opition =>
{
    opition.Configuration = builder.Configuration.GetValue<string>("CashSetting:RedisUrl");
});
// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(UserHandler).GetTypeInfo().Assembly);
builder.Services.AddScoped<IOtpRedisRepository, OtpRedisRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
builder.Services.AddTransient<ShopCommandDbContext>();
builder.Services.AddTransient<ShopQueryDbContext>();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddMassTransit(busConfig =>
{


    busConfig.SetKebabCaseEndpointNameFormatter();
    busConfig.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration.GetValue<string>("Rabbit:Host")), h =>
        {
            h.Username(builder.Configuration.GetValue<string>("Rabbit:UserNmae"));
            h.Password(builder.Configuration.GetValue<string>("Rabbit:Password"));

        });

        cfg.UseMessageRetry(r => r.Exponential(10, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(5)));

        cfg.ConfigureEndpoints(context);
    });
});



builder.Services.AddCap(options =>
{
    options.UseEntityFramework<ShopCommandDbContext>();
    options.UseDashboard(path => path.PathMatch = "/cap");
    options.UseRabbitMQ(options =>
    {
        options.ConnectionFactoryOptions = options =>
        {
            options.Ssl.Enabled = false;
            options.HostName = "localhost";
            options.UserName = "guest";
            options.Password = "guest";
            options.Port = 5672;
        };

    });
    options.FailedRetryCount = 5;
    options.FailedRetryInterval = 60;


});
//builder.Services.AddSingleton<CustomerAddedEventSubscriber>();
Auth.Extensions.AddJwt(builder.Services, builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using StackExchange.Redis;
using Edblock.Library.Constants;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();
Configure(app, builder.Environment);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddScoped(cfg =>
    {
        var projectsConnection = configuration.GetConnectionString(ConnectionNames.ProjectsConnection) ?? 
            throw new Exception("Не удалось прочитать строку подключения");
        var multiplexer = ConnectionMultiplexer.Connect(projectsConnection);

        return multiplexer.GetDatabase();
    });

    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddSwaggerGen();

    services.AddControllers();

    // accepts any access token issued by identity server
    //services.AddAuthentication("Bearer")
    //    .AddJwtBearer("Bearer", options =>
    //    {
    //        options.Authority = "https://localhost:5001";

    //        options.TokenValidationParameters = new TokenValidationParameters
    //        {
    //            ValidateAudience = false
    //        };
    //    });

    //// adds an authorization policy to make sure the token is for scope 'api1'
    //services.AddAuthorizationBuilder()
    //    .AddPolicy("ApiScope", policy =>
    //    {
    //        policy.RequireAuthenticatedUser();
    //        policy.RequireClaim("scope", IdConstants.ApiScope);
    //    });
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseRouting();
    app.UseHttpsRedirection();

    //app.UseAuthorization();
    //app.UseAuthentication();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
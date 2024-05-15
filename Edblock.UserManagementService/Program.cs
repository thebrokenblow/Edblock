using Edblock.Library.Data;
using Edblock.Library.Constants;
using Edblock.Library.UserManagementService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, builder.Environment);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<UsersDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString(ConnectionNames.UsersConnection)));

    services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<UsersDbContext>()
       .AddDefaultTokenProviders();

    services.AddSwaggerGen();

    services.AddControllers();

    // accepts any access token issued by identity server
    services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.Authority = "https://localhost:5001";

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        });

    // adds an authorization policy to make sure the token is for scope 'api1'
    services.AddAuthorizationBuilder()
        .AddPolicy("ApiScope", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", IdConstants.ApiScope);
        });
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();

    app.UseRouting();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers()
        .RequireAuthorization("ApiScope");
    });
}
using Server.Hubs.Locations.BattlePlaces;
using Server.Hubs.Locations.BasePlaces;
using Server.Hubs.Locations.CalmPlaces;
using Microsoft.EntityFrameworkCore;
using Server.Models.Interfaces;
using Server.Models.Utilities;
using Server.EntityFramework;
using Server.Repository;
using Server.Services;
using Server.Hubs;
using Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

AuthenticationConfig.ConfigureAuthentication(builder.Services, builder.Configuration);
SwaggerConfig.ConfigureSwagger(builder.Services);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<ItemRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TravelRepository>();

builder.Services.AddSingleton<UserStorage>();
builder.Services.AddSingleton<IServiceFactory<UserStorage>, ScopedServiceFactory<UserStorage>>();

builder.Services.AddSingleton<ChatStorage>();

RegistrationPlaces(builder.Services);
RegistrationServices(builder.Services);

builder.Services.AddDbContext<DbUserContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}


#if !DEBUG
app.UseHttpsRedirection();
#endif

app.UseCors();

app.MapControllers();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapHub<UserStorage>("/UserStorage");
app.MapHub<PlaceHub>("/PlaceHub");
app.MapHub<ChatHub>("/ChatHub");

app.Run();

void RegistrationPlaces(IServiceCollection services)
{
    services.AddSingleton<BasePlace, Town>();
    services.AddSingleton<BasePlace, Glade>();
    services.AddSingleton<BasePlace, DarkWood>();
    services.AddSingleton<BasePlace, PizzaLand>();
    services.AddSingleton<BasePlace, SpecialPlace>();
}

void RegistrationServices(IServiceCollection services)
{
    services.AddTransient<AuthService>();
    services.AddScoped<InventoryService>();
    services.AddTransient<PlaceService>();
    services.AddTransient<CombatService>();
    services.AddTransient<StatsService>();

    services.AddTransient<UserFactory>();
}
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server;
using Server.Hubs;
using Server.Hubs.Locations;
using Server.Hubs.Locations.BasePlaces;
using Server.Hubs.Locations.BattlePlaces;
using Server.Hubs.Locations.CalmPlaces;
using Server.Models;
using Server.Models.Interfaces;
using Server.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TravelRepository>();

builder.Services.AddTransient<AreaStorage>();
builder.Services.AddSingleton<UserStorage>();
builder.Services.AddSingleton<IServiceFactory<UserRepository>, ScopedServiceFactory<UserRepository>>();

builder.Services.AddSingleton<BattlePlace, DarkWood>();
builder.Services.AddSingleton<BattlePlace, Glade>();
builder.Services.AddSingleton<Town>();
//builder.Services.AddSingleton<DarkWood>();

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
    app.UseSwaggerUI();
}

#if !DEBUG
app.UseHttpsRedirection();
#endif


app.UseCors();
//app.UseAuthorization();

app.MapControllers();
app.UseRouting();


app.MapHub<UserStorage>("/UserStorage");
app.MapHub<PlaceHub>("/GladeHub");
//app.MapHub<Town>("/Town");
//app.MapHub<DarkWood>("/DarkWoodHub");

app.Run();

/*builder.Services.AddSingleton<DbUserContext>(provider => {
    var optionsBuilder = new DbContextOptionsBuilder<DbUserContext>();
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
    return new DbUserContext(optionsBuilder.Options);
});*/


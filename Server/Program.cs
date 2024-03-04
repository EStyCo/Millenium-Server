using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server;
using Server.Hubs;
using Server.Models;
using Server.Models.Interfaces;
using Server.Models.Locations;
using Server.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TravelRepository>();

builder.Services.AddSingleton<UserStorage>();
builder.Services.AddSingleton<IServiceFactory<UserRepository>, ScopedServiceFactory<UserRepository>>();

builder.Services.AddSingleton<Glade>();

builder.Services.AddDbContext<DbUserContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
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

app.UseAuthorization();

app.MapControllers();
app.MapHub<UserStorage>("/UserStorage");

app.Run();

/*builder.Services.AddSingleton<DbUserContext>(provider => {
    var optionsBuilder = new DbContextOptionsBuilder<DbUserContext>();
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
    return new DbUserContext(optionsBuilder.Options);
});*/
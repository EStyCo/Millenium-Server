using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UserRepository>();

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


app.Run();

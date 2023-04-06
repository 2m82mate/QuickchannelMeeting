using Microsoft.EntityFrameworkCore;
using QuickchannelMeeting.Contexts;
using QuickchannelMeeting.Repositories;
using QuickchannelMeeting.Services;
using System.Text.Json.Serialization;
using AutoMapper;
using QuickchannelMeeting.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
    //.AddJsonOptions(o =>
    //{
    //    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    //});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddScoped<IMachineService, MachineService>();

builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IMeetingHub, MeetingHub>();
builder.Services.AddDbContext<PgContext>(opt => opt.UseNpgsql(
    builder.Configuration.GetConnectionString("Postgres"))
);
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseCors(x => x.WithOrigins(new string[] { "http://localhost:3000" })
    app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    .AllowAnyMethod());
    using (var scope = app.Services.CreateScope())
    {
        var pg = scope.ServiceProvider.GetRequiredService<PgContext>();
        await pg.Database.EnsureDeletedAsync();
        await pg.Database.EnsureCreatedAsync();
    }
}


app.UseHttpsRedirection();

app.UseAuthorization();
    app.MapHub<MeetingHub>("/meeting-hub");
    app.MapControllers();



app.Run();

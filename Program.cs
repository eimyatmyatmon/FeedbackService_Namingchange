using FeedbackService.Data;
using FeedbackService.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<Rating>("Ratings");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);
string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// Add services to the container.
builder.Services.AddDbContext<DataContext>(
options => options.UseNpgsql(connectionString)
//options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("v1", GetEdmModel()).Filter().Select().Expand());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

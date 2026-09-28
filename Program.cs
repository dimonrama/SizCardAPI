using Microsoft.EntityFrameworkCore;
using SizCardApi.Data;
using SizCardApi.Repositories;
using SizCardApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
     
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "СИЗ — Карточка учёта API",
        Version = "v1",
        Description = "CRUD API для электронной карточки учёта СИЗ (Пункт №2 методички ГО)."
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")
                       ?? "Data Source=sizcards.db"));


builder.Services.AddScoped<ISizCardRepository, SizCardRepository>();
builder.Services.AddScoped<ISizCardService, SizCardService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "СИЗ API v1");
    c.RoutePrefix = "swagger"; 
});


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();

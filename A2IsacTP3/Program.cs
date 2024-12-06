using A2IsacTP3.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => {
        c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Izap Avia��es",
            Description = "Esse � o Swagger da API, juntamente com a documenta��o de seus m�todos para a avalia��o A2 de T�picos 3",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = " Izap Avia��es",
                Email = "pereiraisac@unitins.br",
                Url = new Uri("https://www.youtube.com/watch?v=_KaA8W_Hov8")
            }
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        c.IncludeXmlComments(xmlPath);
    }
    );

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("Teste"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

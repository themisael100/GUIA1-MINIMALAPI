var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Lista en memoria para las categor�as
var categorias = new List<Categoria>
{
    new Categoria { Id = 1, Nombre = "Electr�nica" },
    new Categoria { Id = 2, Nombre = "Programaci�n" }
};

// Definir el endpoint para obtener todas las categor�as

app.MapGet("/categorias", () =>
{
// Devuelve todas las categor�as como respuesta
return Results.Ok(categorias);
});

app.Run();

internal class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}
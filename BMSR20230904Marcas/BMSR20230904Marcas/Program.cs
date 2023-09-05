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


var marcas = new List<Marcas>();

//Configuracion una ruta POST para agregar una nueva marca a la lista
app.MapPost("/marcas", (Marcas marca) =>
{
    marcas.Add(marca);//agerga el nueva marca a la lista
    return Results.Ok();//Devuelve una lista HTTP 200 OK
});



//Configurar una ruta PUT para actualizar una marca existente por su ID
app.MapPut("/marcas/{id}", (int id, Marcas marca) =>
{
    //Busca una marca en la lista que tenga el ID especificado
    var existingMarcas = marcas.FirstOrDefault(m => m.Id == id);
    if (existingMarcas != null)
    {
        //Busca una marca en la lista que tenga el ID especificado
        existingMarcas.Id = marca.Id;
        existingMarcas.Nombre = marca.Nombre;
        
        return Results.Ok();//Devuelve una respuesta HTTP 200 Ok
    }
    else
    {
        return Results.NotFound($"Marca con ID {id} no actualizada.");//Devulve una ruta HTTP 404 NOt Found si la marca no existe 
    }
});



app.MapGet("/marcas/{id}", (int id) =>
{
    // Busca una marca en la lista que tenga el ID especificado
    var marca = marcas.FirstOrDefault(m => m.Id == id);
    if (marca != null)
    {
        return Results.Ok(marca); // Devuelve la marca encontrada
    }
    else
    {
        return Results.NotFound($"Marca con ID {id} no encontrada."); // Devuelve un error 404 si no se encuentra la marca
    }
});

app.Run();
internal class Marcas
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}
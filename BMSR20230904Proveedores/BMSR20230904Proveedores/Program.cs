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

var proveedores = new List<Proveedores>();


app.MapGet("/proveedores", () =>
{
    return proveedores; //Devuelve la lista de proveedores
});


//Configuracion una ruta POST para agregar un nuevo proveedor a la lista
app.MapPost("/proveedores", (Proveedores proveedor) =>
{
    proveedores.Add(proveedor);//agerga el nuevo proveedor a la lista
    return Results.Ok();//Devuelve una respuesta HTTP 200 Ok
});

//Configura una ruta DELETE para eliminar un proveedor pos por ID
app.MapDelete("/proveedores/{id}", (int id) =>
{
    //Busca un proveedor en la lista que que tenga el ID especificado
    var existingProveedores = proveedores.FirstOrDefault(p => p.Id == id);
    if (existingProveedores != null)
    {
        //Elimina el proveedor de la lista
        proveedores.Remove(existingProveedores);
        return Results.Ok();//Devuelve una respuesta HTTP 200 Ok
    }
    else
    {
        return Results.NotFound();//Devulve una ruta HTTP 404 NOt Found si el producto no existe 
    }
});

app.Run();

internal class Proveedores
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
}
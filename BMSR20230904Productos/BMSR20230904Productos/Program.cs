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

var productos = new List<Productos>();

app.MapGet("/productos", () =>
{
    return productos; //Devuelve la lista de productos
});



//Configura una ruta GET para obtener un producto especifico por su ID
app.MapGet("/productos/{id}", (int id) =>
{
    //Busca un producto en la lista que tenga el ID especificado
    var producto = productos.FirstOrDefault(c => c.Id == id);
    return producto;//Devuelve el producto encotrado (o null si no se encuentra)
});



//Configuracion una ruta POST para agregar un nuevo producto a la lista
app.MapPost("/productos", (Productos producto) =>
{
    productos.Add(producto);//agerga el nuevo producto a la lista
    return Results.Ok();//Devuelve una lista http 200 OK
});



//Configurar una ruta PUT para actualizar un producto existente por su ID
app.MapPut("/productos/{id}", (int id, Productos producto) =>
{
    //Busca un producto en la lista que tenga el ID especificado
    var existingProducto = productos.FirstOrDefault(c => c.Id == id);
    if (existingProducto != null)
    {
        //Busca un producto en la lista que tenga el ID especificado
        existingProducto.Title = producto.Title;
        existingProducto.Description = producto.Description;
        existingProducto.Price = producto.Price;
        return Results.Ok();//Devuelve una respuesta HTTP 200 Ok
    }
    else
    {
        return Results.NotFound();//Devulve una ruta HTTP 404 NOt Found si el producto no existe 
    }
});


//Configura una ruta DELETE para eliminar un producto pos por ID
app.MapDelete("/productos/{id}", (int id) =>
{
    //BUsca un producto en la lista que que tenga el ID especificado
    var existingProducto = productos.FirstOrDefault(c => c.Id == id);
    if (existingProducto != null)
    {
        //Elimina el producto de la lista
        productos.Remove(existingProducto);
        return Results.Ok();//Devuelve una respuesta HTTP 200 Ok
    }
    else
    {
        return Results.NotFound();//Devulve una ruta HTTP 404 NOt Found si el producto no existe 
    }
});

app.Run();

internal class Productos
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}
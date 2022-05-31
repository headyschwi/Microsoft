using PizzaStore.Models;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "Host=localhost;Username=postgres;Password=root;Database=teste;Port=5432";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PizzaDb>(options => options.UseNpgsql(connectionString));
builder.Services.AddSwaggerGen(options=>{
    options.SwaggerDoc("v1", new OpenApiInfo{
        Title = "PizzaStore API",
        Description = "Making the pizzas you love",
        Version="v1.0"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

app.MapGet("/", () => "Hello World!");
app.MapPost("/pizzas", async (PizzaDb db, Pizza pizza) => {
    await db.Pizzas.AddAsync(pizza);
    await db.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});
app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync());
app.MapGet("/pizzas/{id}", async(PizzaDb db, int id) => await db.Pizzas.FindAsync(id));
app.MapPut("/pizzas/{id}", async (PizzaDb db, Pizza updatePizza, int id) => {
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza == null){return Results.NotFound();}
    pizza.Name = updatePizza.Name;
    pizza.Description = updatePizza.Description;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/pizzas/{id}", async (PizzaDb db, int id) => {
    var pizza = await db.Pizzas.FindAsync(id);
    if(pizza == null){
        return Results.NotFound();
    }
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.Run();

using Microsoft.OpenApi.Models;
using PizzaStore.DB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo(){Title = "PizzaStore API", Description = "Making the pizzas you love.", Version = "v1"});
});

var app = builder.Build();
if(app.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => "Hello World!");

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API v1");
});


app.MapGet("/pizzas/{id}", (int id) => PizzaDb.GetPizza(id));
app.MapGet("/pizzas", () => PizzaDb.GetPizzas());
app.MapPost("/pizzas", (Pizza pizza) => PizzaDb.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza update)=> PizzaDb.UpdatePizza(update));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDb.RemovePizza(id));

app.Run();

namespace PizzaStore.DB;

public record Pizza{
    public int Id { get; set; }
    public string ? Name { get; set; }
}

public class PizzaDb{
    private static List<Pizza> _pizzas = new List<Pizza>()
    {
        new Pizza{ Id=1, Name="Montemagno, Pizza shaped like a great mountain" },
        new Pizza{ Id=2, Name="The Galloway, Pizza shaped like a submarine, silent but deadly"},
        new Pizza{ Id=3, Name="The Noring, Pizza shaped like a Viking helmet, where's the mead"} 
    };

    public static IEnumerable<Pizza> GetPizzas(){
        return _pizzas;
    }

    public static Pizza? GetPizza(int id){
        return _pizzas.SingleOrDefault(p => p.Id == id);
    }

    public static Pizza CreatePizza(Pizza pizza){
        _pizzas.Add(pizza);
        return pizza;
    }

    public static Pizza? UpdatePizza(Pizza update){
        _pizzas = _pizzas.Select(p => {
            if(p.Id == update.Id){
                p.Name = update.Name;
            }
            return p;}).ToList();
            return update;
    }
    public static void RemovePizza(int id){
        _pizzas = _pizzas.FindAll(p => p.Id == id).ToList();
    }
}
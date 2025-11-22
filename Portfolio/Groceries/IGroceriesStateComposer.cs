namespace Portfolio.Groceries;

public interface IGroceriesStateComposer
{
    public GroceriesState ComposeState(List<GroceryItem> groceries);
}
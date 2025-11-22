namespace Portfolio.Groceries;

public class GroceryItem(Guid id, string name, bool isChecked, string mealSection, string storeSection)
{
    // Different casing for easy Cosmos interaction
    // TODO: Figure out better pattern
    public Guid id { get; set; } = id;

    public string Name { get; set; } = name;
    public bool IsChecked { get; set; } = isChecked;
    public string MealSection { get; set; } = mealSection;
    public string StoreSection { get; set; } = storeSection;

    public GroceryItemFrontendDto ToFrontendDto() => new(id, Name, IsChecked);
}

public class AddGroceryItemDto(Guid id, string name, string mealSection, string storeSection)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string MealSection { get; set; } = mealSection;
    public string StoreSection { get; set; } = storeSection;
}

public record DeleteGroceryItemDto(Guid Id);

public class GroceryItemFrontendDto(Guid id, string name, bool isChecked)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public bool IsChecked { get; } = isChecked;
}

public class GroceriesState
{
    public GroceryView MealView { get; } = new(ViewType.Meal);
    public GroceryView StoreView { get; } = new(ViewType.Store);
}

public class GroceryView(ViewType viewType)
{
    public List<Section> Sections { get; } = [];
    public ViewType ViewType { get; } = viewType;
}

public class Section(string name, List<GroceryItemFrontendDto> items)
{
    public string Name { get; } = name;
    public List<GroceryItemFrontendDto> Items { get; } = items;
}

public enum ViewType
{
    Meal,
    Store
}
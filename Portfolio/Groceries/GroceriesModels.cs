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
}

public class AddGroceryItemDto(string name, string mealSection, string storeSection)
{
    public string Name { get; set; } = name;
    public string MealSection { get; set; } = mealSection;
    public string StoreSection { get; set; } = storeSection;
}
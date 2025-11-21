namespace Portfolio.Groceries;

public class GroceryItem(Guid id, string name, bool isChecked, string mealSection, string storeSection)
{
    public Guid Id { get; set; } = id;
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
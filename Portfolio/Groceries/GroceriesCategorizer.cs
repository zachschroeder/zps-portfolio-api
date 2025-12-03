namespace Portfolio.Groceries;

public class GroceriesCategorizer : IGroceriesCategorizer
{
    public string GetStoreSection(string name)
    {
        foreach (var kvp in _sectionMap)
            if (name.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
                return kvp.Value;
        
        return Sections.Uncategorized;
    }
    
    private readonly Dictionary<string, string> _sectionMap = new()
    {
        // Prioritized because of spelling
        { "canned", Sections.Cans },
        { "chickpea", Sections.Cans },
        { "tomato paste", Sections.Cans },
        { "tortilla chip", Sections.Snacks },
        { "oz can", Sections.Cans },
        
        // Produce -- Fruits
        { "apple", Sections.Produce },
        { "avocado", Sections.Produce },
        { "banana", Sections.Produce },
        { "berri", Sections.Produce },
        { "berry", Sections.Produce },
        { "lemon", Sections.Produce },
        { "orange", Sections.Produce },
        { "pear", Sections.Produce },
        
        // Produce -- Vegetables
        { "broccoli", Sections.Produce },
        { "brussel", Sections.Produce },
        { "carrot", Sections.Produce },
        { "celery", Sections.Produce },
        { "corn", Sections.Produce },
        { "cucumber", Sections.Produce },
        { "garlic", Sections.Produce },
        { "green", Sections.Produce },
        { "mushroom", Sections.Produce },
        { "onion", Sections.Produce },
        { "peas", Sections.Produce },
        { "pepper", Sections.Produce },
        { "potato", Sections.Produce },
        { "salad", Sections.Produce },
        { "shallot", Sections.Produce },
        { "spinach", Sections.Produce },
        { "tomato", Sections.Produce },
        
        // Produce -- Other
        { "tofu", Sections.Produce },
        
        // Cans
        { "bean", Sections.Cans },
        { "can", Sections.Cans },
        { "sauce", Sections.Cans },
        { "soup", Sections.Cans },
        
        // Grains
        { "baguette", Sections.Grains },
        { "bread", Sections.Grains },
        { "cereal", Sections.Grains },
        { "granola", Sections.Grains },
        { "naan", Sections.Grains },
        { "pasta", Sections.Grains },
        { "rice", Sections.Grains },
        { "tortilla", Sections.Grains },
        { "quinoa", Sections.Grains },
        { "wrap", Sections.Grains },
        
        // Baking
        { "chocolate", Sections.Baking },
        { "flour", Sections.Baking },
        { "oil", Sections.Baking },
        { "salt", Sections.Baking },
        { "sugar", Sections.Baking },
        { "vanilla", Sections.Baking },
        { "vinegar", Sections.Baking },
        { "yeast", Sections.Baking },
        
        // Snacks
        { "chips", Sections.Snacks },
        { "crackers", Sections.Snacks },
        { "hummus", Sections.Snacks },
        { "nuts", Sections.Snacks },
        { "pistachio", Sections.Snacks },
        { "popcorn", Sections.Snacks },
        { "salsa", Sections.Snacks },
        { "sours", Sections.Snacks },
        
        // Deli
        { "bacon", Sections.Deli },
        { "beef", Sections.Deli },
        { "chicken", Sections.Deli },
        { "fish", Sections.Deli },
        { "meat", Sections.Deli },
        { "pork", Sections.Deli },
        { "salmon", Sections.Deli },
        { "sausage", Sections.Deli },
        { "steak", Sections.Deli },
        
        // Dairy
        { "butter", Sections.Dairy },
        { "burrata", Sections.Dairy },
        { "cheddar", Sections.Dairy },
        { "cheese", Sections.Dairy },
        { "cream", Sections.Dairy },
        { "egg", Sections.Dairy },
        { "milk", Sections.Dairy },
        { "parmesan", Sections.Dairy },
        { "ricotta", Sections.Dairy },
        { "yogurt", Sections.Dairy },
        
        // Frozen
        { "frozen", Sections.Frozen },
    };
}

public static class Sections
{
    public const string Baking = "Baking";
    public const string Cans = "Cans";
    public const string Dairy = "Dairy";
    public const string Deli = "Deli";
    public const string Frozen = "Frozen";
    public const string Grains = "Grains";
    public const string Produce = "Produce";
    public const string Snacks = "Snacks";
    public const string Uncategorized = "Uncategorized";
}
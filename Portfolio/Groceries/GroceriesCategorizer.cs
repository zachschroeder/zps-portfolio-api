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
        { "can", Sections.Cans },
        { "chickpea", Sections.Cans },
        { "dried tomato", Sections.Cans },
        { "juice", Sections.Beverages },
        { "salted butter", Sections.Dairy },
        { "tomato paste", Sections.Cans },
        { "tortilla chip", Sections.Snacks },
        
        // Produce -- Fruits
        { "apple", Sections.Produce },
        { "apricot", Sections.Produce },
        { "avocado", Sections.Produce },
        { "banana", Sections.Produce },
        { "berri", Sections.Produce },
        { "berry", Sections.Produce },
        { "lemon", Sections.Produce },
        { "lime", Sections.Produce },
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
        { "basil", Sections.Produce },
        { "cilantro", Sections.Produce },
        { "parsley", Sections.Produce },
        { "rosemary", Sections.Produce },
        { "thyme", Sections.Produce },
        { "tofu", Sections.Produce },
        
        // Cans
        { "bean", Sections.Cans },
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
        { "syrup", Sections.Baking },
        { "vanilla", Sections.Baking },
        { "vinegar", Sections.Baking },
        { "yeast", Sections.Baking },
        
        // Snacks
        { "chips", Sections.Snacks },
        { "crackers", Sections.Snacks },
        { "hummus", Sections.Snacks },
        { "nuts", Sections.Snacks },
        { "olives", Sections.Snacks },
        { "pistachio", Sections.Snacks },
        { "popcorn", Sections.Snacks },
        { "salsa", Sections.Snacks },
        { "sours", Sections.Snacks },
        
        // Beverages
        { "beer", Sections.Beverages },
        { "coke", Sections.Beverages },
        { "pop", Sections.Beverages },
        { "sauvignon", Sections.Beverages },
        { "soda", Sections.Beverages },
        { "sprite", Sections.Beverages },
        { "wine", Sections.Beverages },
        
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
    public const string Beverages = "Beverages";
    public const string Produce = "Produce";
    public const string Snacks = "Snacks";
    public const string Uncategorized = "Uncategorized";
}
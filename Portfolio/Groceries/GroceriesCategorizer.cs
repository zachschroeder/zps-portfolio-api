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
        // Produce
        { "apple", Sections.Produce },
        
        // Cans
        { "bean", Sections.Cans },
        
        // Grains
        { "bread", Sections.Grains },
        
        // Deli
        { "chicken", Sections.Deli },
        
        // Dairy
        { "milk", Sections.Dairy }
    };
}

public static class Sections
{
    public const string Produce = "Produce";
    public const string Cans = "Cans";
    public const string Grains = "Grains";
    public const string Deli = "Deli";
    public const string Dairy = "Dairy";
    public const string Uncategorized = "Uncategorized";
}
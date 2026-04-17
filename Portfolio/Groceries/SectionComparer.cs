namespace Portfolio.Groceries;

public class SectionComparer : IComparer<Section>
{
    private List<string> _sortOrder =
    [
        Sections.Produce,
        Sections.Deli,
        Sections.Cans,
        Sections.Grains,
        Sections.Baking,
        Sections.Snacks,
        Sections.Beverages,
        Sections.Dairy,
        Sections.Frozen,
        Sections.Uncategorized,
    ];

    public int Compare(Section? x, Section? y)
    {
        var xIndex = _sortOrder.FindIndex(s => s.Equals(x?.Name));
        if (xIndex == -1)
            xIndex = 99; // If not in _sortOrder, sort it last

        var yIndex = _sortOrder.FindIndex(s => s.Equals(y?.Name));
        if (yIndex == -1)
            yIndex = 99; // If not in _sortOrder, sort it last

        return xIndex - yIndex;
    }
}

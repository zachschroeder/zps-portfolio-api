namespace Portfolio.Groceries;

public class SectionComparer : IComparer<Section>
{
    private List<string> _sortOrder =
    [
        Sections.Produce,
        Sections.Frozen
    ];

    public int Compare(Section? x, Section? y)
    {
      var xIndex = _sortOrder.FindIndex(s => s.Equals(x?.Name));
      var yIndex = _sortOrder.FindIndex(s => s.Equals(y?.Name));
      return yIndex - xIndex;
    }
}

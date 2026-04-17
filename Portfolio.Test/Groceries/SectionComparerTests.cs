using Portfolio.Groceries;

namespace Portfolio.Test.Groceries;

public class SectionComparerTests
{
    [Fact]
    public void ShouldSortProduceBeforeFrozen()
    {
        // Arrange
        var comparer = new SectionComparer();
        var list = new List<Section>()
        {
            new Section(Sections.Frozen, []),
            new Section(Sections.Produce, []),
        };

        // Act
        list.Sort(comparer);

        // Assert
        Assert.Equal(Sections.Produce, list[0].Name);
        Assert.Equal(Sections.Frozen, list[1].Name);
    }

    [Fact]
    public void ShouldSortKnownBeforeUnknown()
    {
        // Arrange
        var comparer = new SectionComparer();
        var list = new List<Section>()
        {
            new Section(Sections.Frozen, []),
            new Section("Unknown Section", []),
            new Section(Sections.Uncategorized, []),
        };

        // Act
        list.Sort(comparer);

        // Assert
        Assert.Equal(Sections.Frozen, list[0].Name);
        Assert.Equal(Sections.Uncategorized, list[1].Name);
        Assert.Equal("Unknown Section", list[2].Name);
    }
}

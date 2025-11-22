namespace Portfolio.Groceries;

public class GroceriesStateComposer : IGroceriesStateComposer
{
    public GroceriesState ComposeState(List<GroceryItem> groceries)
    {
        var state = new GroceriesState();

        foreach (var groceryItem in groceries)
        {
            var dto = groceryItem.ToFrontendDto();
            AddItemToView(state.MealView, groceryItem.MealSection, dto);
            AddItemToView(state.StoreView, groceryItem.StoreSection, dto);
        }

        return state;
    }

    private static void AddItemToView(GroceryView view, string sectionName, GroceryItemFrontendDto dto)
    {
       var section = view.Sections.FirstOrDefault(s => s.Name == sectionName);
       if (section == null)
       {
           section = new Section(sectionName, []);
           view.Sections.Add(section);
       }
       section.Items.Add(dto);
    }
}
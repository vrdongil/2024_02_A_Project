

public class RecipeList
{
    public static CraftingRecipe[] KitchenRecipes = new CraftingRecipe[]
    {
        new CraftingRecipe
        {
            itemname = "야채스튜",
            resultItem = ItemType.VeagetableStew,
            resultAmount = 1,
            hungerResotreAmont = 40.0f,
            requiredItems = new ItemType[] {ItemType.Plant, ItemType,Bush},
            requiredAmounts = new int[] {2,1 }

        },
        new CraftingRecipe
        {
            itemname = "과일 샐러드",
            resultItem = ItemType.FruitSalad,
            resultAmount = 1,
            hungerResotreAmont = 60.0f,
            requiredItems = new ItemType[] {ItemType.Plant, ItemType,Bush},
            requiredAmounts = new int[] {3,3 }
        }
    };

    public static CraftingRecipe[] WorkbenchRecipes = new CraftingRecipe[] {
        {
            new CraftingRecipe
            {
                itemname = "수리 키트",
            resultItem = ItemType.RepairKit,
            resultAmount = 1,
            hungerResotreAmont = 25.0f,
            requiredItems = new ItemType[] {ItemType.Crystal},
            requiredAmounts = new int[] {3}
            },
        }   };

}  
    




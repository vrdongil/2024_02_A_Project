using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   public enum BuildingType
    {
        CraftingTable,
        Funace,
        Kitchen,
        Storage,

    }

    [System.Serializable]
    public class CraftingRecipe
    {
        public string itemname;
        public ItemType resultItem;
        public int resultAmount = 1;
        public ItemType[] requiredItems;
        public int requiredAmounts;
    }

using PirateJam.Inventory_System;
using PirateJam.Inventory_System.UI_Related;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateJam.Crafting_System.UI_Related
{
    public class CraftingWindow : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CraftingRecipeUI recipeUIPrefab;
        [SerializeField] private RectTransform recipeUIParent;
        [SerializeField] private List<CraftingRecipeUI> craftingRecipeUis;

        [Header("Public variables")]
        public ItemContainer ItemContainer;
        public List<CraftingRecipe> CraftingRecipes;

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;

        private void OnValidate()
        {
            Init();
        }

        private void Start()
        {
            Init();

            foreach (CraftingRecipeUI craftingRecipeUI in craftingRecipeUis)
            {
                craftingRecipeUI.OnPointerEnterEvent += OnPointerEnterEvent;
                craftingRecipeUI.OnPointerExitEvent += OnPointerExitEvent;
            }
        }

        private void Init()
        {
            recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUis);
            UpdateCraftingRecipes();
        }

        public void UpdateCraftingRecipes()
        {
            for (int i = 0; i < CraftingRecipes.Count; i++)
            {
                if (craftingRecipeUis.Count == i)
                {
                    craftingRecipeUis.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
                }
                else if (craftingRecipeUis[i] == null)
                {
                    craftingRecipeUis[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);
                }

                craftingRecipeUis[i].ItemContainer = ItemContainer;
                craftingRecipeUis[i].CraftingRecipe = CraftingRecipes[i];
            }

            for (int i = CraftingRecipes.Count; i < craftingRecipeUis.Count; i++)
            {
                craftingRecipeUis[i].CraftingRecipe = null;
            }
        }
    }
}

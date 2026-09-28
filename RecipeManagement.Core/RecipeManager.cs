using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace RecipeManagement.Core;

/// <summary>
/// Implement this class using the five Part A collections as private fields:
/// Dictionary&lt;int, Recipe&gt;, List&lt;string&gt;, LinkedList&lt;int&gt;,
/// Stack&lt;int&gt; and Queue&lt;string&gt;.
/// </summary>
public sealed class RecipeManager : IRecipeManager
{
    // TODO Part A: add your private collection fields here.
    private readonly Dictionary<int,Recipe> _recipes = new();
    private readonly List<string> _shoppingList= new();
    private readonly LinkedList<int> _cookingPlan = new();
    private readonly Stack<int> _removedRecipeHistory = new();
    private readonly Queue<string> _instructionQueue = new();
    public RecipeManager(IEnumerable<Recipe> recipes)
    {
        if (recipes == null) throw new ArgumentNullException(nameof(recipes));
        foreach(Recipe recipe in recipes)
        {
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));
            if (recipe.Id <= 0) throw new ArgumentException($"Your recipe id {recipe.Id} is lower or equal to 0.");
            if (string.IsNullOrWhiteSpace(recipe.Title)) throw new ArgumentException("Your recipe title is currently null");
            if (_recipes.ContainsKey(recipe.Id)) throw new ArgumentException($"Duplicate recipe ID found: {recipe.Id}");
            _recipes.Add(recipe.Id,recipe);
        }
    }

    public int RecipeCount => _recipes.Count;
    public int ShoppingItemCount => _shoppingList.Count;
    public int CookingPlanCount => _cookingPlan.Count;
    public int PendingInstructionCount => _instructionQueue.Count;
    public int RemovedRecipeCount => _removedRecipeHistory.Count;

    public bool AddRecipe(Recipe recipe)
    {
        if (recipe == null) throw new ArgumentNullException(nameof(recipe));
        if (recipe.Id <= 0) return false;
        if (string.IsNullOrWhiteSpace(recipe.Title)) return false;
        if(_recipes.ContainsKey(recipe.Id)) return false;
        _recipes.Add(recipe.Id, recipe);
        return true;
    }

    public Recipe? FindRecipe(int recipeId)
    {
        _recipes.TryGetValue(recipeId,out var value);
        return value; 
    }

    public bool RemoveRecipe(int recipeId)
    {
        return _recipes.Remove(recipeId);
    }

    public int AddIngredientsToShoppingList(int recipeId)
    {
        Recipe? recipe = FindRecipe(recipeId);
        if(recipe == null) return 0;
        _shoppingList.AddRange(recipe.Ingredients);
        return recipe.Ingredients.Count;   
    }

    public IReadOnlyList<string> GetShoppingList()
    {
        return _shoppingList.AsReadOnly(); // AsReadOnly() return read-only wrapper around a collection
    }
    public void ClearShoppingList()
    {
        _shoppingList.Clear();
    }
    public bool AddRecipeToCookingPlan(int recipeId)
    {
        if(FindRecipe(recipeId) == null) return false;
        if(_cookingPlan.Contains(recipeId)) return false;
        _cookingPlan.AddLast(recipeId);
        return true;
    }

    public bool RemoveRecipeFromCookingPlan(int recipeId)
    {
        if(!_cookingPlan.Remove(recipeId)) return false;
        _removedRecipeHistory.Push(recipeId);
        return true;
    }


    public bool RestoreLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement RestoreLastRemovedRecipe.");

    public int? PeekLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement PeekLastRemovedRecipe.");

    public IReadOnlyList<int> GetCookingPlan() =>
        throw new NotImplementedException("Part A: implement GetCookingPlan.");

    public bool StartCooking(int recipeId) =>
        throw new NotImplementedException("Part A: implement StartCooking.");

    public string? PeekNextInstruction() =>
        throw new NotImplementedException("Part A: implement PeekNextInstruction.");

    public string? CompleteNextInstruction() =>
        throw new NotImplementedException("Part A: implement CompleteNextInstruction.");

    public IReadOnlyList<Recipe> SearchByTitle(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByTitle.");

    public IReadOnlyList<Recipe> SearchByIngredient(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByIngredient.");

    public IReadOnlyList<Recipe> GetHighestProteinRecipes(int count) =>
        throw new NotImplementedException("Part B: implement GetHighestProteinRecipes.");

    public bool AddSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement AddSavedRecipe.");

    public bool RemoveSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement RemoveSavedRecipe.");

    public bool IsRecipeSaved(int recipeId) =>
        throw new NotImplementedException("Part B: implement IsRecipeSaved.");

    public IReadOnlyList<int> GetSavedRecipes() =>
        throw new NotImplementedException("Part B: implement GetSavedRecipes.");
}

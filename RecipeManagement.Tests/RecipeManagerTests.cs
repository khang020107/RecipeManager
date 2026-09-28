using System.Collections.Generic;
using RecipeManagement.Core;

namespace RecipeManagement.Tests;

/// <summary>
/// Example tests from the assignment specification. Add your own tests as you work.
/// </summary>
public sealed class RecipeManagerTests
{
    [Fact]
    public void Constructor_BuildsRecipeDictionary()
    {
        var manager = CreateManager();
        Assert.Equal(2, manager.RecipeCount);
        Assert.Equal("Recipe A", manager.FindRecipe(10)?.Title);
    }

    [Fact]
    public void InstructionsAreCompletedInFileOrder()
    {
        var manager = CreateManager();
        Assert.True(manager.StartCooking(10));
        Assert.Equal("First step", manager.PeekNextInstruction());
        Assert.Equal("First step", manager.CompleteNextInstruction());
        Assert.Equal("Second step", manager.PeekNextInstruction());
    }

    [Fact]
    public void RemovedRecipesAreRestoredLastInFirstOut()
    {
        var manager = CreateManager();
        manager.AddRecipeToCookingPlan(10);
        manager.AddRecipeToCookingPlan(20);
        manager.RemoveRecipeFromCookingPlan(10);
        manager.RemoveRecipeFromCookingPlan(20);
        Assert.Equal(20, manager.PeekLastRemovedRecipe());
        Assert.True(manager.RestoreLastRemovedRecipe());
        Assert.Equal(new[] { 20 }, manager.GetCookingPlan());
    }
    // Constuctor Tests
    [Fact]
    public void Constructor_NullRecipes_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new RecipeManager(null!));
    }

    [Fact]
    public void Constructor_RecipesIdLowerThan0_ThrowsArgumentException()
    {
        var recipe1 = new Recipe{Id = -1, Title = "Pancakes"};
        Assert.Throws<ArgumentException>(() => new RecipeManager([recipe1]));
    }

    [Fact]
    public void Constructor_EmptyRecipeTitle_ThrowsArgumentException()
    {
        var recipe1 = new Recipe{Id = 34, Title = " "};
        Assert.Throws<ArgumentException>(() => new RecipeManager([recipe1]));
    }

    [Fact]
    public void Constructor_DuplicateId_ThrowsArgumentException()
    {
        var recipe1 = new Recipe{Id = 34, Title = "Pancakes"};
        var recipe2 = new Recipe{Id = 34, Title = "Job"};

        Assert.Throws<ArgumentException>(() => new RecipeManager([recipe1,recipe2]));
    }

    // FindRecipe() tests
    [Fact]
    public void FindRecipe_ExistingId_ReturnsRecipe()
    {
        var manager = CreateManager();
        var recipe = manager.FindRecipe(10);
        Assert.NotNull(recipe);
        Assert.Equal("Recipe A", recipe.Title);
    }

    [Fact]
    public void FindRecipe_MissingId_ReturnsNull()
    {
        var manager = CreateManager();
        Assert.Null(manager.FindRecipe(999));
    }
    
    // AddRecipe() tests
    [Fact]
    public void AddRecipe_ValidRecipe_ReturnsTrueAndIncreasesCount()
    {
        var recipe1 = new Recipe{Id = 34, Title = "Pancakes"};
        var manager = CreateManager();
        Assert.True(manager.AddRecipe(recipe1));
        Assert.Equal(3,manager.RecipeCount);
        Assert.Same(recipe1,manager.FindRecipe(34));
    }

    [Fact]
    public void AddRecipe_DuplicateId_ReturnsFalseAndCountUnchanged()
    {
        var recipe1 = new Recipe{Id = 10, Title = "Pancakes"};
        var manager = CreateManager();
        Assert.False(manager.AddRecipe(recipe1));
        Assert.Equal(2,manager.RecipeCount);
        var correct = manager.FindRecipe(10);
        Assert.Equal("Recipe A",correct?.Title);
    }

    [Fact]
    public void AddRecipe_NonPositiveId_ReturnsFalse()
    {
        var recipe1 = new Recipe{Id = -10, Title = "Pancakes"};
        var manager = CreateManager();
        Assert.False(manager.AddRecipe(recipe1));
        Assert.Equal(2,manager.RecipeCount);
    }

    [Fact]
    public void AddRecipe_EmptyTitle_ReturnsFalse()
    {
        var recipe1 = new Recipe{Id = 99, Title = ""};
        var manager = CreateManager();
        Assert.False(manager.AddRecipe(recipe1));
        Assert.Equal(2,manager.RecipeCount);
    }

    [Fact]
    public void AddRecipe_NullRecipe_ThrowsArgumentNullException()
    {
        var manager = CreateManager();
        Assert.Throws<ArgumentNullException>(() => manager.AddRecipe(null!));
    }

    // RemoveRecipe tests
    [Fact]
    public void RemoveRecipe_ExistingId_ReturnsTrueAndDecreasesCount()
    {
        var manager = CreateManager();
        Assert.True(manager.RemoveRecipe(10));
        Assert.Equal(1,manager.RecipeCount);
        Assert.Null(manager.FindRecipe(10));
    }

    [Fact]
    public void RemoveRecipe_MissingId_ReturnsFalseAndCountUnchanged()
    {
        var manager = CreateManager();
        Assert.False(manager.RemoveRecipe(100));
        Assert.Equal(2,manager.RecipeCount);
    }
    private static RecipeManager CreateManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe
            {
                Id = 10,
                Title = "Recipe A",
                Ingredients = new() { "1 apple" },
                Instructions = new() { "First step", "Second step" }
            },
            new Recipe
            {
                Id = 20,
                Title = "Recipe B"
            }
        });
    }
}

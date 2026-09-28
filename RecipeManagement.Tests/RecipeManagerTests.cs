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

    [Fact]
    public void Constructor_NullRecipes_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new RecipeManager(null!));
    }

    [Fact]
    public void Constructor_RecipesIdLowerThan0_ThrowsArgumentException()
    {
        var Recipe1 = new Recipe{Id = -1, Title = "Pancakes"};
        Assert.Throws<ArgumentException>(() => new RecipeManager([Recipe1]));
    }

    [Fact]
    public void Constructor_EmptyRecipeTitle_ThrowsArgumentException()
    {
        var Recipe1 = new Recipe{Id = 34, Title = " "};
        Assert.Throws<ArgumentException>(() => new RecipeManager([Recipe1]));
    }

    [Fact]
    public void Constructor_DuplicateId_ThrowsArgumentException()
    {
        var Recipe1 = new Recipe{Id = 34, Title = "Pancakes"};
        var Recipe2 = new Recipe{Id = 34, Title = "Job"};

        Assert.Throws<ArgumentException>(() => new RecipeManager([Recipe1,Recipe2]));
    }

    [Fact]
    public void FindRecipe_ExistingId_ReturnsRecipe()
    {
        var manager = CreateManager();
        Assert.NotNull(manager.FindRecipe(10));
    }

    [Fact]
    public void FindRecipe_MissingId_ReturnsNull()
    {
        var manager = CreateManager();
        Assert.Null(manager.FindRecipe(999));
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

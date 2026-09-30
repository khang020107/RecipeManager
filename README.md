# Recipe Management System — Student Starter

Starter repository for Parts A and B. Implement `RecipeManager` in Core; the Application menu and JSON loader are supplied.

## What is supplied

- `RecipeManagement.Core/Models/` — recipe model classes
- `RecipeManagement.Core/RecipeLoader.cs` — reads `data/recipes.json`
- `RecipeManagement.Core/IRecipeManager.cs` — public API
- `RecipeManagement.Application/` — console menu (options labelled PartA / PartB)
- `RecipeManagement.Tests/` — example tests
- `data/recipes.json` — recipe dataset

## What you implement

**Part A** — `RecipeManager.cs` using:

- `Dictionary<int, Recipe>`
- `List<string>`
- `LinkedList<int>`
- `Stack<int>`
- `Queue<string>`

### Part A design notes

`RestoreLastRemovedRecipe` pops the ID from the removed-recipe stack
before checking whether the recipe still exists and is not already
planned, matching the order described in the spec ("pop the most
recently removed ID and append it... if..."). If restoration fails,
the ID is not returned to the stack.

**Part B** — LINQ searches, protein report, saved-recipe collection, `Design.md`, and more tests.

## Build and run

Open `StudentPackage/RecipeManagement.sln`:

```bash
dotnet build
dotnet test
dotnet run --project RecipeManagement.Application -- data/recipes.json
```

Until you implement `RecipeManager`, menu options print a **Not implemented** message.

## AI acknowledgement

- I used Claude to help me understand Dictionary key lookup, encapsulation with IReadOnlyList, xUnit Assert.Throws, logic thinking and step by step guide to write tests that cover all conditions of the function. I did not copy or adapt AI-generated code or other material into my submission. I developed the submitted solution myself based on my understanding on the course material.

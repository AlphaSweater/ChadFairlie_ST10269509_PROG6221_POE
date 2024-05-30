# ChadFairlie_ST10269509_PROG6221_POE

## Project Description

This project is a command-line application developed in C# and Visual Studio. It's designed to help users create, store, and manage recipes. The application allows users to enter and store the ingredients and steps for a recipe. It also provides functionality to scale the recipe by a factor of 0.5 (half), 2 (double), or 3 (triple), reset the recipe scale to the original values, and clear all the data to enter a new recipe. The data is stored in memory while the software is running and does not persist between runs.

### Classes

**•	Program.cs:** This is the main class that runs the application. It handles user input, displays the user interface, and manages the flow of the application.

**•	Recipe.cs:** This class represents a cooking recipe. It includes properties for the recipe name, current scale, list of ingredients, and list of cooking steps. It also provides methods to add ingredients and steps, calculate total calories, scale the recipe, reset scaling, and print the recipe.

**•	Ingredient.cs:** This class represents a single ingredient used in a recipe. It includes properties for the name, quantity, original quantity, unit of measurement, calories per unit, and food group of the ingredient. It also provides a method to convert the quantity and unit of measurement if they are convertible.

**•	UnitConverter.cs:** This class provides functionality to convert between different units of measurement. It includes methods to check if a unit of measurement is convertible and to convert the quantity, unit of measurement, and calories per unit.

## How to Compile and Run the Software

If you have no prior experience with coding, don't worry! Just follow these step-by-step instructions:

1. First, you need to install Visual Studio. You can download it from [here](https://visualstudio.microsoft.com/downloads/). Choose the Community version, which is free for individual developers, open source projects, academic research, education, and small professional teams.

2. Once you have Visual Studio installed, go to the GitHub repository page for this project. You can find it [here](https://github.com/AlphaSweater/ChadFairlie_ST10269509_PROG6221_POE).

3. On the GitHub repository page, click the green "Code" button and then click "Download ZIP".

4. Once the ZIP file is downloaded, extract it to a location of your choice on your computer.

5. Open Visual Studio and click on "Open a project or solution".

6. Navigate to the location where you extracted the ZIP file, go into the "ChadFairlie_ST10269509_PROG6221_POE-master" folder, and select the .sln file. Click "Open".

7. Once the project is open in Visual Studio, you can compile it by clicking on "Build" in the menu at the top, and then "Build Solution".

8. After the project has been compiled, you can run it by clicking on "Start" in the menu at the top, and then it should start up.

And that's it! You should now see the command-line interface of the application in a new window, ready for you to start entering and managing your recipes.

## Updates In Part 2

**•	Multiple Recipe Storage:** The program can now store multiple recipes at once in memory, allowing users to create and manage a collection of recipes.

**•	Recipe Selection:** Users can now view a list of all stored recipes and select a specific recipe to view and scale.

**•	Enhanced Recipe Creation:** The process of creating a recipe has been improved with informative prompts to guide the user in entering values. Users now need to enter the calories per unit of measurement for each ingredient and specify the food group to which each ingredient belongs.

**•	Calorie Information:** The total calories of a recipe are now calculated and displayed when viewing the recipe, providing users with important nutritional information. An alert is also displayed while adding ingredients if the total calorie count exceeds 300.

**•	Ingredient Sorting:** The list of ingredients in a recipe is now sorted alphabetically for easier reading and management.

**•	Unit Tests:** Added unit tests for the CalculateTotalCalories method to ensure its accuracy and reliability.

**•	Input Validation:** Fixed a bug where entering 0 for the number of ingredients or steps, or a blank string for names, would cause the program to not display anything. The program now has proper validation for these cases and will prompt the user to enter valid values.

### Lecturers Feedback
Based on the feedback received from the lecturer, changes have been made to the program to improve input validation. Previously, if a user entered a 0 for the number of ingredients or steps in a recipe, the program would simply skip to the next section, allowing a recipe to be saved with no ingredients or steps. This has been addressed by implementing validation checks to ensure that the number of ingredients and steps cannot be 0.

Additionally, the program previously allowed recipes to be saved with blank names if the user entered a blank string. This has been rectified by adding a validation check to ensure that all entered strings are not blank. These changes enhance the user experience by preventing the creation of incomplete or improperly named recipes.

## References

This project was made possible with help and resources from:

- **ChatGPT**: Provided assistance in creating the `UnitConverter` class.
- **Bro Code**: Provided a helpful [video tutorial](https://youtu.be/vQzREQUhGSA?si=zi-m4qyNKLMErAu9) on Lists in C#.

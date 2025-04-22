# 3902
Legendary project from the Bombardier beetles!

Click [here](https://github.com/MonoGame/MonoGame.Samples) for good resource for code examples

## Create empty MonoGame project
```dotnet new mgdesktopgl```

To run, open with VSCode and press play button using C# extension, or cd into 3902 and run `dotnet run`

# Sprint 4
## Controls
WASD / arrow keys for movement. Z to attack. 1, 2, 3 draw items (use Z to use). Walk up to doors to use them.

## Known bugs
Monoko kinda stutters when she moves diagonally. This is a property of keeping everything on pixels and upscaling the render target. Not really a bug but something to note.
Items are rendered off center from her body. Will fix later.
Items don't have fully fleshed out behavior yet (not necessary for this sprint probably)
Room transitions show 2 monokos while transitioning (not really a bug, just a visual artifact)

## .NET Code Analyzer
"Use tools to improve your code and/or create documentation about your codebase - do at least one of the following:"
We picked:
"Use the .NET code analyzers (Roslyn) with rules for code quality analysis. Document any errors or warnings that you get, then on-by-one fix them or set them as supressed with an explanation in your documentation on why you supressed that particular warning/error."

Refer to *code_analyzer.png*. No errors or warning were found in the workspace (we are goated).


# Sprint 5

Write up a document with useful information on your project. This might include: program controls, descriptions of known bugs that program has, and details of any tools or processes your team used that aren't explicitly required (for example, calculating and using Code Metrics as part of your design process) 

Player 1 is controlled using the WASD keys for movement and Z for attack/use item. Player 2 is controlled using the arrow keys and N for attack/use item. For player 1, items can be used by pressing 1, 2, or 3. For player 2, the same behavior can be done using 8, 9, and 0. Doors function automatically on collision.

There are a few bugs in the game currently, however most of these bugs is simply a lack of function of components rather than something that may reduce the existing game experience. There is still currently a small bug that may be resolved by submission time regarding the orign of rotation for the health bar, resulting in the internal and external rectangles gradually becoming de-synchronized in their rotation.

Throughout this sprint we maintained greater communication than some prior sprints and we used a Google Doc at one point to keep track of bugs, tasks, and features that needed to be completed. This was a massive upgrade from the previous Discord thread system that we used for task completion.
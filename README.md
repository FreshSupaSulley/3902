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

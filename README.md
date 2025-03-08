# 3902
Legendary project from the Bombardier beetles!

Click [here](https://github.com/MonoGame/MonoGame.Samples) for good resource for code examples

## Create empty MonoGame project
```dotnet new mgdesktopgl```

To run, open with VSCode and press play button using C# extension, or cd into 3902 and run `dotnet run`

# Sprint 3
## Controls
WASD / arrow keys for movement. 1, 2, 3 draw items (use Z or N to use them). You don't need to use the mouse controller to transition between rooms; simply walk up to open doors.

## Known bugs
Monoko kinda stutters when she moves diagonally. This is a property of keeping everything on pixels and upscaling the render target. Not really a bug but something to note.
Items are rendered off center from her body. Will fix later.
Items don't have fully fleshed out behavior yet (not necessary for this sprint probably)
Room transitions show 2 monokos while transitioning (not really a bug, just a visual artifact)

## 
"Use tools to improve your code and/or create documentation about your codebase - do at least one of the following:"
We picked:
"Use the .NET code analyzers (Roslyn) with rules for code quality analysis. Document any errors or warnings that you get, then on-by-one fix them or set them as supressed with an explanation in your documentation on why you supressed that particular warning/error."

We fix errors on the fly that appear in the problems menu, so there's nothing left to document. But in fear of losing points I guess I'll introduce a warning:

[{
	"resource": "../Game/Entities/Player.cs",
	"owner": "csharp-build",
	"code": "CS0414",
	"severity": 4,
	"message": "The field 'Player.moving' is assigned but its value is never used [/Users/eboschert/Downloads/3902/demo.csproj]",
	"startLineNumber": 28,
	"startColumn": 16,
	"endLineNumber": 28,
	"endColumn": 16
}]

We had this error that we are suppressing because it may become useful in the future for external classes to access. Simple as that.

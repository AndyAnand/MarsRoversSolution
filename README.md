# Mars Rovers Solution

Mars Rovers Solution is a software developed using C# and .NET 5 that allows NASA to send remote commands for its rovers on Mars, enabling the exploration of the Red Planet in search for life.

## Prerequisites
- Have the [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) installed.

## Running the project

### Restore the packages

In the solution's root folder:
```
dotnet restore
```
### Building and Running the project
From a terminal, execute the following commands from the solution's root folder:
```
cd .\src\MarsRoversSolution.ConsoleApp\
dotnet build
dotnet run
```

### Testing the project
From a terminal, execute the following commands from the solution's root folder:
```
cd .\tests\MarsRoversSolution.Tests\
dotnet test
```
## Changing the input file
For submitting new input cases, navigate to the [Input.txt](src/MarsRoversSolution.ConsoleApp/Resources/Input.txt) and alter its content.
Example:

```
3 3
2 3 S
MMRMML
```
Will send to a rover exploring a Mars terrain 3 units long to East and 3 units long to North and initially located at 2 units (East), 3 units (North), facing South (S) the commands: Move(M), Move, Rotate Right (R), Move, Move and Rotate Left (L). After running the program, it will output:
```
0 1 S
```
Stating that the rover is now at position (0 (East), 1 (North)) facing South.

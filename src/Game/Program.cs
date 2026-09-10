var game = new GameRuntime();

var input = new GameInput();

var frameTime = 0.1f;

var simulationDuration = 10f;

var steps = simulationDuration / frameTime;

game.Initialize();

for (var i = 0; i < steps; i++)
{
    game.Update(frameTime, input);
}

using R = Raylib_cs.Raylib;
namespace tilebash;

public class Game
{
    private static int _level = 1;
    private static List<IObject> objects = [];
    public static InputMap controls = new InputMap();

    public static void InitializeGame()
    {
        R.InitWindow(640, 480, "Hello, world");
        R.SetTargetFPS(60);

        InitializeLevel(_level);

        while (!R.WindowShouldClose())
        {
            HandleInput();
            GameLoop();
        }

        R.CloseWindow();
    }

    private static void InitializeLevel(int level)
    {
        objects.Clear();
        objects.Add(new Paddle());
    }

    private static void HandleInput()
    {
        if (R.IsKeyDown(Raylib_cs.KeyboardKey.Left))
        {
            controls.movePaddleLeft = true;
        }
        else
        {
            controls.movePaddleLeft = false;
        }

        if (R.IsKeyDown(Raylib_cs.KeyboardKey.Right))
        {
            controls.movePaddleRight = true;
        }
        else
        {
            controls.movePaddleRight = false;
        }

        foreach (IObject obj in objects) {
            if (obj is IControllable ctrl) {
                ctrl.HandleInput(controls);
            }
        }
    }

    private static void GameLoop()
    {
        R.BeginDrawing();
        R.ClearBackground(Raylib_cs.Color.Black);
        R.DrawText($"{R.GetTime():f1}", 0, 0, 32, Raylib_cs.Color.White);

        foreach (IObject obj in objects) {
            if (obj is IDrawable draw) {
                draw.Draw();
            }
        }

        R.EndDrawing();
    }

    private Game() { }
}

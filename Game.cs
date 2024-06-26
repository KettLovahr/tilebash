using R = Raylib_cs.Raylib;
namespace tilebash;

public class Game
{
    private static int _level = 1;
    private static List<IObject> _objects = [];
    public static InputMap controls = new InputMap();

    public static void InitializeGame()
    {
        R.InitWindow(640, 480, "TILEBASH");
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
        _objects.Clear();
        Paddle paddle = new Paddle();
        _objects.Add(paddle);
        _objects.Add(new Ball(paddle));

        _objects.Add(new Tile(50, 50, 100, 50, Raylib_cs.Color.Lime));
        _objects.Add(new Tile(100, 50, 100, 50, Raylib_cs.Color.Lime));
        _objects.Add(new Tile(150, 50, 100, 50, Raylib_cs.Color.Lime));
        _objects.Add(new Tile(200, 50, 100, 50, Raylib_cs.Color.Lime));
        _objects.Add(new Tile(250, 50, 100, 50, Raylib_cs.Color.Lime));
        _objects.Add(new Tile(300, 50, 100, 50, Raylib_cs.Color.Lime));
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

        if (R.IsKeyDown(Raylib_cs.KeyboardKey.Space))
        {
            controls.launchBall = true;
        }
        else
        {
            controls.launchBall = false;
        }

        foreach (IObject obj in _objects)
        {
            if (obj is IControllable ctrl)
            {
                ctrl.HandleInput(controls);
            }
        }
    }

    private static void GameLoop()
    {
        R.BeginDrawing();
        R.ClearBackground(Raylib_cs.Color.Black);

        foreach (IObject obj in _objects)
        {
            obj.Update(_objects);
            if (obj is IDrawable draw)
            {
                draw.Draw();
            }
        }

        R.EndDrawing();
    }

    private Game() { }
}

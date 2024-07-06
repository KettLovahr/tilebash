using R = Raylib_cs.Raylib;
namespace tilebash;

public class Game
{
    public static int Score { private set; get; } = 0;
    public static int Level { private set; get; } = 1;
    public static int TilesLeft { private set; get; } = 0;
    private static List<IObject> _objects = [];
    public static InputMap controls = new InputMap();

    public static void InitializeGame()
    {
        R.InitWindow(640, 480, "TILEBASH");
        R.SetTargetFPS(60);

        InitializeLevel(Level);

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

        for (int i = 1; i <= 3 + level; i++)
        {
            for (int j = 1; j <= 11; j++)
            {
                _objects.Add(new Tile(50 * j, 25 * i, 40, 20, Raylib_cs.Color.Lime));
                TilesLeft++;
            }
        }
    }

    private static void HandleInput()
    {
        RegisterInputKey(ref controls.movePaddleLeft, Raylib_cs.KeyboardKey.Left);
        RegisterInputKey(ref controls.movePaddleRight, Raylib_cs.KeyboardKey.Right);
        RegisterInputKey(ref controls.launchBall, Raylib_cs.KeyboardKey.Space);

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

        if (TilesLeft == 0) {
            Level++;
            InitializeLevel(Level);
        }

        R.DrawText($"{Score}", 20, 20, 32, Raylib_cs.Color.White);
        R.DrawText($"Tiles Left: {TilesLeft}", 20, 60, 16, Raylib_cs.Color.White);
        R.DrawText($"Level: {Level}", 20, 80, 16, Raylib_cs.Color.White);

        R.EndDrawing();
    }

    public static void AddScore(int amount, bool tile)
    {
        Score += amount * Level;
        if (tile) {
            TilesLeft--;
        }
    }

    public static void RegisterInputKey(ref bool field, Raylib_cs.KeyboardKey key) {
        if (R.IsKeyDown(key))
        {
            field = true;
        }
        else
        {
            field = false;
        }
    }

    private Game() { }
}

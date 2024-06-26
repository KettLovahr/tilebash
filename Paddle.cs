namespace tilebash;

public class Paddle : IDrawable, IObject, IControllable
{
    private float _x = 320;
    private float _width = 96;

    public float X
    {
        set
        {
            _x = Math.Clamp(value, 0, 640 - Width);
        }
        get
        {
            return _x;
        }
    }

    public float Width
    {
        set
        {
            _width = value;
        }
        get
        {
            return _width;
        }
    }

    public void Draw()
    {
        Raylib_cs.Raylib.DrawRectangle((int)X, 450, (int)Width, 10, Raylib_cs.Color.Red);
    }

    public void HandleInput(InputMap input)
    {
        if (input.movePaddleLeft)
        {
            X -= 6;
        }
        if (input.movePaddleRight)
        {
            X += 6;
        }
    }

    public void Update(List<IObject> objects)
    {
    }
}

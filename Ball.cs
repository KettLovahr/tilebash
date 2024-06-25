namespace tilebash;

public class Ball : IDrawable
{
    public float x { private set; get; }
    public float y { private set; get; }

    public void Draw() {
        Raylib_cs.Raylib.DrawCircle((int)x, (int)y, 10, Raylib_cs.Color.Gray);
    }
}

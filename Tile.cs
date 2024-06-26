namespace tilebash;

public class Tile : IDrawable, IObject
{
    public float X {private set; get;}
    public float Y {private set; get;}
    public float Width {private set; get;}
    public float Height {private set; get;}
    public Raylib_cs.Color Color {private set; get;}

    public bool Alive {private set; get;} = true;

    public void Draw()
    {
        if (Alive){
            Raylib_cs.Raylib.DrawRectangle((int)X, (int)Y, (int)Width, (int)Height, Color);
        }
    }

    public void Update(List<IObject> objects)
    {
    }

    public void Destroy() {
        Alive = false;
    }

    public Tile(float x, float y, float width, float height, Raylib_cs.Color color) {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
        this.Color = color;
    }
}

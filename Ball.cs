namespace tilebash;

public class Ball : IDrawable, IObject
{
    public float x { private set; get; } = 220;
    public float y { private set; get; } = 240;

    private Paddle _paddle;

    private float _dirX = 1;
    private float _dirY = 1;
    private float _radius = 8;
    private float _speed = 4;

    public void Draw()
    {
        Raylib_cs.Raylib.DrawCircle((int)x, (int)y, _radius, Raylib_cs.Color.Gray);
    }

    public void Update()
    {
        x += _dirX * _speed;
        y += _dirY * _speed;

        if (x > 640 - _radius && _dirX > 0) { _dirX = -1; }
        if (x < _radius && _dirX < 0) { _dirX = 1; }

        if (y < _radius && _dirY < 0) { _dirY = 1; }

        if (x > _paddle.X && x < _paddle.X + _paddle.Width)
        {
            if (y > 450 - _radius && _dirY > 0)
            {
                _dirY = -1;
                _speed += 1;
            }
        }
    }

    public Ball(Paddle paddle)
    {
        this._paddle = paddle;
    }
}

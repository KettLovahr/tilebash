namespace tilebash;

public class Ball : IDrawable, IObject, IControllable
{
    public float x { private set; get; } = 320;
    public float y { private set; get; } = 240;

    private Paddle _paddle;

    private bool _launched = false;
    private float _dirX = 0;
    private float _dirY = 1;
    private float _radius = 8;
    private float _speed = 4;

    public void Draw()
    {
        Raylib_cs.Raylib.DrawCircle((int)x, (int)y, _radius, Raylib_cs.Color.Gray);
    }

    public void Update()
    {
        if (_launched)
        {
            x += _dirX * _speed;
            y += _dirY * _speed;

            if (x > 640 - _radius && _dirX > 0) { _dirX *= -1; }
            if (x < _radius && _dirX < 0) { _dirX *= -1; }

            if (y < _radius && _dirY < 0) { _dirY *= -1; }

            if (x > _paddle.X && x < _paddle.X + _paddle.Width)
            {
                if (y > 450 - _radius && _dirY > 0)
                {
                    float xOffset = (x - _paddle.X) / _paddle.Width;
                    float dir = (float)(-Math.PI + xOffset * Math.PI);
                    _dirX = (float)Math.Cos(dir);
                    _dirY = (float)Math.Sin(dir);
                    if (xOffset < 0.1 || xOffset > 0.9) {
                        _speed += 0.4f;
                    } else {
                        _speed += 0.2f;
                    }
                }
            }
        }
        else
        {
            x = _paddle.X + _paddle.Width / 2;
            y = 420;
        }
    }

    public void HandleInput(InputMap input)
    {
        if (input.launchBall && !_launched) {
            _launched = true;
        }
    }

    public Ball(Paddle paddle)
    {
        this._paddle = paddle;
    }
}

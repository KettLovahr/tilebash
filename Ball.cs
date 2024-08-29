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
    private float _speed = 8;

    public void Draw()
    {
        Raylib_cs.Raylib.DrawCircle((int)x, (int)y, _radius, Raylib_cs.Color.Gray);
    }

    public void Update(List<IObject> objects)
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
                if (y > 450 - _radius && y < 480 && _dirY > 0)
                {
                    float xOffset = (x - _paddle.X) / _paddle.Width;
                    float dir = Lerp((float)(-Math.PI * 0.85), (float)(-Math.PI * 0.15), xOffset);
                    _dirX = (float)Math.Cos(dir);
                    _dirY = (float)Math.Sin(dir);
                    _speed += (xOffset < 0.1 || xOffset > 0.9) ? 0.2f : 0.1f;
                    Game.ResetStreak();
                }
            }

            foreach (IObject obj in objects)
            {
                if (obj is Tile tile)
                {
                    if (!tile.Alive) { continue; }
                    if (x + _radius <= tile.X || x - _radius >= tile.X + tile.Width) { continue; }
                    if (y + _radius <= tile.Y || y - _radius >= tile.Y + tile.Height) { continue; }
                    tile.Destroy();
                    if (x + _radius > tile.X + tile.Width && _dirX < 0) { _dirX *= -1; }
                    if (x - _radius < tile.X && _dirX > 0) { _dirX *= -1; }
                    if (y + _radius > tile.Y + tile.Height && _dirY < 0) { _dirY *= -1; }
                    if (y - _radius < tile.Y && _dirY > 0) { _dirY *= -1; }
                }
            }

            if (y > 500)
            {
                _launched = false;
            }
        }
        else
        {
            x = _paddle.X + _paddle.Width / 2;
            y = 440;
        }
    }

    public void HandleInput(InputMap input)
    {
        if (input.launchBall && !_launched)
        {
            _launched = true;
            _speed /= 2;
            if (_speed < 8)
            {
                _speed = 8;
            }
        }
    }

    public Ball(Paddle paddle)
    {
        this._paddle = paddle;
    }

    private float Lerp(float from, float to, float weight)
    {
        return from * (1 - weight) + to * weight;
    }
}

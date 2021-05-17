public struct NetPosition
{
    int _x, _y;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }

    public NetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }
}

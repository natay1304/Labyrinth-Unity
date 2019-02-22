using System.Drawing;
using UnityEngine;

public class PathMap
{
    private int _pathLength = 0;
    private bool[,] _pathMap = new bool[0,0];
    private Point _pathEnter = new Point(0,0);
    private Point _pathExit = new Point(0,0);

    public PathMap(int width, int height)
    {
        _pathMap = new bool[GetOdd(height), GetOdd(width)];
    }

    public int PathLength { get => _pathLength; set => _pathLength = value; }
    public bool[,] PathMapArray { get => _pathMap; set => _pathMap = value; }
    public Point PathEnter { get => _pathEnter; set => _pathEnter = value; }
    public Point PathExit { get => _pathExit; set => _pathExit = value; }

    private int GetOdd(int value)
    {
        if (value % 2 == 0)
        {
            value++;
        }
        return value;
    }
}

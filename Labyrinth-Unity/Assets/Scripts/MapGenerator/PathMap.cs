using System.Drawing;
using UnityEngine;

public class PathMap
{
    private Point[,] _keyPoints = new Point[0, 0];
    private int _pathLength = 0;
    private bool[,] _pathMap = new bool[0,0];
    private Point _pathEnter = new Point(0,0);
    private Point _pathExit = new Point(0,0);

    public PathMap(int width, int height, int pathLength)
    {
        _pathMap = new bool[GetOdd(height), GetOdd(width)];
        _keyPoints = new Point[(_pathMap.GetLength(0) - 1) / 2, (_pathMap.GetLength(1) - 1) / 2];
        PathLength = pathLength;
    }

    public int PathLength
    {
        get => _pathLength;
        set
        {
            while(value % 2 == 0 || value <= 2)
            {
                value += 1;
            }
            _pathLength = value;
        }
    }
    public bool[,] PathMapArray { get => _pathMap; set => _pathMap = value; }
    public Point PathEnter { get => _pathEnter; set => _pathEnter = value; }
    public Point PathExit { get => _pathExit; set => _pathExit = value; }
    public Point[,] KeyPoints { get => _keyPoints; set => _keyPoints = value; }

    private int GetOdd(int value)
    {
        if (value % 2 == 0)
        {
            value++;
        }
        return value;
    }
}

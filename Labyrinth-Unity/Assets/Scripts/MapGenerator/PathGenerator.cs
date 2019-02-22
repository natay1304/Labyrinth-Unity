using System.Drawing;
using UnityEngine;

public class PathGenerator
{
    private Point _pathEnter = new Point(0, 0); 
    private Point _pathExit = new Point(0,0); 
    private PathMap _pathMap;
    private int _pathLength = 0;
    private Point[,] _subPassPoints;
    public PathMap GeneratePathMap(int height, int width)
    {
        _pathMap = new PathMap(width, height);
        _pathMap.PathMapArray = GetPathsArray();
        _pathMap.PathEnter = _pathEnter;
        _pathMap.PathExit = _pathExit;
        _pathMap.PathLength = _pathLength;
        return _pathMap;
    }
    private bool[,] GetPathsArray()
    {
        _subPassPoints = new Point[(_pathMap.PathMapArray.GetLength(1) - 1) / 2, (_pathMap.PathMapArray.GetLength(0) - 1) / 2];
        return new bool[0, 0];
    }
    /*static private void FillLabyrinthBlocks()
    {
        Point currentPoint;
        for (int row = 0; row < _map.GetLength(0); row++)
        {
            for (int col = 0; col < _map.GetLength(0); col++)
            {
                currentPoint = new Point(col, row);
                if (
                    row % 2 != 0 && col % 2 != 0 &&
                    row < _map.GetLength(0) - 1 && col < _map.GetLength(1) - 1
                    )
                {
                    SetMapBlock(currentPoint, PassId);
                }
                else
                {
                    SetMapWall(currentPoint);
                    _subPassPoints.Add(currentPoint);
                }
            }
        }
    }*/
    private bool IsInMapRange(Point point, bool[,] map)
    {
        if (point.X > 0 && point.X < map.GetLength(1) && point.Y > 0 && point.Y < map.GetLength(0))
        {
            return true;
        }
        return false;
    }
}

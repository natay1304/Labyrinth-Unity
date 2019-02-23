using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PathGenerator
{
    private Stack<Point> _subPassPointsPath = new Stack<Point>();
    private Point _currentSubPassPoint = new Point(0,0);
    private Point _currentPathExit = new Point(0,0); 
    private int _currentPathLength = 0;
    public PathMap GeneratePathMap(int height, int width)
    {
        PathMap pathMap = new PathMap(width, height);
        Point[,] subPassPoints = GetSubPassPoints(pathMap.PathMapArray);
        pathMap.PathEnter = GetPathEnter(subPassPoints);
        SetPaths(ref pathMap, ref subPassPoints);
        pathMap.PathExit = _currentPathExit;
        pathMap.PathLength = _currentPathLength;
        return pathMap;
    }
    private Point[,] GetSubPassPoints(bool[,] passArray)
    {
        Point[,] subPassPoints = new Point[(passArray.GetLength(1) - 1) / 2, (passArray.GetLength(0) - 1) / 2];
        for (int row = 1; row < passArray.GetLength(0)-1; row++)
        {
            for (int col = 1; col < passArray.GetLength(1)-1; col++)
            {
                if(row%2 == 0 && col%2 == 0)
                {
                    subPassPoints[row / 2, col / 2] = new Point(col, row);
                }
            }
        }
        return subPassPoints;
    }
    private Point GetPathEnter(Point[,] subPathPoints)
    {
        _currentSubPassPoint = new Point(
                Random.Range(0, subPathPoints.GetLength(0) - 1),
                Random.Range(0, subPathPoints.GetLength(1) - 1)
            );
        _subPassPointsPath.Push(_currentSubPassPoint);
        return subPathPoints[
                _currentSubPassPoint.Y,
                _currentSubPassPoint.X
            ];
    }
    private void SetPaths( ref PathMap pathMap, ref Point[,] subPassPoints)
    {
        SetPass(ref pathMap, pathMap.PathEnter);
        Point nextSubPassPoint;
        do
        {
            List<Point> nextSubPassPoints = GetNextSubPassPoints(ref subPassPoints);
            if (nextSubPassPoints.Count > 0)
            {
                nextSubPassPoint = GetNextSubPassPoint(nextSubPassPoints);
                SetPass(ref pathMap, GetSubPassMapPoint(nextSubPassPoint, subPassPoints));
                SetPass(ref pathMap, subPassPoints[_currentSubPassPoint.Y, _currentSubPassPoint.X]);
                _subPassPointsPath.Push(nextSubPassPoint);
                AddPathLength(ref pathMap, subPassPoints);
            }
            else if (_subPassPointsPath.Count > 0)
            {
                _currentSubPassPoint = _subPassPointsPath.Pop();
                _currentPathLength -= 2;
            }
        } while (_subPassPointsPath.Count > 0);
    }
    private void SetPass(ref PathMap pathMap, Point point)
    {
        pathMap.PathMapArray[point.Y, point.X] = true;
    }
    private Point GetSubPassMapPoint(Point nextSubPassPoint, Point[,] subPassPoints)
    {
        Point subPassMapPoint = subPassPoints[_currentSubPassPoint.Y,_currentSubPassPoint.X];
        if(_currentSubPassPoint.Y < nextSubPassPoint.Y)
        {
            subPassMapPoint.Y++; 
        }
        if(_currentSubPassPoint.Y > nextSubPassPoint.Y)
        {
            subPassMapPoint.Y--; 
        }
        if(_currentSubPassPoint.X < nextSubPassPoint.X)
        {
            subPassMapPoint.X++; 
        }
        if(_currentSubPassPoint.X > nextSubPassPoint.X)
        {
            subPassMapPoint.X--; 
        }
        return subPassMapPoint;
    }
    private Point GetNextSubPassPoint(List<Point> NextSubPassPoints)
    {
        return NextSubPassPoints[Random.Range(0,NextSubPassPoints.Count-1)];
    }
    private List<Point> GetNextSubPassPoints(ref Point[,] subPassPoints)
    {
        List<Point> nextSubPassPoints = new List<Point>();
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                if(IsPassableVector(subPassPoints, _currentSubPassPoint, new Point(x,y)))
                {
                    nextSubPassPoints.Add(new Point(_currentSubPassPoint.X + x, _currentSubPassPoint.Y + y));
                }
            }
        }
        return nextSubPassPoints;
    }
    private bool IsPassableVector(Point[,] subPassPoints, Point currentSubPassPoint,  Point passVector)
    {
        return
            Mathf.Abs(passVector.Y) != Mathf.Abs(passVector.X) &&
            (currentSubPassPoint.Y + passVector.Y > subPassPoints.GetLength(0) ||
            currentSubPassPoint.X + passVector.X > subPassPoints.GetLength(1));
    }
    private bool IsInMapRange(Point point, bool[,] map)
    {
        if (point.X > 0 && point.X < map.GetLength(1) && point.Y > 0 && point.Y < map.GetLength(0))
        {
            return true;
        }
        return false;
    }
    private void AddPathLength(ref PathMap pathMap, Point[,] subPassPoints)
    {
        _currentPathLength += 2;
        if(_currentPathLength > pathMap.PathLength)
        {
            pathMap.PathLength = _currentPathLength;
            _currentPathExit = subPassPoints[_currentSubPassPoint.Y, _currentSubPassPoint.X];
        }
    }
}

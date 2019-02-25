using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PathGenerator
{
    private Stack<Point> _keyPointsPath = new Stack<Point>();
    private Point _exitPoint;
    private bool _isExitExists = false; 
    private int _currentPathLength = 0;
    public PathMap GeneratePathMap(int height, int width, int pathLength)
    {
        PathMap pathMap = new PathMap(width, height, pathLength);
        pathMap.KeyPoints = GetKeyPoints(pathMap);
        Point currentKeyPoint = GetRandomPoint(pathMap.KeyPoints);
        pathMap.PathEnter = GetMapPoint(pathMap.KeyPoints, currentKeyPoint);
        pathMap.PathMapArray = GetPathMapArray(ref pathMap, currentKeyPoint);
        pathMap.PathExit = _exitPoint;
        return pathMap;
    }
    private Point[,] GetKeyPoints(PathMap pathMap)
    {
        Point[,] keyPoints = pathMap.KeyPoints;
        for (int row = 1; row < pathMap.PathMapArray.GetLength(0)-1; row+=2)
        {
            for (int col = 1; col < pathMap.PathMapArray.GetLength(1)-1; col+=2)
            {
                keyPoints[(row-1) / 2, (col-1) / 2] = new Point(col, row);
            }
        }
        return keyPoints;
    }
    private Point GetRandomPoint(Point[,] pointsArray)
    {
        return new Point(
                UnityEngine.Random.Range(0, pointsArray.GetLength(0)),
                UnityEngine.Random.Range(0, pointsArray.GetLength(1))
            );
    }
    private bool[,] GetPathMapArray(ref PathMap pathMap, Point currentKeyPoint)
    {
        bool[,] mapArray = pathMap.PathMapArray;
        Point nextKeyPoint;
        SetPass(pathMap.KeyPoints[currentKeyPoint.Y, currentKeyPoint.X], ref mapArray);
        _keyPointsPath.Push(currentKeyPoint);
        do
        {
            List<Point> nextKeyPoints = GetNextKeyPoints(pathMap, currentKeyPoint);
            if (nextKeyPoints.Count > 0 && _currentPathLength < pathMap.PathLength)
            {
                nextKeyPoint = GetNextKeyPoint(nextKeyPoints);
                _keyPointsPath.Push(nextKeyPoint);

                SetPass(GetMapPoint(pathMap.KeyPoints, nextKeyPoint), ref mapArray);
                SetPass(GetNextMapPoint(pathMap.KeyPoints, nextKeyPoint, currentKeyPoint), ref mapArray);
                
                currentKeyPoint = nextKeyPoint;
            }
            else if (_keyPointsPath.Count > 0)
            {
                if(_currentPathLength >= pathMap.PathLength && !_isExitExists)
                {
                    _exitPoint = GetMapPoint(pathMap.KeyPoints, currentKeyPoint);
                    _isExitExists = true;
                    _keyPointsPath.Pop();
                    _currentPathLength -= 2;
                }
                currentKeyPoint = _keyPointsPath.Pop();
                _currentPathLength -= 2;
            }
        } while (_keyPointsPath.Count > 0);
        return mapArray;
    }


    private Point GetPassableKeyPoint(PathMap pathMap)
    {
        Point currentKeyPoint;
        for (int y = 0; y < pathMap.KeyPoints.GetLength(0); y++)
        {
            for (int x = 0; x < pathMap.KeyPoints.GetLength(1); x++)
            {
                currentKeyPoint = new Point(x, y);
                if (IsPassedKeyPoint(pathMap, currentKeyPoint))
                {
                    return currentKeyPoint;
                }
            }
        }
        return currentKeyPoint;
    }
    private void SetPass(Point point, ref bool[,] mapArray)
    {
        _currentPathLength ++;
        mapArray[point.Y, point.X] = true;
    }
    private Point GetNextMapPoint(Point[,] keyPoints, Point nextKeyPoint, Point currentKeyPoint)
    {
        Point currentMapPoint = GetMapPoint(keyPoints, currentKeyPoint);
        Point nextMapPoint = GetMapPoint(keyPoints, nextKeyPoint);
        return GetNextPoint(currentMapPoint, nextMapPoint);
    }
    private Point GetMapPoint(Point[,] keyPoints, Point keyPoint)
    {
        return keyPoints[keyPoint.Y, keyPoint.X];
    }
    private Point GetNextPoint(Point currentPoint, Point aimPoint)
    {
        int diffX = NormalizeValue(currentPoint.X - aimPoint.X);
        int diffY = NormalizeValue(currentPoint.Y - aimPoint.Y);
        return new Point(currentPoint.X - diffX, currentPoint.Y - diffY);
    }
    private int NormalizeValue(int diff)
    {
        if (diff != 0)
        {
            diff /= Math.Abs(diff);
        }
        return diff;
    }

    private Point GetNextKeyPoint(List<Point> NextKeyPoints)
    {
        return NextKeyPoints[UnityEngine.Random.Range(0,NextKeyPoints.Count)];
    }
    private List<Point> GetNextKeyPoints(PathMap pathMap, Point currentKeyPoint)
    {
        List<Point> nextKeyPoints = new List<Point>();
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                if(IsPassableVector(pathMap, currentKeyPoint, new Point(x,y)))
                {
                    nextKeyPoints.Add(new Point(currentKeyPoint.X + x, currentKeyPoint.Y + y));
                }
            }
        }
        return nextKeyPoints;
    }
    private bool IsPassableVector(PathMap pathMap, Point currentKeyPoint,  Point passVector)
    {
        Point nextKeyPoint = new Point(currentKeyPoint.X + passVector.X, currentKeyPoint.Y + passVector.Y);
        return
            Mathf.Abs(passVector.Y) != Mathf.Abs(passVector.X) &&
            IsInPointsRange(nextKeyPoint, pathMap.KeyPoints)&&
            !IsPassedKeyPoint(pathMap, nextKeyPoint);
    }
    private bool IsPassedKeyPoint(PathMap pathMap, Point nextKeyPoint)
    {
        Point mapPoint = GetMapPoint(pathMap.KeyPoints, nextKeyPoint);
        return pathMap.PathMapArray[mapPoint.Y, mapPoint.X] == true;
    }
    private bool IsInPointsRange(Point point, Point[,] points)
    {
        return point.X >= 0 &&
        point.X < points.GetLength(1) &&
        point.Y >= 0 &&
        point.Y < points.GetLength(0);
    }
}

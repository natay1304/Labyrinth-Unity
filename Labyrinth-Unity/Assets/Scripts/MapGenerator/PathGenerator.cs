using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace LabyrinthUnity.MapGeneratorNS
{
    internal class PathGenerator
    {
        private Stack<Point> _keyPointsPath = new Stack<Point>();
        private Point _exitPoint;
        private bool _isExitExists = false;
        private int _currentPathLength = 0;

        public PathMap GeneratePathMap(int height, int width, int pathLength, Point? enterPoint = null)
        {
            PathMap pathMap = new PathMap(width, height, pathLength);
            pathMap.KeyPoints = GetKeyPoints(pathMap);
            Point enterKeyPoint = GetRandomPoint(pathMap.KeyPoints);
            pathMap.Enter = enterPoint.HasValue ? GetMapPoint(pathMap.KeyPoints, enterKeyPoint) : enterPoint.Value;
            pathMap.PathMapArray = GetPathMapArray(ref pathMap, enterKeyPoint);
            pathMap.Exit = _exitPoint;
            pathMap.isExitExists = _isExitExists;
            return pathMap;
        }

        private Point[,] GetKeyPoints(PathMap pathMap)
        {
            Point[,] keyPoints = pathMap.KeyPoints;
            for (int row = 1; row < pathMap.PathMapArray.GetLength(0) - 1; row += 2)
            {
                for (int col = 1; col < pathMap.PathMapArray.GetLength(1) - 1; col += 2)
                {
                    keyPoints[(row - 1) / 2, (col - 1) / 2] = new Point(col, row);
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
            SetEnterPass(pathMap, ref currentKeyPoint, ref mapArray);
            do
            {
                List<Point> nextKeyPoints = GetNextKeyPoints(pathMap, currentKeyPoint);
                if (IsExitPoint(pathMap))
                {
                    SetExit(pathMap, currentKeyPoint, ref nextKeyPoints);
                }
                if (IsImpasse(nextKeyPoints))
                {
                    currentKeyPoint = GetPreviousKeyPoint();
                }
                else
                {
                    nextKeyPoint = GetNextKeyPoint(nextKeyPoints);
                    SetPassesToKeyPoint(ref mapArray, pathMap, currentKeyPoint, nextKeyPoint);
                    currentKeyPoint = GetCurrentKeyPoint(nextKeyPoint);
                }
            } while (_keyPointsPath.Count > 0);
            return mapArray;
        }

        private void SetEnterPass(PathMap pathMap, ref Point currentKeyPoint, ref bool[,] mapArray)
        {
            SetPass(pathMap.KeyPoints[currentKeyPoint.Y, currentKeyPoint.X], ref mapArray);
            _keyPointsPath.Push(currentKeyPoint);
        }

        private void SetPass(Point point, ref bool[,] mapArray)
        {
            _currentPathLength++;
            mapArray[point.Y, point.X] = true;
        }

        private bool IsImpasse(List<Point> nextKeyPoints)
        {
            return _keyPointsPath.Count > 0 && nextKeyPoints.Count == 0;
        }

        private bool IsExitPoint(PathMap pathMap)
        {
            return _currentPathLength >= pathMap.PathLength && !_isExitExists;
        }

        private void SetExit(PathMap pathMap, Point currentKeyPoint, ref List<Point> nextKeyPoints)
        {
            nextKeyPoints = new List<Point>();
            _exitPoint = GetMapPoint(pathMap.KeyPoints, currentKeyPoint);
            _isExitExists = true;
            _keyPointsPath.Pop();
            _currentPathLength -= 2;
        }

        private Point GetPreviousKeyPoint()
        {
            Point currentKeyPoint = _keyPointsPath.Pop();
            _currentPathLength -= 2;
            return currentKeyPoint;
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
            return NextKeyPoints[UnityEngine.Random.Range(0, NextKeyPoints.Count)];
        }

        private bool[,] SetPassesToKeyPoint(
                ref bool[,] mapArray, 
                PathMap pathMap, 
                Point currentKeyPoint, 
                Point nextKeyPoint
            )
        {
            SetPass(GetMapPoint(pathMap.KeyPoints, nextKeyPoint), ref mapArray);
            SetPass(GetNextMapPoint(pathMap.KeyPoints, nextKeyPoint, currentKeyPoint), ref mapArray);
            return mapArray;
        }

        private Point GetCurrentKeyPoint(Point nextKeyPoint)
        {
            Point currentKeyPoint;
            _keyPointsPath.Push(nextKeyPoint);
            currentKeyPoint = nextKeyPoint;
            return currentKeyPoint;
        }
        private List<Point> GetNextKeyPoints(PathMap pathMap, Point currentKeyPoint)
        {
            List<Point> nextKeyPoints = new List<Point>();
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (IsPassableVector(pathMap, currentKeyPoint, new Point(x, y)))
                    {
                        nextKeyPoints.Add(new Point(currentKeyPoint.X + x, currentKeyPoint.Y + y));
                    }
                }
            }
            return nextKeyPoints;
        }

        private bool IsPassableVector(PathMap pathMap, Point currentKeyPoint, Point passVector)
        {
            Point nextKeyPoint = new Point(currentKeyPoint.X + passVector.X, currentKeyPoint.Y + passVector.Y);
            return
                Mathf.Abs(passVector.Y) != Mathf.Abs(passVector.X) &&
                IsInPointsRange(nextKeyPoint, pathMap.KeyPoints) &&
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
}
 

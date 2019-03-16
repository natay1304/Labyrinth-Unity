using System;
using System.Drawing;
using UnityEngine;

namespace LabyrinthUnity.MapGeneratorNS
{
    internal class PathMap
    {
        
        public PathMap(int width, int height, int pathLength)
        {
            _pathMap = new bool[GetOdd(height), GetOdd(width)];
            KeyPoints = new Point[(_pathMap.GetLength(0) - 1) / 2, (_pathMap.GetLength(1) - 1) / 2];
            PathLength = pathLength;
        }
        
        private Point[,] _keyPoints = new Point[0, 0];
        private int _pathLength = 0;
        private bool[,] _pathMap;
        private Point _enter = new Point(0, 0);
        private Point _exit = new Point(0, 0);
        public bool isExitExists = false; 
        

        public int PathLength
        {
            get => _pathLength;
            set
            {
                if(IsValidPathLength(value))
                {
                    value = GetOdd(value);
                    _pathLength = value;
                }
            }
        }
        private bool IsValidPathLength(int pathLength)
        {
            if (pathLength > _pathMap.GetLength(0) * _pathMap.GetLength(1) / 3)
                throw new ArgumentException(
                    String.Format("Too low map size ({0} blocks) for such path ({1}).",
                        _pathMap.GetLength(0) * _pathMap.GetLength(1),
                        pathLength
                    ),
                    "PathLength"
                );

            return true;
        }

        public bool[,] PathMapArray
        {
            get => _pathMap;
            set
            {
                if (IsValidPathMapArray(value))
                    _pathMap = value;
            }
        }

        private bool IsValidPathMapArray(bool[,] pathMapArray)
        {
            if(pathMapArray.GetLength(0) < 5)
                throw new ArgumentException("Too low map height. Need height >= 5. " +
                    "You entered " + PathMapArray.GetLength(0), "height");

            if (pathMapArray.GetLength(1) < 5)
                throw new ArgumentException("Too low map width. Need width >= 5. " +
                    "You entered " + PathMapArray.GetLength(1), "width");

            return true;
        }

        public Point Enter
        {
            get => _enter;
            set
            {
                if (IsPositivePoint(value, new ArgumentException("Enter point may have only positive X and Y " +
                        "You entered y=" + value.Y + " x=" + value.X, "Enter")))
                    _enter = value;
            }
        }

        public Point Exit
        {
            get => _exit;
            set
            {
                if(IsPositivePoint(value, new ArgumentException("Exit point may have only positive X and Y " +
                        "You entered y=" + value.Y + " x=" + value.X, "Exit")))
                    _exit = value;
            }
        }

        public Point[,] KeyPoints
        {
            get => _keyPoints;
            set
            {
                if(value.Length <= 0)
                    throw new ArgumentException("Too low array length of KeyPoints. It must be more then 0." +
                        "Current KeyPoints.Lenght is: " + value.Length, "KeyPoints");

                _keyPoints = value;

            }
        }

        private bool IsPositivePoint(Point point, ArgumentException exception)
        {
            if(point.X < 0 && point.Y < 0)
                throw exception;

            return true;
        }

        private int GetOdd(int value)
        {
            if (value % 2 == 0)
            {
                value++;
            }
            return value;
        }
    }
}


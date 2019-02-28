using System;
using System.Drawing;
using UnityEngine;

namespace LabyrinthUnity.MapGenerator
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
        private Point _pathEnter = new Point(0, 0);
        private Point _pathExit = new Point(0, 0);
        public bool isExitExists = false; 
        

        public int PathLength
        {
            get => _pathLength;
            set
            {
                if (_pathLength > _pathMap.GetLength(0) * _pathMap.GetLength(1) / 3)
                {
                    throw new ArgumentException(
                        String.Format("Too low map size ({0} blocks) for such path ({1}).",
                            _pathMap.GetLength(0) * _pathMap.GetLength(1),
                            _pathLength
                        ),
                        "PathLength"
                    );
                }
                else
                {
                    while (value % 2 == 0)
                    {
                        value += 1;
                    }
                }
                _pathLength = value;
            }
        }

        public bool[,] PathMapArray
        {
            get => _pathMap;
            set
            {
                if (PathMapArray.GetLength(1) < 5)
                {
                    throw new ArgumentException("Too low map width. Need width >= 5. " +
                        "You entered " + PathMapArray.GetLength(1), "width");
                }
                if (PathMapArray.GetLength(0) < 5)
                {
                    throw new ArgumentException("Too low map height. Need width >= 5. " +
                        "You entered " + PathMapArray.GetLength(0), "height");
                }
                _pathMap = value;
            }
        }

        public Point PathEnter
        {
            get => _pathEnter;
            set
            {
                if (value.X >= 0 && value.Y >= 0)
                {
                    _pathEnter = value;
                }
                else
                {
                    throw new ArgumentException("PathEnter point may have only positive X and Y " +
                        "You entered y=" + value.Y+" x=" + value.X, "PathEnter");
                }

            }
        }

        public Point PathExit
        {
            get => _pathExit;
            set
            {
                if (value.X >= 0 && value.Y >= 0)
                {
                    _pathExit = value;
                }
                else
                {
                    throw new ArgumentException("PathExit point may have only positive X and Y " +
                        "You entered y=" + value.Y + " x=" + value.X, "PathExit");
                }
            }
        }

        public Point[,] KeyPoints
        {
            get => _keyPoints;
            set
            {
                if(value.Length > 0)
                {
                    _keyPoints = value;
                }
                else
                {
                    throw new ArgumentException("Too low array length of KeyPoints. It must be more then 0." +
                        "Current KeyPoints.Lenght is: " + value.Length, "KeyPoints");
                }
                
            }
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


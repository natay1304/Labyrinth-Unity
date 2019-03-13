using System;
using System.Drawing;
using UnityEngine;

namespace LabyrinthUnity.LocationGenerator
{
    public class Location : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _cellSize = new Vector3(3,3,3);
        public Vector3 CellSize { get => _cellSize; set => _cellSize = value; }

        public LocationLib locationLib;

        public Pass currentPass;

        [SerializeField]
        private Vector2 _enterCell;
        public Point EnterCell { get => VectorToPoint(_enterCell); set => _enterCell = PointToVector(value); }

        [SerializeField]
        private Vector2 _sizeInCells = new Vector2(30, 30);
        public Point SizeInCells { get => VectorToPoint(_sizeInCells); set => _enterCell = PointToVector(value); }

        [SerializeField]
        private int _enterToExitCells = 200;
        public int EnterToExitCells { get => _enterToExitCells; set => _enterToExitCells = value; }

        private void OnValidate()
        {
            if(_sizeInCells.x < 5)
                _sizeInCells.x = 5;
            if(_sizeInCells.y < 5)
                _sizeInCells.y = 5;
            if(_enterToExitCells > (_sizeInCells.x* _sizeInCells.y)/3)
            {
                _enterToExitCells = Mathf.FloorToInt((_sizeInCells.x * _sizeInCells.y) / 3);
            }
        }
        private Point VectorToPoint(Vector2 vector2)
        {
            return new Point(Convert.ToInt32(vector2.y), Convert.ToInt32(vector2.y));
        }
        private Vector2 PointToVector(Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public void Regenerate(Vector2? enterCell = null)
        {
            if (enterCell.HasValue)
                _enterCell = enterCell.Value;
            Clear();
            LocationGenerator locationGenerator = new LocationGenerator();
            locationGenerator.GenerateLocation(this);
        }
        void Start()
        {
            Regenerate();
        }
        private void Clear()
        {
            if (transform.childCount > 0)
            {
                for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
                {
                    Destroy(transform.GetChild(childIndex).gameObject);
                }
            }
        }
    }

}

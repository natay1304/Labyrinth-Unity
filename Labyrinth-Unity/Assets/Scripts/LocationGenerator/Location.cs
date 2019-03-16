using UnityEngine;

namespace LabyrinthUnity.LocationGenerator
{
    public class Location : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _cellSize = new Vector3(3,3,3);
        public Vector3 CellSize { get => _cellSize; set => _cellSize = value; }

        public LocationLib LocationLib;

        public Pass CurrentPass;

        [SerializeField]
        private Vector2Int _enterCell;
        public Vector2Int EnterCell { get => _enterCell; set => _enterCell = value; }


        [SerializeField]
        private Vector2Int _sizeInCells = new Vector2Int(10, 10);
        public Vector2Int SizeInCells { get => _sizeInCells; set => _sizeInCells = value; }

        [SerializeField]
        private int _enterToExitCells = 200;
        public int EnterToExitCells { get => _enterToExitCells; set => _enterToExitCells = GetValidPathLength(value); }
        
        private void OnValidate()
        {
            if (_sizeInCells.x < 5)
                _sizeInCells.x = 5;
            if(_sizeInCells.y < 5)
                _sizeInCells.y = 5;
            EnterToExitCells = _enterToExitCells;
        }

        public void Regenerate(Vector2Int? enterCell = null)
        {
            if (enterCell.HasValue)
                _enterCell = enterCell.Value;
            Clear();
            LocationGenerator locationGenerator = new LocationGenerator();
            locationGenerator.GenerateLocation(this);
        }

        private void Start()
        {
            Regenerate();
        }

        private void Clear()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        private int GetValidPathLength(int pathLength)
        {
            if (pathLength > SizeInCells.x * SizeInCells.y / 3)
            {
                pathLength = Mathf.FloorToInt((SizeInCells.x * SizeInCells.y) / 3);
            }
            else if(pathLength < 5)
            {
                pathLength = 5;
            }
            return pathLength;
        }
    }

}

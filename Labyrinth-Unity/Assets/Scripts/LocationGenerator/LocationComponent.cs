using System.Drawing;
using UnityEngine;

namespace LabyrinthUnity.LocationGenerator
{
    public class LocationComponent : MonoBehaviour
    {
        public LocationLib locationLib;
        [SerializeField]
        private Point _exitCell;
        public Point ExitCell { get => _exitCell; set => _exitCell = value; }
        [SerializeField]
        private Point _enterCell;
        public Point EnterCell { get => _enterCell; set => _enterCell = value; }
        [SerializeField]
        private Point _sizeInCells = new Point(30, 30);
        public Point SizeInCells { get => _sizeInCells; set => _sizeInCells = value; }
        [SerializeField]
        private int _enterToExitCells = 200;
        public int EnterToExitCells { get => _enterToExitCells; set => _enterToExitCells = value; }

        private void OnValidate()
        {
            if(_sizeInCells.X < 5)
                _sizeInCells.X = 5;
            if(_sizeInCells.Y < 5)
                _sizeInCells.Y = 5;
            if(_enterToExitCells > (_sizeInCells.X* _sizeInCells.Y)/3)
            {
                _enterToExitCells = Mathf.FloorToInt((_sizeInCells.X * _sizeInCells.Y) / 3);
            }
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

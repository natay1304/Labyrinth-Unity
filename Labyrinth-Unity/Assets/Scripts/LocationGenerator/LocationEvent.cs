using UnityEngine;

namespace LabyrinthUnity.LocationGenerator
{
    class LocationEvent
    {
        private readonly Location _location;
        public LocationEvent(Location location)
        {
            _location = location;
        }
        public void Run()
        {
            if(
                _location.currentPass.Coordinates.x == _location.ExitCell.X &&
                _location.currentPass.Coordinates.y == _location.ExitCell.Y
                )
            {
                NextLocation();
            }
        }
        private void NextLocation()
        {
            _location.Regenerate();
        }
    }
}

using UnityEngine;
using LabyrinthUnity.MapGenerator;


namespace LabyrinthUnity.LocationGenerator
{
    internal class LocationGenerator
    {
        public void GenerateLocation(LocationComponent location, LocationLib blocksLib)
        {
            MapGenerator.MapGenerator mapGenerator = new MapGenerator.MapGenerator(new MapIds());
            int[,] labyrinthMap = mapGenerator.GenerateLabyrinth(
                    location.SizeInCells, 
                    location.EnterToExitCells, 
                    location.EnterCell
                );
            GenerateCells(location, blocksLib, labyrinthMap);
        }
        private void GenerateCells(LocationComponent location, LocationLib blocksLib, int[,] labyrinthMap)
        {

        }
    }
}

using UnityEngine;
using LabyrinthUnity.MapGenerator;
using System.Drawing;

namespace LabyrinthUnity.LocationGenerator
{
    internal class LocationGenerator
    {
        public void GenerateLocation(Location location)
        {
            MapGenerator.MapGenerator mapGenerator = new MapGenerator.MapGenerator(new MapIds());
            int[,] labyrinthMap = mapGenerator.GenerateLabyrinth(
                    location.SizeInCells, 
                    location.EnterToExitCells, 
                    location.EnterCell
                );
            GenerateCells(location, labyrinthMap);
        }
        private void GenerateCells(Location location, int[,] labyrinthMap)
        {
            Point currentPoint = new Point();
            for (int y = 0; y < labyrinthMap.GetLength(0); y++)
            {
                for (int x = 0; x < labyrinthMap.GetLength(1); x++)
                {
                    currentPoint.Y = y;
                    currentPoint.X = x;
                    SetBlock(location, labyrinthMap[y, x], currentPoint);
                }
            }

        }
        private void SetBlock(Location location, int blockId, Point mapPosition)
        {
            SetBlockObject(location, 3, mapPosition, -1);
            SetBlockObject(location, blockId, mapPosition);
            //SetCeilBlockObject(location, 3, mapPosition);
        }
        private void SetBlockObject(Location location, int blockId, Point mapPosition, int positionZ = 0)
        {
            GameObject blockPrefab = location.locationLib.Blocks[blockId];
            Vector3 worldPosition = new Vector3(
                    location.transform.position.x + mapPosition.X * blockPrefab.transform.localScale.x,
                    location.transform.position.z + (blockPrefab.transform.localScale.y * positionZ),
                    location.transform.position.y + mapPosition.Y * blockPrefab.transform.localScale.y
                );
            Object.Instantiate(blockPrefab, worldPosition, Quaternion.identity, location.transform);
        }
    }
}

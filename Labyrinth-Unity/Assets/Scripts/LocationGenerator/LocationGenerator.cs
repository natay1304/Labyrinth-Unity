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
            SetBlockObject(location, 3, mapPosition, 1);
        }
        private void SetBlockObject(Location location, int blockId, Point mapPosition, int positionZ = 0)
        {
            GameObject blockPrefab = location.locationLib.Blocks[blockId];
            TrySetPass(location, blockPrefab, mapPosition);
            Vector3 worldPosition = new Vector3(
                    location.transform.position.x + mapPosition.X * location.CellSize.x,
                    location.transform.position.z + (location.CellSize.z * positionZ),
                    location.transform.position.y + mapPosition.Y * location.CellSize.y
                );
            Object.Instantiate(blockPrefab, worldPosition, Quaternion.identity, location.transform);
        }
        private void TrySetPass(Location location, GameObject blockPrefab, Point mapPosition)
        {
            Pass pass = blockPrefab.GetComponent<Pass>();
            if (pass != null)
            {
                pass.Location = location;
                pass.Coordinates = new Vector2(mapPosition.X, mapPosition.Y);
            }
        }
    }
}

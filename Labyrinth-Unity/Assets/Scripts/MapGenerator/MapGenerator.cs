using System.Drawing;
using UnityEngine;

namespace Assets.Scripts.MapGenerator
{
    public class MapGenerator
    {
        private MapIds _mapIds = new MapIds();

        public MapIds MapIds { get => _mapIds; set => _mapIds = value; }

        public int[,] GenerateLabyrinth(int height, int width, int labyrinthLenght)
        {
            PathGenerator pathGenerator = new PathGenerator();
            int[,] map = new int[0, 0];
            if (labyrinthLenght > height * width / 4)
            {
                Debug.Log("Too low map size (" + height * width + " blocks) for such path (" + labyrinthLenght + ").");
            }
            else
            {
                PathMap pathMap = pathGenerator.GeneratePathMap(height, width, labyrinthLenght);
                map = GanerateIds(pathMap, MapIds);
            }
            return map;
        }
        private int[,] GanerateIds(PathMap pathMap, MapIds mapIds)
        {
            int[,] idsMap = new int[pathMap.PathMapArray.GetLength(0), pathMap.PathMapArray.GetLength(1)];
            for (int y = 0; y < pathMap.PathMapArray.GetLength(0); y++)
            {
                for (int x = 0; x < pathMap.PathMapArray.GetLength(1); x++)
                {
                    if (pathMap.PathMapArray[y, x] == false)
                    {
                        idsMap[y, x] = mapIds.BlocksIds[Random.Range(0, mapIds.BlocksIds.Length - 1)];
                    }
                    else
                    {
                        idsMap[y, x] = mapIds.PassId;
                    }
                }
            }
            idsMap[pathMap.PathEnter.Y, pathMap.PathEnter.X] = mapIds.EnterId;
            idsMap[pathMap.PathExit.Y, pathMap.PathExit.X] = mapIds.ExitId;
            return idsMap;
        }
    }
}
    

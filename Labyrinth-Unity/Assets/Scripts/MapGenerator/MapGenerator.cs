using System;
using System.Drawing;
using UnityEngine;

namespace LabyrinthUnity.MapGenerator
{
    public class MapGenerator
    {
        private MapIds _mapIds = new MapIds();
        public MapIds MapIds { get => _mapIds; set => _mapIds = value; }

        private int _attemptsNumber = 30;
        public int AttemptsNumber
        {
            get => _attemptsNumber;
            set
            {
                if(value > 0)
                {
                    _attemptsNumber = value;
                }
                else
                {
                    throw new ArgumentException("Too low AttemptsNumber count. It must be more then 0." +
                        "Entered AttemptsNumber is: " + value, "AttemptsNumber");
                }
            }
        }

        public int[,] GenerateLabyrinth(int height, int width, int labyrinthLenght)
        {

            PathGenerator pathGenerator = new PathGenerator();
            int[,] map = new int[0,0];
            try
            {
                PathMap pathMap;
                int attemptsNumber = AttemptsNumber;
                do
                {
                    pathMap = pathGenerator.GeneratePathMap(height, width, labyrinthLenght);
                    attemptsNumber--;
                } while (!pathMap.isExitExists && attemptsNumber >= 0);
                if (!pathMap.isExitExists)
                {
                    throw new ArgumentException(
                        String.Format("Too low map size ({0} blocks) for such labyrinthLenght ({1}).",
                            height * width,
                            labyrinthLenght
                        ),
                        "labyrinthLenght"
                    );
                }
                map = GanerateMapIds(pathMap, MapIds);
            }
            catch (ArgumentException e)
            {
                Debug.Log(String.Format("{0}: {1}", e.GetType().Name, e.Message));
                throw new ArgumentException("Labyrinth map is not created.", "MapGenerator");
            }
            return map;
        }

        private int[,] GanerateMapIds(PathMap pathMap, MapIds mapIds)
        {
            int[,] idsMap = new int[pathMap.PathMapArray.GetLength(0), pathMap.PathMapArray.GetLength(1)];
            for (int y = 0; y < pathMap.PathMapArray.GetLength(0); y++)
            {
                for (int x = 0; x < pathMap.PathMapArray.GetLength(1); x++)
                {
                    if (pathMap.PathMapArray[y, x] == false)
                    {
                        idsMap[y, x] = mapIds.BlocksIds[UnityEngine.Random.Range(0, mapIds.BlocksIds.Length - 1)];
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
    

using System;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace LabyrinthUnity.MapGeneratorNS
{
    public class MapGenerator
    {
        public MapIds MapIds { get; private set; }
        public int AttemptsNumber { get; private set; }

        public MapGenerator(MapIds mapIds, int attemptsNumber = 30)
        {
            if (attemptsNumber < 1)
                throw new ArgumentException("Too low AttemptsNumber count. It must be more then 0." +
                        "Entered attemptsNumber is: " + attemptsNumber, "attemptsNumber");
            MapIds = mapIds;
            AttemptsNumber = attemptsNumber;
        }

        public int[,] GenerateLabyrinth(int hidth, int width, int labyrinthLenght, Point? enterPoint = null)
        {
            return GenerateLabyrinth(new Point(width, hidth), labyrinthLenght, enterPoint);
        }
        public int[,] GenerateLabyrinth(Point labyrinthSize, int labyrinthLenght, Point? enterPoint = null)
        {
            PathGenerator pathGenerator = new PathGenerator();
            int[,] map = new int[0,0];
            try
            {
                PathMap pathMap;
                int attemptsNumber = AttemptsNumber;
                do
                {
                    pathMap = pathGenerator.GeneratePathMap(labyrinthSize.Y, labyrinthSize.X, labyrinthLenght, enterPoint);
                    attemptsNumber--;
                } while (!pathMap.isExitExists && attemptsNumber >= 0);

                if (!pathMap.isExitExists)
                {
                    throw new ArgumentException(
                        String.Format("Too low map size ({0} blocks) for such labyrinthLenght ({1}).",
                            labyrinthSize.Y * labyrinthSize.X,
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
                        idsMap[y, x] = mapIds.WallsIds.ElementAt(UnityEngine.Random.Range(0, mapIds.WallsIds.Count() - 1));
                    }
                    else
                    {
                        idsMap[y, x] = mapIds.PassId;
                    }
                }
            }
            idsMap[pathMap.Enter.Y, pathMap.Enter.X] = mapIds.EnterId;
            idsMap[pathMap.Exit.Y, pathMap.Exit.X] = mapIds.ExitId;
            return idsMap;
        }
    }
}
    

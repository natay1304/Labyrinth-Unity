using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class MapGenerator
{
    static private int[,] _map;
    static private int _labyrinthLength;
    static private int _passId = 0;
    public static int PassId { get => _passId; set => _passId = value; }
    static private int _enterId = 1;
    static private Point _enterPoint;
    public static int EnterId { get => _enterId; set => _enterId = value; }
    static private int _exitId = 2;
    static private Point _exitPoint;
    public static int ExitId { get => _exitId; set => _exitId = value; }
    static private int _defaultBlockId = 3;
    static private int[] _blocksIds = new int[1] {_defaultBlockId};
    static public int[] BlocksIds
    {
        get {
            return _blocksIds;
        }
        set {
            if (value.Length > 0 && !DefinedIdsExists(value))
            {
                _blocksIds = value;
            }
            else
            {
                Debug.Log("Blocks Ids list is not setted. It is empty or exists defined IDs");
            }
        }
    }


    static public int[,] GenerateLabyrinth(int height, int width, int labyrinthLenght)
    {
        PathGenerator pathGenerator = new PathGenerator();

        if (labyrinthLenght < height * width / 3)
        {
            Debug.Log("Too low map size for such path.");
            return _map;
        }
        else
        {
            int currentLabyrinthLenght = 0;
            do
            {
                PathMap pathMap = pathGenerator.GeneratePathMap(height, width);
                currentLabyrinthLenght = pathMap.PathLength;
            } while (currentLabyrinthLenght < labyrinthLenght);
        }
        return _map;
    }
    static private int[,] GanerateIds(PathMap pathMap)
    {
        return new int[0, 0];
    }
    static private void SetMapWall(Point wallPoint)
    {
        SetMapBlock(wallPoint, BlocksIds[Random.Range(0, BlocksIds.Length - 1)]);
    }
    static private void SetMapBlock(Point blockPoint, int id)
    {
        _map[blockPoint.Y, blockPoint.X] = id;
    }
    static private bool DefinedIdsExists(int[] blockIds)
    {
        for (int idNumber = 0; idNumber < blockIds.Length; idNumber++)
        {
            if(
                blockIds[idNumber] == PassId ||
                blockIds[idNumber] == EnterId ||
                blockIds[idNumber] == ExitId
                )
            {
                return false;
            }
        }
        return true;
    }
}

using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class MapGenerator
{
    static private int[,] _map = new int[0,0];
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


    static public int[,] GenerateLabyrinth(int height, int width, int minLabyrinthLenght)
    {
        PathGenerator pathGenerator = new PathGenerator();

        if (minLabyrinthLenght > height * width / 4)
        {
            Debug.Log("Too low map size ("+ height * width + " blocks) for such path ("+ minLabyrinthLenght + ").");
        }
        else
        {
            PathMap pathMap = pathGenerator.GeneratePathMap(height, width, minLabyrinthLenght);
            _map = GanerateIds(pathMap);
        }
        return _map;
    }
    static private int[,] GanerateIds(PathMap pathMap)
    {
        int[,] idsMap = new int[pathMap.PathMapArray.GetLength(0), pathMap.PathMapArray.GetLength(1)];
        for (int y = 0; y < pathMap.PathMapArray.GetLength(0); y++)
        {
            for (int x = 0; x < pathMap.PathMapArray.GetLength(1); x++)
            {
                if(pathMap.PathMapArray[y,x] == false)
                {
                    idsMap[y, x] = BlocksIds[Random.Range(0, BlocksIds.Length - 1)];
                }
                else
                {
                    idsMap[y, x] = PassId;
                }
            }
        }
        idsMap[pathMap.PathEnter.Y, pathMap.PathEnter.X] = EnterId;
        idsMap[pathMap.PathExit.Y, pathMap.PathExit.X] = ExitId;
        return idsMap;
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

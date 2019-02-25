using UnityEngine;

[CreateAssetMenu(menuName ="Test/MapGenerator")]
public class MapGeneratorTest : ScriptableObject
{
    private int[,] _map;
    private void OnEnable()
    {
        _map = MapGenerator.GenerateLabyrinth(9, 9, 10);
        
        ShowMap();
    }
    private void ShowMap()
    {
        string line = "\n";
        for (int y = 0; y < _map.GetLength(0); y++)
        {
            for (int x = 0; x < _map.GetLength(1); x++)
            {
                line += _map[y, x];
            }
            line += "\n";
        }
        Debug.Log(line);
    }
}

using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LevelProcessor : MonoBehaviour
{
    public int columns = 60;
    public int rows = 20;
    public int playerHeight = 1;
    public int maxJumpHeight = 3;
    public int maxJumpLength = 1;
    public int minGroundHeight = 4;
    public int minGroundLength = 4;
    public int maxGroundLength = 10;
    public float gapProbability = 0.2f;
    public float steepProbability = 0.5f;
    public float blockProbability = 0.3f;

    public void Reset()
    {
        ProcessLevel();
    }

    public void ProcessLevel()
    {
        List<IGenerator> generators = new List<IGenerator>();
        AddGenerators(generators);

        Level[,] map = new Level[rows, columns];
        InitializeMap(map);

        foreach (IGenerator generator in generators)
        {
            generator.Generate(map);
        }

        DebugMap(map);
        DrawMap(map);
    }

    void AddGenerators(List<IGenerator> generators)
    {
        LevelInfo levelInfo = new LevelInfo();
        levelInfo.columns = columns;
        levelInfo.rows = rows;
        levelInfo.playerHeight = playerHeight;
        levelInfo.maxJumpHeight = maxJumpHeight;
        levelInfo.maxJumpLength = maxJumpLength;
        levelInfo.minGroundHeight = minGroundHeight;
        levelInfo.minGroundLength = minGroundLength;
        levelInfo.maxGroundLength = maxGroundLength;
        levelInfo.gapProbability = gapProbability;
        levelInfo.steepProbability = steepProbability;
        levelInfo.blockProbability = blockProbability;

        GroundGenerator groundGenerator = new GroundGenerator(levelInfo);
        ObstacleGenerator obstacleGenerator = new ObstacleGenerator(levelInfo);
        BlockGenerator blockGenerator = new BlockGenerator(levelInfo);

        generators.Add(groundGenerator);
        generators.Add(obstacleGenerator);
        generators.Add(blockGenerator);
    }
    
    void InitializeMap(Level[,] map)
    {
        for (int r = 0; r < map.GetLength(0); r++)
        {
            for (int c = 0; c < map.GetLength(1); c++)
            {
                map[r, c] = Level.EMPTY;
            }
        }
    }

    void DebugMap(Level[,] map)
    {
        StringBuilder strBuilder = new StringBuilder();

        for (int r = 0; r < map.GetLength(0); r++)
        {
            for (int c = 0; c < map.GetLength(1); c++)
            {
                if (c != 0)
                {
                    strBuilder.Append(",");
                }

                strBuilder.Append((int) map[r, c]);
            }

            strBuilder.Append("\n");
        }

        Debug.Log(strBuilder.ToString());
    }

    void DrawMap(Level[,] map)
    {

    }
}

using System.Collections.Generic;
using UnityEngine;

public class LevelProcessor : MonoBehaviour
{
    public int columns = 100;
    public int rows = 25;
    public int playerHeight = 1;
    public int maxJumpHeight = 1;

    public void ProcessLevel()
    {
        List<IGenerator> generators = new List<IGenerator>();
        AddGenerators();

        int[,] map = new int[rows, columns];

        foreach (IGenerator generator in generators)
        {
            generator.Generate(map);
        }


    }

    void AddGenerators()
    {
        LevelInfo levelInfo;
        levelInfo.columns = columns;
        levelInfo.rows = rows;
        levelInfo.playerHeight = playerHeight;
        levelInfo.maxJumpHeight = maxJumpHeight;

        GroundGenerator groundGenerator = new GroundGenerator(levelInfo);
        ObstacleGenerator obstacleGenerator = new ObstacleGenerator(levelInfo);
        BlockGenerator blockGenerator = new BlockGenerator(levelInfo);
    }
}

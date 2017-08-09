using UnityEngine;

public class GroundGenerator : IGenerator
{
    LevelInfo levelInfo;

    public GroundGenerator(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
    }

    public void Generate(Level[,] map)
    {
        int lastRow = levelInfo.rows - 1;
        int lastColumn = levelInfo.columns - 1;

        int maxGroundHeight = levelInfo.rows - levelInfo.playerHeight - levelInfo.maxJumpHeight;

        float gapProbability = Random.Range(0f, 1f);

        // int minGroundCount = levelInfo.columns / levelInfo.maxJumpLength;
        // int maxGroundCount = levelInfo.columns;

        // int groundCount = Random.Range(minGroundCount, maxGroundCount);

        int groundLengthOffset = 0;

        // Vector2 groundBlock = new Vector2(0, 0);

        int lastGroundHeight = 0;

        for (int g = 0; g < 10; g++)
        {
            int groundHeight = Random.Range(levelInfo.minGroundHeight, lastGroundHeight + levelInfo.maxJumpHeight);
            int groundLength = Random.Range(levelInfo.minGroundLength, levelInfo.maxGroundLength);

            if (1 - gapProbability < levelInfo.gapProbability)
            {
                groundLengthOffset += levelInfo.maxJumpLength;
                continue;
            }

            for (int c = 0; c < groundLength && c <= lastColumn; c++)
            {
                for (int r = lastRow; r > (lastRow - groundHeight) && r >= 0; r--)
                {
                    if (c + groundLengthOffset > lastColumn)
                    {
                        break;
                    }

                    if (map[r, c + groundLengthOffset] == Level.EMPTY)
                    {
                        map[r, c + groundLengthOffset] = Level.GROUND;
                    }
                }
            }

            groundLengthOffset += groundLength;

            lastGroundHeight = groundHeight;
        }
    }
}
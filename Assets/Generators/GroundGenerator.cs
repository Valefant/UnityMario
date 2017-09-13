using UnityEngine;

public class GroundGenerator : IGenerator
{
    LevelInfo levelInfo;
    public static int lastGroundHeight = -1;

    public GroundGenerator(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
    }

    public void Generate(Level[,] map)
    {
        int lastRow = levelInfo.rows - 1;
        int lastColumn = levelInfo.columns - 1;

        int maxGroundHeight = levelInfo.rows - levelInfo.playerHeight - levelInfo.maxJumpHeight - 1;

        int groundCount = levelInfo.columns / levelInfo.minGroundLength;
        int groundLengthOffset = 0;

        int currentGroundHeight = Random.Range(levelInfo.minGroundHeight, maxGroundHeight);

        if (lastGroundHeight > 0)
        {
            currentGroundHeight = Random.Range(lastGroundHeight, lastGroundHeight + levelInfo.maxJumpHeight);
        }

        for (int g = 0; g < groundCount; g++)
        {
            float gapProbability = Random.Range(0f, 1f);
            float heightProbability = Random.Range(0f, 1f);
            float steepProbability = Random.Range(0f, 1f);

            int minGroundHeight = steepProbability >= levelInfo.steepProbability ? currentGroundHeight : levelInfo.minGroundHeight; 

            int groundHeight = Random.Range(minGroundHeight, currentGroundHeight + levelInfo.maxJumpHeight);

            if (groundHeight > maxGroundHeight)
            {
                groundHeight = maxGroundHeight;
            }

            int groundLength = Random.Range(levelInfo.minGroundLength, levelInfo.maxGroundLength);

            if (g > 0 && g < (groundCount - 1) && 1 - gapProbability < levelInfo.gapProbability)
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

            currentGroundHeight = groundHeight;
        }

        lastGroundHeight = currentGroundHeight;
    }
}
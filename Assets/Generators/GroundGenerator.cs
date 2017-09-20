using System.CodeDom;
using UnityEngine;

public class GroundGenerator : IGenerator
{
    LevelInfo _levelInfo;
    public static int lastGroundHeight = -1;

    public GroundGenerator(LevelInfo levelInfo)
    {
        this._levelInfo = levelInfo;
    }

    public void Generate(Level[,] map)
    {       
        int lastRow = _levelInfo.rows - 1;
        int lastColumn = _levelInfo.columns - 1;

        int maxGroundHeight = _levelInfo.rows - _levelInfo.playerHeight - _levelInfo.maxJumpHeight - 1;

        int groundCount = _levelInfo.columns / _levelInfo.minGroundLength;
        int groundLengthOffset = 0;

        int currentGroundHeight = Random.Range(_levelInfo.minGroundHeight, maxGroundHeight);

        if (lastGroundHeight > 0)
        {
//            currentGroundHeight = Random.Range(lastGroundHeight, lastGroundHeight + levelInfo.maxJumpHeight);
            currentGroundHeight = lastGroundHeight;
        }

        for (int g = 0; g < groundCount; g++)
        {
            float gapProbability = Random.Range(0f, 1f);
            float heightProbability = Random.Range(0f, 1f);
            float steepProbability = Random.Range(0f, 1f);

            int minGroundHeight = steepProbability >= _levelInfo.steepProbability ? currentGroundHeight : _levelInfo.minGroundHeight; 

            int groundHeight = Random.Range(minGroundHeight, currentGroundHeight + _levelInfo.maxJumpHeight - 1);

            if (groundHeight > maxGroundHeight)
            {
                groundHeight = maxGroundHeight;
            }

            int groundLength = Random.Range(_levelInfo.minGroundLength, _levelInfo.maxGroundLength);

            if (g > 0 && g < (groundCount - 1) && 1 - gapProbability < _levelInfo.gapProbability)
            {
                groundLengthOffset += _levelInfo.maxJumpLength;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : IGenerator
{
    LevelInfo levelInfo;

    public ItemGenerator(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
    }

    public void Generate(Level[,] map)
    {
        int lastRow = levelInfo.rows - 1;
        int lastColumn = levelInfo.columns - 1;

        List<Vector2> emptyLocationsAboveGround = new List<Vector2>();

        for (int r = lastRow - levelInfo.minGroundHeight; r > 0; r--)
        {
            for (int c = 0; c < lastColumn; c++)
            {
                if (r > 0 && map[r, c] == Level.GROUND && map[r - 1, c] == Level.EMPTY)
                {
                    emptyLocationsAboveGround.Add(new Vector2(c, r - 1));
                }
            }
        }

        Vector2 randomLocation = emptyLocationsAboveGround[Random.Range(0, emptyLocationsAboveGround.Count - 1)];

        map[(int) randomLocation.y, (int) randomLocation.x] = Level.STAR;
    }
}

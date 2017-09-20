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
        List<Vector2> emptyLocationsAboveGround = ExtensionMethods.MyExtensions.FindEmptyLocationsAboveGround(map, 0.1f);

        foreach (Vector2 emptyLocationAboveGround in emptyLocationsAboveGround)
        {
            map[(int) emptyLocationAboveGround.y, (int) emptyLocationAboveGround.x] = Level.COIN;   
        }
    }
}

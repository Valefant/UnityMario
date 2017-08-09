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
        List<Vector2> emptyLocationsAboveGround = ExtensionMethods.MyExtensions.FindEmptyLocationsAboveGround(map);

        Vector2 randomLocation = emptyLocationsAboveGround[Random.Range(0, emptyLocationsAboveGround.Count - 1)];

        map[(int) randomLocation.y, (int) randomLocation.x] = Level.STAR;
    }
}

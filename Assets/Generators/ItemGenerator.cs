using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : IGenerator
{
    LevelInfo levelInfo;
    Level[] types = new Level[2]{Level.COIN, Level.ENEMY};
    private float _monsterProbability = 0.2f;
    
    public ItemGenerator(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
    }

    public void Generate(Level[,] map)
    {
        List<Vector2> emptyLocationsAboveGround = ExtensionMethods.MyExtensions.FindEmptyLocationsAboveGround(map, 0.08f);

        int index = 0;
        
        foreach (Vector2 emptyLocationAboveGround in emptyLocationsAboveGround)
        {
            var random = Random.Range(0f, 1f);

            if (_monsterProbability >= random)
            {
                index = 1;
            }
            
            map[(int) emptyLocationAboveGround.y, (int) emptyLocationAboveGround.x] = types[index];

            index = 0;
        }
    }
}

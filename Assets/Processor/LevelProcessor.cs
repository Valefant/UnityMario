using System.Collections.Generic;
using UnityEngine;
using System.Text;


public class LevelProcessor : MonoBehaviour
{
    public int columns = 50;
    public int rows = 20;
    public int playerHeight = 1;
    public int maxJumpHeight = 1;
    public int maxJumpLength = 1;
    public int minGroundHeight = 2;
    public int minGroundLength = 3;
    public int maxGroundLength = 10;
    public float gapProbability = 0.1f;

    public List<GameObject> leftWorld = new List<GameObject>();
    public List<GameObject> rightWorld = new List<GameObject>();

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

        GroundGenerator groundGenerator = new GroundGenerator(levelInfo);
        ObstacleGenerator obstacleGenerator = new ObstacleGenerator(levelInfo);
        BlockGenerator blockGenerator = new BlockGenerator(levelInfo);
        ItemGenerator itemGenerator = new ItemGenerator(levelInfo);

        generators.Add(groundGenerator);
        generators.Add(obstacleGenerator);
        generators.Add(blockGenerator);
        generators.Add(itemGenerator);
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
        DestroyOldWorld();
        ChangeRightToLeftWorld();


        // build new rightWorld 
        BuildRightGround(map);
    }

    void DestroyOldWorld()
    {
        for (int i = 0; i < leftWorld.Count; i++)
        {
            Destroy(leftWorld[i]);
        }
    }

    void ChangeRightToLeftWorld()
    {
        leftWorld = rightWorld;
    }

    void BuildRightGround(Level[,] map)
    {
        int GroundHeight = 0, LastGroundHeight = 0;
        int GroundWidth = 0;
        int StartPosition = 0;


        for (int x = 0; x < map.GetLength(1); x++)
        {
            Debug.Log("X = " + (x+1));

            if (map[map.GetLength(0)-1, x] == Level.GROUND)
            {
                if (GroundWidth == 0)
                    StartPosition = x;

                GroundWidth++;

                for (int y = map.GetLength(0)-1; y > 0; y--)
                {
                    if (map[y, x] == Level.GROUND)
                        GroundHeight++;

                    Debug.Log("Y = " + (y+1));
                }

                if(GroundHeight > LastGroundHeight)
                {
                    // We need to build the Ground-Object
                    // and start with the next one

                    Debug.Log(string.Format("Position = {0}; GroundWeidth = {1}; GroundHeight = {2}", StartPosition, GroundWidth, GroundHeight));
                    LastGroundHeight = GroundHeight;
                    CreateGround(StartPosition, GroundWidth, GroundHeight);
                    GroundHeight = 0;
                    GroundWidth = 0;
                    continue;
                }

                //Debug.Log(string.Format("Ende X: Position = {0}; GroundWeidth = {1}; GroundHeight = {2}", StartPosition, GroundWidth, GroundHeight));
                LastGroundHeight = GroundHeight;
                GroundHeight = 0;
            }


        }
    }


    private void CreateGround(int StartPosition, int GroundWidth, int GroundHeight)
    {
        GameObject ground = new GameObject();

        ground.AddComponent<Ground>();
        Debug.Log(string.Format("Ground: StartPosition = {0}; GroundWidth = {1}; GroundHeight = {2}", StartPosition, GroundWidth, GroundHeight));
        ground.GetComponent<Ground>().Height = GroundHeight;
        ground.GetComponent<Ground>().Width = GroundWidth;
        ground.GetComponent<Ground>().Depth = 1;
        ground.GetComponent<Ground>().CreateMesh();

        Transform transform = ground.GetComponent<Transform>();
        transform.position = new Vector2(StartPosition, 0);

        ground.transform.parent = this.transform;
        leftWorld.Add(ground);
    }


}

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Assets.EventSystem;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class LevelProcessor : MonoBehaviour
{
    public int columns = 40;
    public int rows = 20;
    public int playerHeight = 1;
    public int maxJumpHeight = 3;
    public int maxJumpLength = 2;
    public int minGroundHeight = 5;
    public int minGroundLength = 4;
    public int maxGroundLength = 10;
    public int columnPosition = 0;
    public float gapProbability = 0.2f;
    public float steepProbability = 0.5f;
    public float blockProbability = 0.35f;
    public Vector3 startingPosition = Vector3.zero;
    public int levelCount = 0;

    public static String Seed;
    public List<List<GameObject>> levelSections = new List<List<GameObject>>();
    private List<IGenerator> _generators = new List<IGenerator>();

    public void Reset()
    {
        ProcessLevel();
    }

    public void ProcessLevel()
    {
        Debug.Log("ProcessLevel");
        List<IGenerator> generators = new List<IGenerator>();
        AddGenerators(generators);

        if (generators.Count <= 0)
        {
            AddGenerators(generators);
        }

        Level[,] map = new Level[rows, columns];
        InitializeMap(map);

        foreach (IGenerator generator in generators)
        {
            generator.Generate(map);
        }

        DebugMap(map);
        DrawMap(map);

        levelCount++;
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

        Seed = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        Random.InitState(Seed.GetHashCode());
        
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
        BuildGround(map);
    }

    void BuildGround(Level[,] map)
    {
        List<GameObject> levelSection = new List<GameObject>();

        int GroundHeight = 0, LastGroundHeight = 0;
        int GroundWidth = 0;
        int StartPosition = columnPosition;
        bool Start = true;


        for (int x = 0; x < map.GetLength(1); x++)
        {
            if (map[map.GetLength(0) - 1, x] == Level.EMPTY)
            {
                // Debug.Log(string.Format("Position = {0}; GroundWeidth = {1}; GroundHeight = {2}", StartPosition, GroundWidth, GroundHeight));
                // Debug.Log(string.Format("X = {0}", x));
                CreateGround(StartPosition, GroundWidth, LastGroundHeight, levelSection);
                LastGroundHeight = 0;
                GroundHeight = 0;
                GroundWidth = 0;
                Start = true;
            }


            if (map[map.GetLength(0) - 1, x] == Level.GROUND)
            {
                if (GroundWidth == 0)
                    StartPosition = x + columnPosition;

                GroundWidth++;

                for (int y = map.GetLength(0) - 1; y >= 0; y--)
                {
                    if (map[y, x] == Level.GROUND)
                        GroundHeight++;

                    // Debug.Log("Y = " + (y));
                }

                if (!Start)
                {
                    if (GroundHeight != LastGroundHeight)
                    {
                        // We need to build the Ground-Object
                        // and start with the next one

                        // Debug.Log(string.Format("Position = {0}; GroundWeidth = {1}; GroundHeight = {2}", StartPosition, GroundWidth, GroundHeight));
                        CreateGround(StartPosition, GroundWidth, LastGroundHeight, levelSection);
                        LastGroundHeight = GroundHeight;
                        GroundHeight = 0;
                        GroundWidth = 0;
                        continue;
                    }
                }
                else
                {
                    Start = false;
                    LastGroundHeight = GroundHeight;
                }

                LastGroundHeight = GroundHeight;
                GroundHeight = 0;
            }
        }
        CreateGround(StartPosition, GroundWidth, LastGroundHeight, levelSection);

        BuildBlockTypes(map, levelSection);
        PlaceCoins(map, levelSection);
        columnPosition += columns;

        Debug.Log("Startposition: " + StartPosition);

        levelSections.Add(levelSection);
    }


    private void BuildBlockTypes(Level[,] map, List<GameObject> levelSection)
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                IfBlockTypeCreate(map[y, x], (map.GetLength(0) - y) - 1, x, levelSection);
            }
        }
    }

    private void PlaceCoins(Level[,] map, List<GameObject> levelSection)
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == Level.COIN)
                {
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                    cube.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                    cube.AddComponent<Rotator>();
                    cube.GetComponent<BoxCollider>().isTrigger = true;

                    int adjustedX = x + levelSections.Count * columns;
                    int adjustedY = (map.GetLength(0) - y);

                    cube.transform.localPosition = new Vector3(adjustedX, adjustedY - 0.25f, 0.5f);
                    levelSection.Add(cube);
                }
            }
        }
    }

    private void CreateGround(int StartPosition, int GroundWidth, int GroundHeight, List<GameObject> levelSection)
    {
        if (GroundWidth <= 2)
            return;

        GameObject ground = new GameObject();

        ground.AddComponent<Ground>();
        ground.GetComponent<Ground>().Height = GroundHeight;
        ground.GetComponent<Ground>().Width = GroundWidth;
        ground.GetComponent<Ground>().Depth = 1;
        ground.GetComponent<Ground>().CreateMesh();

        Transform transform = ground.GetComponent<Transform>();
        transform.position = new Vector2(StartPosition, 0);

        ground.transform.parent = this.transform;

        BoxCollider boxCollider = ground.AddComponent<BoxCollider>();

        if (startingPosition == Vector3.zero)
        {
            startingPosition = new Vector3(Random.Range(1, GroundWidth / 2), GroundHeight + 1, 0.5f);
        }

        levelSection.Add(ground);
    }

    private void IfBlockTypeCreate(Level level, int r, int c, List<GameObject> levelSection)
    {
        List<Level> blockTypes = new List<Level>
        {
            Level.BRICK_BLOCK,
            Level.QUESTION_MARK_BLOCK,
            Level.EXCLAMATION_RED_BLOCK,
            Level.EXCLAMATION_GREEN_BLOCK,
            Level.EXCLAMATION_BLUE_BLOCK
        };

        if (blockTypes.Contains(level))
        {
            CreateBlockType(level, r, c, levelSection);
        }
    }

    private void CreateBlockType(Level level, int r, int c, List<GameObject> levelSection)
    {
        GameObject block = new GameObject();
        block.AddComponent<Block>();

        Block blockComponent = block.GetComponent<Block>();
        blockComponent.width = 1;
        blockComponent.height = 1;
        blockComponent.depth = 1;

        if (level == Level.BRICK_BLOCK)
        {
            blockComponent.blocktype = "Brick".ToLower();
        }

        if (level == Level.QUESTION_MARK_BLOCK)
        {
            blockComponent.blocktype = "Question Mark".ToLower();
        }

        if (level == Level.EXCLAMATION_RED_BLOCK)
        {
            blockComponent.blocktype = "Exclamation Red".ToLower();
        }

        if (level == Level.EXCLAMATION_GREEN_BLOCK)
        {
            blockComponent.blocktype = "Exclamation Green".ToLower();
        }

        if (level == Level.EXCLAMATION_BLUE_BLOCK)
        {
            blockComponent.blocktype = "Exclamation Blue".ToLower();
        }

        blockComponent.CreateMesh();

        block.transform.position = new Vector3(c + columnPosition, r, 0);
        block.transform.parent = this.transform;
        BoxCollider boxCollider = block.AddComponent<BoxCollider>();

        levelSection.Add(block);
    }

    private void IfItemCreate(Level level, int r, int c)
    {
        if (level == Level.STAR)
        {
            GameObject star = new GameObject();
            star.AddComponent<Star>();

            Star starComponent = star.GetComponent<Star>();
            starComponent.depth = 1;
            starComponent.size = 1f;
            starComponent.CreateMesh();

            star.transform.position = new Vector3(c, r, 0);
            star.transform.parent = this.transform;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LevelProcessor : MonoBehaviour
{
    public int columns = 50;
    public int rows = 20;
    public int playerHeight = 1;
    public int maxJumpHeight = 3;
    public int maxJumpLength = 1;
    public int minGroundHeight = 4;
    public int minGroundLength = 4;
    public int maxGroundLength = 10;
	public int columnPosition = 0;
    public float gapProbability = 0.2f;
    public float steepProbability = 0.5f;
    public float blockProbability = 0.35f;
	public Vector3 startingPosition = Vector3.zero;

    private List<GameObject> leftWorld = new List<GameObject>();
    private List<GameObject> rightWorld = new List<GameObject>();
	private List<IGenerator> generators = new List<IGenerator>();

    public void Reset()
    {
        ProcessLevel();
		Debug.Log ("Last gh: " + GroundGenerator.lastGroundHeight);
		ProcessLevel();
		Debug.Log ("Last gh: " + GroundGenerator.lastGroundHeight);
		ProcessLevel();
		Debug.Log ("Last gh: " + GroundGenerator.lastGroundHeight);
		ProcessLevel();
		Debug.Log ("Last gh: " + GroundGenerator.lastGroundHeight);
		ProcessLevel();
    }

    public void ProcessLevel()
    {
        Debug.Log("ProcessLevel");
        List<IGenerator> generators = new List<IGenerator>();
        AddGenerators(generators);
        
		    if (generators.Count <= 0) {
			    AddGenerators(generators);
		    }

        Level[,] map = new Level[rows, columns];
        InitializeMap(map);

        foreach (IGenerator generator in generators)
        {
            generator.Generate(map);
        }

        // DebugMap(map);
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
        levelInfo.steepProbability = steepProbability;
        levelInfo.blockProbability = blockProbability;

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
		int StartPosition = columnPosition;
        bool Start = true;


        for (int x = 0; x < map.GetLength(1); x++)
        {
            // Debug.Log("X = " + (x));

            //if(map[map.GetLength(0) - 1, x] == Level.GROUND)
            //{
            //    Debug.Log(string.Format("Position = {0}; GroundWeidth = {1}; GroundHeight = {2}", StartPosition, GroundWidth, GroundHeight));
            //    CreateGround(StartPosition, GroundWidth - 1, LastGroundHeight);
            //    LastGroundHeight = GroundHeight;
            //    GroundHeight = 0;
            //    GroundWidth = 1;
            //    continue;
            //}


            if (map[map.GetLength(0) - 1, x] == Level.EMPTY)
            {
                // Debug.Log(string.Format("Position = {0}; GroundWeidth = {1}; GroundHeight = {2}", StartPosition, GroundWidth, GroundHeight));
                // Debug.Log(string.Format("X = {0}", x));
                CreateGround(StartPosition, GroundWidth, LastGroundHeight);
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
                        CreateGround(StartPosition, GroundWidth, LastGroundHeight);
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

                //Debug.Log(string.Format("Ende X: Position = {0}; GroundWeidth = {1}; GroundHeight = {2}", StartPosition, GroundWidth, GroundHeight));
                LastGroundHeight = GroundHeight;
                GroundHeight = 0;
            }
        }
        CreateGround(StartPosition, GroundWidth, LastGroundHeight);

        BuildBlockTypes(map);

		columnPosition += columns;

		Debug.Log ("Startposition: " + StartPosition);
    }


    private void BuildBlockTypes(Level[,] map)
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                IfBlockTypeCreate(map[y,x],(map.GetLength(0)-y)-1, x);
            }
        }
    }

    private void CreateGround(int StartPosition, int GroundWidth, int GroundHeight)
    {
        GameObject ground = new GameObject();

        ground.AddComponent<Ground>();
        // Debug.Log(string.Format("Ground: StartPosition = {0}; GroundWidth = {1}; GroundHeight = {2}", StartPosition, GroundWidth, GroundHeight));
        ground.GetComponent<Ground>().Height = GroundHeight;
        ground.GetComponent<Ground>().Width = GroundWidth;
        ground.GetComponent<Ground>().Depth = 1;
        ground.GetComponent<Ground>().CreateMesh();

        Transform transform = ground.GetComponent<Transform>();
        transform.position = new Vector2(StartPosition, 0);

        ground.transform.parent = this.transform;

        BoxCollider boxCollider = ground.AddComponent<BoxCollider>();

        leftWorld.Add(ground);

		if (startingPosition == Vector3.zero)
		{
			startingPosition = new Vector3(Random.Range(1, GroundWidth / 2), GroundHeight + 1, 0.5f);
		}	
    }

    private void IfBlockTypeCreate(Level level, int r, int c)
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
            CreateBlockType(level, r, c);
        }
    }

    private void CreateBlockType(Level level, int r, int c)
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

        rightWorld.Add(block);
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

            rightWorld.Add(star);
        }
    }
}

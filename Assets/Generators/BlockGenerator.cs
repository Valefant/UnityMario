using UnityEngine;

public class BlockGenerator : IGenerator
{
    LevelInfo levelInfo;

    public BlockGenerator(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
    }

    public void Generate(Level[,] map)
    {
        // propability for the block types
        float[] props = new float[5]
        {
            0.70f,
            0.295f,
            0.003f,
            0.002f,
            0.001f
        };

        // the order of the block types relates to the order of the props array
        Level[] blockTypes = new Level[] {
            Level.BRICK_BLOCK,
            Level.QUESTION_MARK_BLOCK,
            Level.EXCLAMATION_RED_BLOCK,
            Level.EXCLAMATION_GREEN_BLOCK,
            Level.EXCLAMATION_BLUE_BLOCK
        };

        int lastRow = levelInfo.rows - 1;
        int lastColumn = levelInfo.columns - 1;

        for (int r = lastRow - levelInfo.minGroundHeight; r > 0; r--)
        {
            int rowToPlaceBlock = Random.Range(levelInfo.playerHeight, levelInfo.maxJumpHeight + 1);
            float blockProbability = Random.Range(0f, 1f);
            for (int c = 0; c < lastColumn; c++)
            {
                float continueBlock = Random.Range(0f, 1f);

                if (blockCanBePlaced(r, c, rowToPlaceBlock, map) && 1 - blockProbability < levelInfo.blockProbability)
                {
                    if (continueBlock > 0.3f)
                    {
                        map[r - 1 - rowToPlaceBlock, c] = blockTypes[ChooseBlockType(props)];
                    }
                }
            }
        }
    }
    int ChooseBlockType(float[] props)
    {
        float random = Random.Range(0f, 1f);

        for (int i = 0; i < props.Length; i++)
        {
            if (random < props[i])
            {
                return i;
            }
            else
            {
                random -= props[i];
            }
        }

        // the random value can be one so we won't find the random point anywhere
        return props.Length - 1;
    }

    private bool blockCanBePlaced(int r, int c, int rowToPlaceBlock, Level[,] map)
    {
        return r - rowToPlaceBlock >= 0 &&
               map[r, c] == Level.GROUND &&
               map[r - 1, c] == Level.EMPTY &&
               map[r - 1 - rowToPlaceBlock, c + 1] == Level.EMPTY &&
               map[r - rowToPlaceBlock, c + 1] == Level.EMPTY;
    }
}
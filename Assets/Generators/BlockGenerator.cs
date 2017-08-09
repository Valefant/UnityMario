public class BlockGenerator : IGenerator
{
    LevelInfo levelInfo;

    public BlockGenerator(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
    }

    public void Generate(Level[,] map)
    {

    }
}

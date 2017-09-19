public class GenerateSectionEvent : IEventable
{
    public string GetName()
    {
        return GetType().Name;
    }
}
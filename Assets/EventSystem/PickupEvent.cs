public class PickupEvent : IEventable
{
    private readonly int _points;

    public PickupEvent(int points)
    {
        this._points = points;
    }

    public string GetName()
    {
        return "PickupEvent";
    }

    public int GetPoints()
    {
        return _points;
    }
}
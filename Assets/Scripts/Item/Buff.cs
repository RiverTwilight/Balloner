public class Buff
{
    public string Name { get; private set; }
    public float Duration { get; set; }  // Make the set accessor public

    public Buff(string name, float duration)
    {
        Name = name;
        Duration = duration;
    }
}
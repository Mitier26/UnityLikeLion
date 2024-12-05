
public enum TargetType { SingleEnemy, AllEnemies, SingleAlly, AllAllies }

[System.Serializable]
public class Skill
{
    public string Name;
    public int Power;
    public TargetType Target;

    public Skill(string name, int power, TargetType target)
    {
        Name = name;
        Power = power;
        Target = target;
    }
}

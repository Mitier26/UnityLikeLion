
public enum TargetType { SingleEnemy, AllEnemies, SingleAlly, AllAllies }

[System.Serializable]
public class Skill
{
    public string Name;
    public int Power;
    public TargetType target;
}

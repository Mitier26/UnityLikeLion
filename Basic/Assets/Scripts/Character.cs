
[System.Serializable]
public class Character
{
    public string Name;
    public int HP;
    public int Attack;
    public int Defense;
    public bool IsPlayer;

    public Character(string name, int hp, int atk, int def, bool isPlayer)
    {
        Name = name;
        HP = hp;
        Attack = atk;
        Defense = def;
        IsPlayer = isPlayer;
    }
}
